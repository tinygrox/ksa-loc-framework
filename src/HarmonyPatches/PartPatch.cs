using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(Part))]
public static class PartPatch
{
    [HarmonyPatch("ShowContextMenu")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ShowContextMenuTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_Enter));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_ChutesOn));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_Decouple));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_DecouplerHeader));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_Electricity));
        return codes;
    }

    [HarmonyPatch("DrawTransferButton")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawTransferButtonTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_TransferOut));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_TransferIn));
        return codes;
    }

    [HarmonyPatch("OnDrawUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnDrawUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("ShowControlFromHereItem")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ShowControlFromHereItemTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_ControlFromHere));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_ControlFromHereConnector));
        return codes;
    }

    [HarmonyPatch("DrawPartInfo")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawPartInfoTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_TankHeader));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_GrainSegmentHeader));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_EngineCoreHeader));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Part_NozzleHeader));
        return codes;
    }
}
