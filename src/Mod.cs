using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Brutal.ImGuiApi;
using Brutal.Localization;
using HarmonyLib;
using KSA;
using StarMap.API;
using Tomlet;
using Tomlet.Models;

namespace KsaLocFramework;

[StarMapMod]
public class LocFrameworkMod
{
    private static Harmony _harmony;
    private static string _settingsPath;
    internal static readonly List<string> FontPaths = new();

    private const string DefaultLanguage = "en-US";
    private const string ConfigDirName = "Config";
    private const string SettingsFileName = "ModSettings.toml";

    private static readonly AccessTools.FieldRef<Language, Dictionary<LStringId, string>> LocalisedStrings =
        AccessTools.FieldRefAccess<Language, Dictionary<LStringId, string>>("_localisedStrings");

    private static readonly AccessTools.FieldRef<Mod, string[]> DeclaredFonts =
        AccessTools.FieldRefAccess<Mod, string[]>("Fonts");

    internal static string SelectedLanguage;

    [StarMapImmediateLoad]
    public void ImmediateLoad(Mod definingMod)
    {
        _settingsPath = Path.Combine(definingMod.DirectoryPath, ConfigDirName, SettingsFileName);
        LoadSettings();

        _harmony = new Harmony("tinygrox.KSAMod.LocFramework");
        ApplyPatches(_harmony, typeof(LocFrameworkMod).Assembly);

        RegisterFonts(definingMod);
        FontManager.RegenerateFonts();

        Console.WriteLine("KSA localization framework loaded.");
    }

    internal static void AddFontPath(string fontPath)
    {
        if (File.Exists(fontPath) && !FontPaths.Contains(fontPath))
        {
            FontPaths.Add(fontPath);
        }
    }

    private static void RegisterFonts(Mod definingMod)
    {
        string[] declaredFonts = DeclaredFonts(definingMod);
        if (declaredFonts == null || declaredFonts.Length == 0)
        {
            return;
        }

        foreach (string font in declaredFonts)
        {
            AddFontPath(Path.Combine(definingMod.DirectoryPath, font));
        }
    }

    private static void ApplyPatches(Harmony harmony, Assembly assembly)
    {
        foreach (Type type in AccessTools.GetTypesFromAssembly(assembly))
        {
            if (HarmonyMethodExtensions.GetFromType(type).Count <= 0)
            {
                continue;
            }

            try
            {
                harmony.CreateClassProcessor(type).Patch();
            }
            catch (Exception exception)
            {
                Console.WriteLine(
                    $"KSA localization framework: patch class {type.FullName} failed: {exception.GetBaseException().Message}");
            }
        }
    }

    internal static void ApplyLanguage(Language language)
    {
        if (language.Id == DefaultLanguage)
        {
            Brutal.Localization.LStrings.RemoveLocalizedStringProvider();
            return;
        }
        Dictionary<LStringId, string> localisedStrings = LocalisedStrings(language);
        Brutal.Localization.LStrings.SetLocalizedStringProvider(
            (LString lString, out string localizedTemplateString) => localisedStrings.TryGetValue(lString.Id, out localizedTemplateString));
    }

    private static void SelectLanguage(Language language)
    {
        SelectedLanguage = language.Id;
        ApplyLanguage(language);
        SaveSettings();
    }

    private static void LoadSettings()
    {
        if (!File.Exists(_settingsPath)) return;
        TomlDocument document = new TomlParser().Parse(File.ReadAllText(_settingsPath));
        ModSettings settings = TomletMain.To<ModSettings>(document, new TomlSerializerOptions());
        SelectedLanguage = settings.Language;
    }

    private static void SaveSettings()
    {
        ModSettings settings = new ModSettings { Language = SelectedLanguage };
        Directory.CreateDirectory(Path.GetDirectoryName(_settingsPath));
        File.WriteAllText(_settingsPath, TomletMain.TomlStringFrom(settings));
    }

    public static void DrawLanguageOptions()
    {
        ConsoleWidgets.BeginRow(GameStrings.KLF_UI_Language.Format());
        if (ConsoleWidgets.BeginComboControl("Lang", SelectedLanguage ?? DefaultLanguage, false))
        {
            foreach (Language language in Languages.All.UnsafeAsList())
            {
                if (ImGui.Selectable(language.Id, language.Id == SelectedLanguage))
                {
                    SelectLanguage(language);
                }

                if (language.Id == SelectedLanguage)
                {
                    ImGui.SetItemDefaultFocus();
                }
            }
            ConsoleWidgets.EndComboControl();
        }
        ConsoleWidgets.EndRow();
    }
}
