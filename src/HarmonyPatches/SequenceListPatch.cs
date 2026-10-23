using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(SequenceList))]
public static class SequenceListPatch
{
    [HarmonyPatch("DrawActionTooltip")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawActionTooltipTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawSequenceContent")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawSequenceContentTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Sequence_OnlyEmptyDelete));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Sequence_EnvironmentHint));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Sequence_TotalDv));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Sequence_Header));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Sequence_Vac));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Sequence_Atm));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Sequence_Isp));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Sequence_Twr));
        return codes;
    }
}
