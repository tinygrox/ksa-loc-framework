using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(Program))]
public static class ProgramMousePatch
{
    [HarmonyPatch("OnMouseButton")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnMouseButtonTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Program_RightClickDeparture));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Program_RightClickChained));
        return codes;
    }
}
