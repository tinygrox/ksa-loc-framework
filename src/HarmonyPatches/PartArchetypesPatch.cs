using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(PartArchetypes))]
public static class PartArchetypesPatch
{
    [HarmonyPatch("AppendMass")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> AppendMassTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_Mass));
        return codes;
    }

    [HarmonyPatch("AppendVolume")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> AppendVolumeTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_Volume));
        return codes;
    }

    [HarmonyPatch("AppendPowerGenerated")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> AppendPowerGeneratedTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_PowerGenerated));
        return codes;
    }

    [HarmonyPatch("AppendCharge")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> AppendChargeTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_BatteryCapacity));
        return codes;
    }

    [HarmonyPatch("AppendPowerConsumed")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> AppendPowerConsumedTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_PowerUsed));
        return codes;
    }

    [HarmonyPatch("AppendThrust")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> AppendThrustTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_Engine));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_PerThruster));
        return codes;
    }

    [HarmonyPatch("AppendThrustBlock")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> AppendThrustBlockTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_ThrustSeaLevel));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_ThrustVacuum));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_IspSeaLevel));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_IspVacuum));
        return codes;
    }

    [HarmonyPatch("AppendPropellantBranch")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> AppendPropellantBranchTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_Propellant));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_ListSeparator));
        return codes;
    }

    [HarmonyPatch("AppendTankRoleAffinity")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> AppendTankRoleAffinityTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_RoleAffinity));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_Engine));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_Thruster));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_ListSeparator));
        return codes;
    }

    [HarmonyPatch("AppendDecouplerForce")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> AppendDecouplerForceTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartArchetypes_DecouplerForce));
        return codes;
    }
}
