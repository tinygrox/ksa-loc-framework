using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(Vehicle))]
public static class VehiclePatch
{
    private static readonly MethodInfo ReadoutTarget =
        AccessTools.Method(typeof(ConsoleWidgets), "Readout", new[] { typeof(ReadOnlySpan<char>), typeof(ReadOnlySpan<char>) });

    private static readonly MethodInfo ReadoutReplacement =
        AccessTools.Method(typeof(ClassNames), nameof(ClassNames.Readout));

    [HarmonyPatch("DrawPatchWindow")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawPatchWindowTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Fpl_Title));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Fpl_AllPlansExpire));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Fpl_AllPlansUpdateIn));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Fpl_MainExpires));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Fpl_Never));
        return codes;
    }

    [HarmonyPatch("DrawCelestialWindowData")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawCelestialWindowDataTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Celestial_Class));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleInfo_Total));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleInfo_Situation));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleInfo_DynamicPressure));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleInfo_GForce));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleInfo_Gravitation));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleInfo_PropellantMass));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleInfo_NearbyVehicle));
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
