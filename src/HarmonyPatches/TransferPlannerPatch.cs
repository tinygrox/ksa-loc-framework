using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(TransferPlanner))]
public static class TransferPlannerPatch
{
    [HarmonyPatch(MethodType.StaticConstructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> StaticConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, "Hohmann", nameof(GameStrings.KLF_TransferPlanner_TransferTypeHohmann), 1);
        TranspilerTools.ReplaceLocalizedLiteral(codes, "Circularize Apoapsis", nameof(GameStrings.KLF_TransferPlanner_TransferTypeCircularizeApoapsis), 1);
        TranspilerTools.ReplaceLocalizedLiteral(codes, "Circularize Periapsis", nameof(GameStrings.KLF_TransferPlanner_TransferTypeCircularizePeriapsis), 1);
        TranspilerTools.ReplaceLocalizedLiteral(codes, "Interstellar", nameof(GameStrings.KLF_TransferPlanner_TransferTypeInterstellar), 1);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TimeUnitSeconds));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TimeUnitMinutes));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TimeUnitHours));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TimeUnitDays));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TimeUnitWeeks));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TimeUnitYears));
        return codes;
    }

    [HarmonyPatch("DrawPlanWindow")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawPlanWindowTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Title));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Plan));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_PlanType));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Source));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Destination));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NoneAvailable));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TransferWindow));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TimeOfFlight));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TimeUnit));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_MinTimeOfFlight));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Threads));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_MaxTimeOfFlight));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_SetTarget));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_ShowAlignment));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_MustHaveValidOrbit));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Calculate));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Recalculate));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NoSourceVehicle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NoValidApsis));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_BestTransfer));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_SelectBestDv));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_SelectBestDvRelSpeed));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_SelectedTransfer));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NoSolution));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_DepartureFlightTimeDv));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_PreviewSelected));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_PreviewSelectedFlightPlan));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_PreviewLambert));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Calculating));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TransferSelected));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NoSelection));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Create));
        return codes;
    }

    [HarmonyPatch("DrawTransferSummary")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawTransferSummaryTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Dv));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Depart));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Transit));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_InterceptVelocity));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_ClosestApproach));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_None));
        return codes;
    }

    [HarmonyPatch("DrawInterstellarBurnsSummary")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawInterstellarBurnsSummaryTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_SpeedUpBurnIn));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_SlowDownBurnIn));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_ArrivesIn));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_ArrivalPeriapsis));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_PlannedSpeedUpBurn));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_PlannedSlowDownBurn));
        return codes;
    }

    [HarmonyPatch("DrawInterstellarPlan")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawInterstellarPlanTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Destination));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NoSourceVehicle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NoStarNearby));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NoOtherStar));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Departing));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Crossing));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Distance));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NoAntimatterDrive));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_MaxSpeed));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_DepartureAntimatter));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_BrakeAntimatter));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_FinalSpeed));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_CircularSpeedAtPeriapsis));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TimeUnit));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_SpeedUpBurn));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Coast));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_CoastTime));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_HydrogenScooped));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_SlowDownBurn));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_BrakeOutOfAntimatter));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NotEnoughRoom));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Journey));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TimeOfFlight));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_MustDepartFromBodyOrbit));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_MustHaveValidOrbit));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NoDepartureFound));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Calculate));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Recalculate));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_SpeedUpBurnGoesNowhere));
        return codes;
    }

    [HarmonyPatch("DrawInterstellarBurnFuel")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawInterstellarBurnFuelTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_BurnTime));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_EndsAt));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_AntimatterUsed));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_HydrogenUsed));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Halfway));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_VesselBurnedAway));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TargetSpeed));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_SpeedAfter));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Antimatter));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Hydrogen));
        return codes;
    }

    [HarmonyPatch("DrawTargetPeriapsisRow")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawTargetPeriapsisRowTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TargetPeriapsis));
        return codes;
    }

    [HarmonyPatch("DrawInterstellarCorrection")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawInterstellarCorrectionTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_ApproachCorrection));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Arriving));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_ClosestApproach));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_ArrivesIn));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NoCorrectionFound));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Calculate));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Recalculate));
        return codes;
    }

    [HarmonyPatch("DrawCorrectionTransfer")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawCorrectionTransferTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_CalculatingCorrection));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Periapsis));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Correction));
        return codes;
    }

    [HarmonyPatch("DrawSelectedTransferFlightPlan")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawSelectedTransferFlightPlanTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_SelectedTransfer));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Leaving));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_AfterSlowingDown));
        return codes;
    }

    [HarmonyPatch("DrawPorkChopToolTip")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawPorkChopToolTipTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_TransferPoint));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Dv));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Time));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Transit));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_RelativeVelocity));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Closest));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_None));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Position));
        return codes;
    }

    [HarmonyPatch("DrawCalculatingSegmentBars")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawCalculatingSegmentBarsTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Calculating));
        return codes;
    }

    [HarmonyPatch("DrawDeltaVShortfall")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawDeltaVShortfallTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NotEnoughDeltaV));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_DeltaVShortUnit));
        return codes;
    }

    [HarmonyPatch("DrawPropellantShortfalls")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawPropellantShortfallsTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_NotEnough));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_ShortfallSeparator));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_TransferPlanner_Short));
        return codes;
    }
}
