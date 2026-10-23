using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(FuelLinkList))]
public static class FuelLinkListPatch
{
    [HarmonyPatch("DrawFuelLinksSection")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawFuelLinksSectionTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
