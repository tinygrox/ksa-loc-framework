using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(VehicleEditingSpace))]
public static class VehicleEditingSpacePatch
{
    [HarmonyPatch(MethodType.StaticConstructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> StaticConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditingSpace_AutoGroups));
        return codes;
    }

    [HarmonyPatch("DrawStageWindow")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawStageWindowTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditingSpace_RefillConsumables));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditingSpace_Resources));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditingSpace_RefillFullHint));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditingSpace_RefillHint));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditingSpace_AutoGroupHint));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditingSpace_OpenGroupsHint));
        return codes;
    }
}
