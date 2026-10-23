using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using Brutal.Localization;
using KSA;

namespace KsaLocFramework.XmlPatch;

public static class XmlPatchEngine
{
    private static List<XmlPatchOperation> _operations = new List<XmlPatchOperation>();

    private static readonly Dictionary<string, string> _systemNames = new Dictionary<string, string>(StringComparer.Ordinal);

    private static Dictionary<string, string> _translations = new Dictionary<string, string>(StringComparer.Ordinal);

    public static bool Enabled => _operations.Count > 0 && _translations.Count > 0;

    public static void LoadPatches(string directory)
    {
        _operations = XmlPatchLoader.Load(directory);
    }

    public static void SetTranslations(Dictionary<LStringId, string> translations)
    {
        Dictionary<string, string> converted = new Dictionary<string, string>(StringComparer.Ordinal);
        if (translations != null)
        {
            foreach (KeyValuePair<LStringId, string> translation in translations)
            {
                converted[translation.Key.ToString()] = translation.Value;
            }
        }
        _translations = converted;
    }

    public static int Apply(XDocument document, string filePath)
    {
        if (!Enabled)
        {
            return 0;
        }
        int changed = 0;
        foreach (XmlPatchOperation operation in _operations)
        {
            changed += operation.Apply(document, Lookup);
        }
        if (changed > 0)
        {
            Console.WriteLine(
                $"KSA localization framework: {Path.GetFileName(filePath)} localized ({changed} values).");
        }
        return changed;
    }

    public static XmlSerializer SerializerFor(Type type)
    {
        if (!XmlHelper.Serializers.TryGetValue(type, out XmlSerializer serializer))
        {
            serializer = new XmlSerializer(type);
            XmlHelper.Serializers.Add(type, serializer);
        }
        return serializer;
    }

    public static T Load<T>(string filePath, Mod mod) where T : class, ILibraryData
    {
        XDocument document = ReadPatched(filePath);
        if (document == null)
        {
            return XmlLoader.Load<T>(filePath, mod);
        }
        T result = DeserializePatched<T>(document, filePath);
        if (result == null)
        {
            return null;
        }
        result.OnDataLoad(mod);
        return result;
    }

    public static void CaptureSystemNames()
    {
        foreach (SystemInfo system in SelectSystem.Systems)
        {
            if (!_systemNames.ContainsKey(system.Id))
            {
                _systemNames[system.Id] = system.DisplayName.Value;
            }
        }
    }

    public static void ApplySystemNames()
    {
        foreach (SystemInfo system in SelectSystem.Systems)
        {
            if (_systemNames.TryGetValue(system.Id, out string original))
            {
                system.DisplayName.Value = Lookup(original) ?? original;
            }
        }
    }

    private static T DeserializePatched<T>(XDocument document, string filePath) where T : class
    {
        try
        {
            using MemoryStream stream = new MemoryStream();
            document.Save(stream, SaveOptions.DisableFormatting);
            stream.Position = 0L;
            return SerializerFor(typeof(T)).Deserialize(stream) as T;
        }
        catch (Exception exception)
        {
            Console.WriteLine(
                $"KSA localization framework: failed to deserialize patched {Path.GetFileName(filePath)}: {exception.GetBaseException().Message}");
            return null;
        }
    }

    private static XDocument ReadPatched(string filePath)
    {
        if (!Enabled || !File.Exists(filePath))
        {
            return null;
        }
        try
        {
            XDocument document = XDocument.Load(filePath);
            return Apply(document, filePath) > 0 ? document : null;
        }
        catch (Exception exception)
        {
            Console.WriteLine(
                $"KSA localization framework: patch skipped for {Path.GetFileName(filePath)}: {exception.GetBaseException().Message}");
            return null;
        }
    }

    private static string Lookup(string key)
    {
        return key != null && _translations.TryGetValue(key, out string value) ? value : null;
    }
}
