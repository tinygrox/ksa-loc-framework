using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(GroundTrackWindow))]
public static class GroundTrackWindowPatch
{
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Track_TitleGroundTrack));
        return codes;
    }

    [HarmonyPatch("DrawViewMenu")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawViewMenuTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        SettingsEnumLocalization.LocalizeNameCalls(codes);
        return codes;
    }

    [HarmonyPatch("DrawContent")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawContentTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
