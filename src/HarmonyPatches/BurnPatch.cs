using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(Burn))]
public static class BurnPatch
{
    [HarmonyPatch("DrawUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_None));
        return codes;
    }

    [HarmonyPatch("DrawDeletePill")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawDeletePillTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawBurnEditorWindowContent")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawBurnEditorWindowContentTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_DeleteBurn));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_MoveToVehicleFlightPlan));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_MakeVehicleTransferFlightPlan));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_CurrentTarget));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_BurnRelativeInclination));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_RelativeInclination));
        return codes;
    }

    [HarmonyPatch("DrawBurnPatchWindow")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawBurnPatchWindowTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_PatchHeader));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_Patch));
        return codes;
    }

    [HarmonyPatch("DrawBurnInfo")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawBurnInfoTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_EditLabel));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_Edit));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_DeleteLabel));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_Delete));
        return codes;
    }
}
