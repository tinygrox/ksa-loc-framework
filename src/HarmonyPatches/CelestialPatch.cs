using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(Celestial))]
public static class CelestialPatch
{
    [HarmonyPatch("DrawCelestialWindowData")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawCelestialWindowDataTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_Mass));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_SphereOfInfluence));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_AxialTilt));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_Retrograde));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_No));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_Yes));
        return codes;
    }
}
