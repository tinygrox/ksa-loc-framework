using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Brutal.ImGuiApi;
using Brutal.Localization;
using Core;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(VehicleEditor))]
public static class VehicleEditorPatch
{
    [HarmonyPatch(MethodType.StaticConstructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> StaticConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_GizmoPlace));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_GizmoTranslate));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_GizmoRotate));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_GizmoScale));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_GizmoFrameAsmb));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_GizmoFrameLocal));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_SymmetryNone));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_LabelInherit));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_LabelSnap));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_CameraFree));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_CameraOrbit));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_ProjectionOrtho));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_ProjectionPersp));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_SnapTop));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_SnapBottom));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_SnapRight));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_SnapLeft));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_SnapBack));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_SnapFront));
        return codes;
    }

    [HarmonyPatch("DrawGizmoToolbarButtons")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawGizmoToolbarButtonsTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_FrameAsmbHint));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_FrameLocalHint));
        TranspilerTools.ReplaceLocalizedSpanHint(codes, nameof(GameStrings.KLF_VehicleEditor_PlaceMode));
        TranspilerTools.ReplaceLocalizedSpanHint(codes, nameof(GameStrings.KLF_VehicleEditor_TranslateMode));
        TranspilerTools.ReplaceLocalizedSpanHint(codes, nameof(GameStrings.KLF_VehicleEditor_RotateMode));
        TranspilerTools.ReplaceLocalizedSpanHint(codes, nameof(GameStrings.KLF_VehicleEditor_ScaleMode));
        TranspilerTools.ReplaceLocalizedSpanHint(codes, nameof(GameStrings.KLF_VehicleEditor_ReferenceFrame));
        return codes;
    }

    [HarmonyPatch("DrawSymmetryToolbarButtons")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawSymmetryToolbarButtonsTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, "Disabled [", nameof(GameStrings.KLF_VehicleEditor_Disabled));
        TranspilerTools.ReplaceLocalizedLiteral(codes, "Enabled [", nameof(GameStrings.KLF_VehicleEditor_Enabled));
        TranspilerTools.ReplaceLocalizedSpanStateHint(codes, nameof(GameStrings.KLF_VehicleEditor_Symmetry));
        TranspilerTools.ReplaceLocalizedSpanStateHint(codes, nameof(GameStrings.KLF_VehicleEditor_SymmetryInheritance));
        TranspilerTools.ReplaceLocalizedSpanStateHint(codes, nameof(GameStrings.KLF_VehicleEditor_AngleSnapping));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_Bin));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_BinDragHint));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_DropToDelete));
        return codes;
    }

    [HarmonyPatch("DrawTransformMenu")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawTransformMenuTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawLaunchUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawLaunchUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_LaunchTitle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_CrewAssignment));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_LaunchVehicle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_NoCrewAssigned));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_NoCrewMessage));
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("OnDrawUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnDrawUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_CameraFreeHint));
        TranspilerTools.ReplaceLocalizedSpanHint(codes, nameof(GameStrings.KLF_VehicleEditor_CameraOrbitHint));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_ProjectionOrthoHint));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_ProjectionPerspHint));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_ModeTranslate));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_ModeRotate));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_ModeScale));
        TranspilerTools.ReplaceLocalizedShortConcatenation(codes, nameof(GameStrings.KLF_VehicleEditor_SelectPart));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_EscMenu_NewVehicle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_SwitchVehicle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartTreeSelectionPopup_ActiveVehicleDescription));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_NewVehicleTitle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_UI_Cancel));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_ClearAndCreate));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_UnsavedVehicles));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_Save));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_SaveVehicle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_NewVehicleAnyway));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_ExitAnyway));
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("GetTreeLabel")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> GetTreeLabelTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedShortConcatenation(codes, nameof(GameStrings.KLF_VehicleEditor_ExistingVehicle));
        return codes;
    }

    [HarmonyPatch("DrawPartUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawPartUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_Delete));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_Save));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_SaveVehicle));
        TranspilerTools.ReplaceLocalizedLabelBuilder(codes, nameof(GameStrings.KLF_VehicleEditor_AddPrefix));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_DecouplerPrefix));
        TranspilerTools.ReplaceLocalizedCountLabel(codes, nameof(GameStrings.KLF_VehicleEditor_TankCount), " tank");
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_TankPrefix));
        TranspilerTools.ReplaceLocalizedShortConcatenation(codes, nameof(GameStrings.KLF_VehicleEditor_SeatPrefix));
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawTankGroupControls")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawTankGroupControlsTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_Auto));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_Manual));
        TranspilerTools.ReplaceLocalizedConcatenation(codes, nameof(GameStrings.KLF_VehicleEditor_ContentsNone));
        TranspilerTools.ReplaceLocalizedTwoValueConcatenation(codes, nameof(GameStrings.KLF_VehicleEditor_ContentsAssigned));
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawTankGroupFillSliders")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawTankGroupFillSlidersTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLabelBuilder(codes, nameof(GameStrings.KLF_VehicleEditor_VolumePrefix));
        return codes;
    }

    [HarmonyPatch("TryGetFuelPairingWarning")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> TryGetFuelPairingWarningTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_FuelLineEmptyDonor));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_FuelLineMixMismatch));
        return codes;
    }

    [HarmonyPatch("TryCompleteFuelLinePairing")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> TryCompleteFuelLinePairingTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_FuelLineSameVehicle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_FuelLineSamePart));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_FuelLineDuplicate));
        TranspilerTools.ReplaceLocalizedInterpolatedAlert(codes, nameof(GameStrings.KLF_VehicleEditor_FuelLineTooLongMessage));
        return codes;
    }

    [HarmonyPatch("TrySpawnPortAndCompletePairing")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> TrySpawnPortAndCompletePairingTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_NoFuelPort));
        TranspilerTools.ReplaceLocalizedInterpolatedAlert(codes, nameof(GameStrings.KLF_VehicleEditor_FuelLineTooLongMessage));
        return codes;
    }

    [HarmonyPatch("DrawGrainCombo")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawGrainComboTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_Mixed));
        TranspilerTools.ReplaceLocalizedTwoValueConcatenation(codes, nameof(GameStrings.KLF_VehicleEditor_GrainCrossSection));
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawGrainReaction")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawGrainReactionTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_NoMotor));
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawNozzleExpansionSection")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawNozzleExpansionSectionTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLabelBuilder(codes, nameof(GameStrings.KLF_VehicleEditor_NozzleOn));
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawGrainSection")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawGrainSectionTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedCountLabel(codes, nameof(GameStrings.KLF_VehicleEditor_SegmentsInStack));
        TranspilerTools.ReplaceLocalizedLabelBuilder(codes, nameof(GameStrings.KLF_VehicleEditor_Segment));
        TranspilerTools.ReplaceLocalizedLabelBuilder(codes, nameof(GameStrings.KLF_VehicleEditor_StackInvalid));
        TranspilerTools.ReplaceLocalizedLabelBuilder(codes, nameof(GameStrings.KLF_VehicleEditor_WontIgnite));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_TimePrefix));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_TimeThrust));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_IspLine));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_TimeChamber));
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawParachuteSection")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawParachuteSectionTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_ChutesOn));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_Off));
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

}

[HarmonyPatch]
public static class VehicleEditorPartWindowPatch
{
    [HarmonyTargetMethod]
    public static MethodBase OnDrawUiTarget()
    {
        return AccessTools.Method(AccessTools.Inner(typeof(VehicleEditor), "PartWindow"), "OnDrawUi");
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnDrawUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_All));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_AllSizes));
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
