using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(LayoutSaves))]
public static class LayoutSavesPatch
{
    [HarmonyPatch("DrawMenu")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawMenuTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
