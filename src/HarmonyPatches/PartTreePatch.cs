using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(PartTree))]
public static class PartTreePatch
{
    [HarmonyPatch("DrawResourceWindow")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawResourceWindowTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Common_Resources));
        return codes;
    }
}
