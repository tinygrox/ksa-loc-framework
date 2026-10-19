using System;
using System.Collections.Generic;
using Brutal.Localization;

namespace KsaLocFramework;

public static class SettingsUiText
{
    private static readonly LString[] NavLabelTexts = new LString[]
    {
        GameStrings.KLF_SettingsTab_Display,
        GameStrings.KLF_SettingsTab_Graphics,
        GameStrings.KLF_SettingsTab_Audio,
        GameStrings.KLF_SettingsTab_Interface,
        GameStrings.KLF_SettingsTab_Controls,
        GameStrings.KLF_SettingsTab_Simulation,
        GameStrings.KLF_SettingsTab_Console,
        GameStrings.KLF_SettingsTab_Mods
    };

    private static readonly Dictionary<string, LString> TabNameTexts =
        new Dictionary<string, LString>(StringComparer.Ordinal)
        {
            ["Mods"] = GameStrings.KLF_Settings_ModsRegion
        };

    private static readonly Dictionary<string, LString> EnumNameTexts =
        new Dictionary<string, LString>(StringComparer.Ordinal)
        {
            ["Windowed"] = GameStrings.KLF_Settings_Enum_Windowed,
            ["Borderless"] = GameStrings.KLF_Settings_Enum_Borderless,
            ["Fullscreen"] = GameStrings.KLF_Settings_Enum_Fullscreen,
            ["Disabled"] = GameStrings.KLF_Settings_Enum_Disabled,
            ["Cmaa2"] = GameStrings.KLF_Settings_Enum_Cmaa2,
            ["Multisample X2"] = GameStrings.KLF_Settings_Enum_MultisampleX2,
            ["Multisample X4"] = GameStrings.KLF_Settings_Enum_MultisampleX4,
            ["Multisample X8"] = GameStrings.KLF_Settings_Enum_MultisampleX8,
            ["Unlimited"] = GameStrings.KLF_Settings_Enum_Unlimited,
            ["One Frame"] = GameStrings.KLF_Settings_Enum_OneFrame,
            ["Off"] = GameStrings.KLF_Settings_Enum_Off,
            ["Low"] = GameStrings.KLF_Settings_Enum_Low,
            ["Medium"] = GameStrings.KLF_Settings_Enum_Medium,
            ["High"] = GameStrings.KLF_Settings_Enum_High,
            ["Ultra"] = GameStrings.KLF_Settings_Enum_Ultra,
            ["Extreme"] = GameStrings.KLF_Settings_Enum_Extreme,
            ["Very Low"] = GameStrings.KLF_Settings_Enum_VeryLow,
            ["Hard"] = GameStrings.KLF_Settings_Enum_Hard,
            ["Pcf"] = GameStrings.KLF_Settings_Enum_Pcf,
            ["Pcss"] = GameStrings.KLF_Settings_Enum_Pcss,
            ["Uncharted"] = GameStrings.KLF_Settings_Enum_Uncharted,
            ["Agx"] = GameStrings.KLF_Settings_Enum_Agx,
            ["Aces Fitted"] = GameStrings.KLF_Settings_Enum_AcesFitted,
            ["Aces"] = GameStrings.KLF_Settings_Enum_Aces,
            ["Filmic"] = GameStrings.KLF_Settings_Enum_Filmic,
            ["Rom Bin Da House"] = GameStrings.KLF_Settings_Enum_RomBinDaHouse,
            ["Reinhard Simple"] = GameStrings.KLF_Settings_Enum_ReinhardSimple,
            ["Reinhard Luma"] = GameStrings.KLF_Settings_Enum_ReinhardLuma,
            ["Gran Turismo"] = GameStrings.KLF_Settings_Enum_GranTurismo,
            ["Linear"] = GameStrings.KLF_Settings_Enum_Linear,
            ["Hable"] = GameStrings.KLF_Settings_Enum_Hable
        };

    public static string[] NavRailLabels()
    {
        string[] labels = new string[NavLabelTexts.Length];
        for (int i = 0; i < NavLabelTexts.Length; i++)
        {
            labels[i] = NavLabelTexts[i].Format();
        }

        return labels;
    }

    public static string LocalizeTabName(string name)
    {
        if (name != null && TabNameTexts.TryGetValue(name, out LString text))
        {
            return text.Format();
        }

        return name;
    }

    public static string LocalizeEnumName(string name)
    {
        if (name != null && EnumNameTexts.TryGetValue(name, out LString text))
        {
            return text.Format();
        }

        return name;
    }
}
