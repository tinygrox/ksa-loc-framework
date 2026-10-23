using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(MapController))]
public static class MapControllerPatch
{
    [HarmonyPatch("OnDrawUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnDrawUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_Title));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_Viewing));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_Scope));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_OffsetX));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_OffsetY));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_OffsetZ));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_Overlays));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_BodyGridPlanes));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_AlignPlanes));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_Magnetospheres));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_VehicleSection));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_Release));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_MapMode));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_Take));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_CameraSection));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_Recenter));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_FocusVehicle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_MapControl));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_VehicleControl));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_BodyPlanesHint));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_AlignPlanesHint));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Map_MagnetospheresHint));
        return codes;
    }
}
