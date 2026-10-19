using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Brutal.ImGuiApi;
using Brutal.Localization;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch]
public static class ImGuiHelperPatch
{
    [HarmonyTargetMethods]
    public static IEnumerable<MethodBase> TargetMethods()
    {
        Type enumPlaceholder = Type.MakeGenericMethodParameter(0);
        MethodInfo combo = AccessTools.Method(typeof(ImGuiHelper), "DrawCombo", new[]
        {
            typeof(ImString),
            enumPlaceholder.MakeByRefType(),
            typeof(StringEnumCollection<>).MakeGenericType(enumPlaceholder),
            typeof(ReadOnlySpan<>).MakeGenericType(enumPlaceholder),
            typeof(bool)
        });
        MethodInfo comboWithTooltips = AccessTools.Method(typeof(ImGuiHelper), "DrawComboWithTooltips", new[]
        {
            typeof(ImString),
            enumPlaceholder.MakeByRefType(),
            typeof(StringEnumCollection<>).MakeGenericType(enumPlaceholder),
            typeof(ReadOnlySpan<>).MakeGenericType(typeof(LString)),
            typeof(bool)
        });
        foreach (Type enumType in SettingsEnumLocalization.Types)
        {
            yield return combo.MakeGenericMethod(enumType);
            yield return comboWithTooltips.MakeGenericMethod(enumType);
        }
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        SettingsEnumLocalization.LocalizeNameCalls(codes);
        return codes;
    }
}
