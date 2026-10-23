using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(UncompressedVehicleSave))]
public static class UncompressedVehicleSavePatch
{
    [HarmonyPatch("DrawInTable")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawInTableTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
