using System.Collections.Generic;
using System.Reflection;
using Core;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch]
public static class MassReferencePatch
{
    private static MethodBase TargetMethod()
    {
        return AccessTools.Method(
            typeof(MassReference),
            "ToNearest",
            new[] { typeof(double), typeof(SpanBuilder).MakeByRefType(), typeof(int) });
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Mass_EarthMasses));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Mass_JupiterMasses));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Mass_SolarMasses));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_OrbitType_Invalid));
        return codes;
    }
}
