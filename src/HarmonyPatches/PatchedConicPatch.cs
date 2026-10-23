using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(PatchedConic))]
public static class PatchedConicPatch
{
    [HarmonyPatch("DrawPatchInfo")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawPatchInfoTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Fpl_Never));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Burn_None));
        return codes;
    }
}
