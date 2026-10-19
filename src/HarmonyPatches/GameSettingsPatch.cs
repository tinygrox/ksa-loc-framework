using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(GameSettings))]
public static class GameSettingsPatch
{
    [HarmonyPatch(nameof(GameSettings.OnDrawUi))]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnDrawUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        ReplaceSettingsText(codes);
        ReplaceNavRailLabels(codes);
        LocalizeSettingsTabNames(codes);
        InjectLanguageOptions(codes);
        return codes;
    }

    [HarmonyPatch("DrawFooter")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawFooterTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_Apply));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_Reset));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_Discard));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_ResetKeys));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_Close));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_NoPendingChanges));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_TabPending));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_TabsPending));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_Pending));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_RestartRequired));
        return codes;
    }

    private static void ReplaceSettingsText(List<CodeInstruction> codes)
    {
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_Title));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_Output));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_CameraAndFrame));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_Display));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_Unlimited));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_Fps));
    }

    private static void ReplaceNavRailLabels(List<CodeInstruction> codes)
    {
        FieldInfo navRailLabels = AccessTools.Field(typeof(GameSettings), "_navRailLabels");
        MethodInfo replacement = AccessTools.Method(typeof(SettingsUiText), nameof(SettingsUiText.NavRailLabels));
        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode == OpCodes.Ldsfld && codes[i].operand is FieldInfo field && field == navRailLabels)
            {
                codes[i].opcode = OpCodes.Call;
                codes[i].operand = replacement;
            }
        }
    }

    private static void LocalizeSettingsTabNames(List<CodeInstruction> codes)
    {
        Type settingsTabType = AccessTools.Inner(typeof(GameSettings), "SettingsTab");
        MethodInfo getSettingsTabName = AccessTools.Method(
            typeof(EnumCollection<,>).MakeGenericType(settingsTabType, typeof(string)),
            "GetName",
            new[] { settingsTabType });
        MethodInfo replacement = AccessTools.Method(typeof(SettingsUiText), nameof(SettingsUiText.LocalizeTabName));
        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode == OpCodes.Callvirt && codes[i].operand is MethodInfo method && method == getSettingsTabName)
            {
                codes.Insert(i + 1, new CodeInstruction(OpCodes.Call, replacement));
                i++;
            }
        }
    }

    private static void InjectLanguageOptions(List<CodeInstruction> codes)
    {
        FieldInfo settingsInterfaceGeneral = AccessTools.Field(typeof(LStrings), "SettingsInterfaceGeneral");
        MethodInfo regionHeaderMethod = AccessTools.Method(typeof(ConsoleWidgets), nameof(ConsoleWidgets.RegionHeader));
        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Call || !(codes[i].operand is MethodInfo method) || method != regionHeaderMethod)
            {
                continue;
            }

            for (int j = Math.Max(0, i - 5); j < i; j++)
            {
                if (codes[j].opcode == OpCodes.Ldsfld && codes[j].operand is FieldInfo field && field == settingsInterfaceGeneral)
                {
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(LocFrameworkMod), nameof(LocFrameworkMod.DrawLanguageOptions))));
                    return;
                }
            }
        }
    }
}
