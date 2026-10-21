using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(UncompressedVehicleSave))]
public static class UncompressedVehicleSavePatch
{
    [HarmonyPatch("DrawInTable", new[] { typeof(IViewport) })]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawInTableTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartTreeSelectionPopup_Description));
        return codes;
    }
}
