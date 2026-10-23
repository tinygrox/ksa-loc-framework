using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace KsaLocFramework.XmlPatch;

public static class XmlPatchLoader
{
    public static List<XmlPatchOperation> Load(string directory)
    {
        List<XmlPatchOperation> operations = new List<XmlPatchOperation>();
        if (!Directory.Exists(directory))
        {
            Console.WriteLine($"KSA localization framework: patch directory '{directory}' not found.");
            return operations;
        }
        string[] files = Directory.GetFiles(directory, "*.xml");
        Array.Sort(files, StringComparer.OrdinalIgnoreCase);
        foreach (string file in files)
        {
            try
            {
                XDocument document = XDocument.Load(file);
                foreach (XElement element in document.Root.Elements())
                {
                    operations.Add(Parse(element));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(
                    $"KSA localization framework: patch file '{Path.GetFileName(file)}' skipped: {exception.GetBaseException().Message}");
            }
        }
        Console.WriteLine(
            $"KSA localization framework: loaded {operations.Count} XML patch operations from {files.Length} file(s).");
        return operations;
    }

    private static XmlPatchOperation Parse(XElement element)
    {
        return element.Name.LocalName switch
        {
            "Localize" => new XmlLocalizeOperation
            {
                XPath = (string)element.Attribute("XPath"),
                Element = (string)element.Attribute("Element"),
                Attribute = (string)element.Attribute("Attribute"),
                Value = (string)element.Attribute("Value"),
                Key = (string)element.Attribute("Key")
            },
            "Replace" => new XmlReplaceOperation
            {
                XPath = (string)element.Attribute("XPath"),
                Value = (string)element.Attribute("Value")
            },
            _ => throw new FormatException($"Unknown operation '{element.Name.LocalName}'.")
        };
    }
}
