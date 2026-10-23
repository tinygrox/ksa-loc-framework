using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(TargetTrackWindow))]
public static class TargetTrackWindowPatch
{
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Track_TitleTargetTrack));
        return codes;
    }

    [HarmonyPatch("DrawMenuBar")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawMenuBarTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawAddBurnPopup")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawAddBurnPopupTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawContent")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawContentTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Track_Altitude));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Track_Along));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Track_Alt));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Track_Cross));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Menu_TopDown));
        return codes;
    }

    [HarmonyPatch("DrawPanel")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawPanelTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Track_Closest));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Track_Sep));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Track_Scale));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Track_Altitude));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Menu_TopDown));
        return codes;
    }

    [HarmonyPatch("DrawBurnEditPopup")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawBurnEditPopupTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Track_Burn));
        return codes;
    }
}
