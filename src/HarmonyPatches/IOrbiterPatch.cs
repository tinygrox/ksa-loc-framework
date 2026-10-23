using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(IOrbiter))]
public static class IOrbiterPatch
{
    private static readonly MethodInfo ReadoutTarget =
        AccessTools.Method(typeof(ConsoleWidgets), "Readout", new[] { typeof(ReadOnlySpan<char>), typeof(ReadOnlySpan<char>) });

    private static readonly MethodInfo ReadoutReplacement =
        AccessTools.Method(typeof(ClassNames), nameof(ClassNames.Readout));

    [HarmonyPatch("OnDrawUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnDrawUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbiter_FocusHint));
        return codes;
    }

    [HarmonyPatch("DrawCelestialWindowData")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawCelestialWindowDataTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_TrueAnomaly));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_SemiMajorAxis));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_SemiMinorAxis));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_OrbitalPeriod));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_Inclination));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_Eccentricity));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_OrbitType));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_Unknown));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_Lan));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_Aop));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_Periapsis));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_Apoapsis));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_OrbitalSpeed));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_TimeAtPeriapsis));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_TimeSincePe));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Orbit_Period));
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
