using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(Astronomical))]
public static class AstronomicalPatch
{
    private static readonly MethodInfo ReadoutTarget =
        AccessTools.Method(typeof(ConsoleWidgets), "Readout", new[] { typeof(ReadOnlySpan<char>), typeof(ReadOnlySpan<char>) });

    private static readonly MethodInfo ReadoutReplacement =
        AccessTools.Method(typeof(ClassNames), nameof(ClassNames.Readout));

    [HarmonyPatch("DrawCelestialWindowData")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawCelestialWindowDataTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_Class));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_Moon));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_Radius));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_Atmosphere));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_No));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_Yes));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_AtmosphereHeight));
        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].operand is MethodInfo method && method == ReadoutTarget)
            {
                codes[i].operand = ReadoutReplacement;
            }
        }

        return codes;
    }
}
