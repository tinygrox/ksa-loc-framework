using System;
using System.Collections.Generic;
using Brutal.Localization;
using KSA;

namespace KsaLocFramework;

public static class ClassNames
{
    private static readonly Dictionary<string, LString> Names = new Dictionary<string, LString>(StringComparer.Ordinal)
    {
        ["Star"] = GameStrings.KLF_Class_Star,
        ["StellarBody"] = GameStrings.KLF_Class_StellarBody,
        ["PlanetaryBody"] = GameStrings.KLF_Class_PlanetaryBody,
        ["TerrestrialBody"] = GameStrings.KLF_Class_TerrestrialBody,
        ["AtmosphericBody"] = GameStrings.KLF_Class_AtmosphericBody,
        ["Barycenter"] = GameStrings.KLF_Class_Barycenter,
        ["Planet"] = GameStrings.KLF_Class_Planet,
        ["Moon"] = GameStrings.KLF_Celestial_Moon,
        ["Asteroid"] = GameStrings.KLF_Class_Asteroid,
        ["Comet"] = GameStrings.KLF_Class_Comet,
        ["PeriodicComet"] = GameStrings.KLF_Class_PeriodicComet,
        ["InterstellarComet"] = GameStrings.KLF_Class_InterstellarComet,
        ["MinorBody"] = GameStrings.KLF_Class_MinorBody,
        ["City"] = GameStrings.KLF_Class_City,
        ["Crater"] = GameStrings.KLF_Class_Crater,
        ["Mountain"] = GameStrings.KLF_Class_Mountain,
        ["Landmark"] = GameStrings.KLF_Class_Landmark,
        ["Point of Interest"] = GameStrings.KLF_Class_PointOfInterest,
        ["Debris"] = GameStrings.KLF_Manifest_Debris,
        ["Vehicle"] = GameStrings.KLF_ConfigOnStartPopup_Vehicle,
        ["Elliptical"] = GameStrings.KLF_OrbitType_Elliptical,
        ["Parabolic"] = GameStrings.KLF_OrbitType_Parabolic,
        ["Hyperbolic"] = GameStrings.KLF_OrbitType_Hyperbolic,
        ["Invalid"] = GameStrings.KLF_OrbitType_Invalid,
        ["Maneuvering"] = GameStrings.KLF_Situation_Maneuvering,
        ["Freefall"] = GameStrings.KLF_Situation_Freefall,
        ["Rolling"] = GameStrings.KLF_Situation_Rolling,
        ["Landed"] = GameStrings.KLF_Situation_Landed,
        ["Sailing"] = GameStrings.KLF_Situation_Sailing,
        ["Floating"] = GameStrings.KLF_Situation_Floating,
        ["Dragging"] = GameStrings.KLF_Situation_Dragging,
        ["Bottomed"] = GameStrings.KLF_Situation_Bottomed
    };

    public static void Readout(ReadOnlySpan<char> key, ReadOnlySpan<char> value)
    {
        if (Names.TryGetValue(value.ToString(), out LString localized))
        {
            ConsoleWidgets.Readout(key, localized.Format());
            return;
        }

        ConsoleWidgets.Readout(key, value);
    }
}
