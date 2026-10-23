using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(ResourceGroupList))]
public static class ResourceGroupListPatch
{
    [HarmonyPatch("DrawResourceGroupContent")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawResourceGroupContentTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
