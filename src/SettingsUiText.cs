using System;
using System.Collections.Generic;
using Brutal.Localization;

namespace KsaLocFramework;

public static class SettingsUiText
{
    private static readonly LString[] NavLabelTexts = new LString[]
    {
        GameStrings.KLF_SettingsTab_Display,
        GameStrings.KLF_SettingsTab_Graphics,
        GameStrings.KLF_SettingsTab_Audio,
        GameStrings.KLF_SettingsTab_Interface,
        GameStrings.KLF_SettingsTab_Controls,
        GameStrings.KLF_SettingsTab_Simulation,
        GameStrings.KLF_SettingsTab_Console,
        GameStrings.KLF_SettingsTab_Mods
    };

    private static readonly Dictionary<string, LString> TabNameTexts =
        new Dictionary<string, LString>(StringComparer.Ordinal)
        {
            ["Mods"] = GameStrings.KLF_Settings_ModsRegion
        };

    private static readonly Dictionary<string, LString> EnumNameTexts =
        new Dictionary<string, LString>(StringComparer.Ordinal)
        {
            ["Windowed"] = GameStrings.KLF_Settings_Enum_Windowed,
            ["Borderless"] = GameStrings.KLF_Settings_Enum_Borderless,
            ["Fullscreen"] = GameStrings.KLF_Settings_Enum_Fullscreen,
            ["Disabled"] = GameStrings.KLF_Settings_Enum_Disabled,
            ["Cmaa2"] = GameStrings.KLF_Settings_Enum_Cmaa2,
            ["Multisample X2"] = GameStrings.KLF_Settings_Enum_MultisampleX2,
            ["Multisample X4"] = GameStrings.KLF_Settings_Enum_MultisampleX4,
            ["Multisample X8"] = GameStrings.KLF_Settings_Enum_MultisampleX8,
            ["Unlimited"] = GameStrings.KLF_Settings_Enum_Unlimited,
            ["One Frame"] = GameStrings.KLF_Settings_Enum_OneFrame,
            ["Off"] = GameStrings.KLF_Settings_Enum_Off,
            ["Low"] = GameStrings.KLF_Settings_Enum_Low,
            ["Medium"] = GameStrings.KLF_Settings_Enum_Medium,
            ["High"] = GameStrings.KLF_Settings_Enum_High,
            ["Ultra"] = GameStrings.KLF_Settings_Enum_Ultra,
            ["Extreme"] = GameStrings.KLF_Settings_Enum_Extreme,
            ["Very Low"] = GameStrings.KLF_Settings_Enum_VeryLow,
            ["Hard"] = GameStrings.KLF_Settings_Enum_Hard,
            ["Pcf"] = GameStrings.KLF_Settings_Enum_Pcf,
            ["Pcss"] = GameStrings.KLF_Settings_Enum_Pcss,
            ["Uncharted"] = GameStrings.KLF_Settings_Enum_Uncharted,
            ["Agx"] = GameStrings.KLF_Settings_Enum_Agx,
            ["Aces Fitted"] = GameStrings.KLF_Settings_Enum_AcesFitted,
            ["Aces"] = GameStrings.KLF_Settings_Enum_Aces,
            ["Filmic"] = GameStrings.KLF_Settings_Enum_Filmic,
            ["Rom Bin Da House"] = GameStrings.KLF_Settings_Enum_RomBinDaHouse,
            ["Reinhard Simple"] = GameStrings.KLF_Settings_Enum_ReinhardSimple,
            ["Reinhard Luma"] = GameStrings.KLF_Settings_Enum_ReinhardLuma,
            ["Gran Turismo"] = GameStrings.KLF_Settings_Enum_GranTurismo,
            ["Linear"] = GameStrings.KLF_Settings_Enum_Linear,
            ["Hable"] = GameStrings.KLF_Settings_Enum_Hable
        };

    private static readonly Dictionary<string, LString> InputActionNameTexts =
        new Dictionary<string, LString>(StringComparer.Ordinal)
        {
            ["Forward"] = GameStrings.KLF_InputAction_Forward,
            ["Backward"] = GameStrings.KLF_InputAction_Backward,
            ["Left"] = GameStrings.KLF_InputAction_Left,
            ["Right"] = GameStrings.KLF_InputAction_Right,
            ["Roll Left"] = GameStrings.KLF_InputAction_RollLeft,
            ["Roll Right"] = GameStrings.KLF_InputAction_RollRight,
            ["Translate Forward"] = GameStrings.KLF_InputAction_TranslateForward,
            ["Translate Backward"] = GameStrings.KLF_InputAction_TranslateBackward,
            ["Translate Left"] = GameStrings.KLF_InputAction_TranslateLeft,
            ["Translate Right"] = GameStrings.KLF_InputAction_TranslateRight,
            ["Translate Up"] = GameStrings.KLF_InputAction_TranslateUp,
            ["Translate Down"] = GameStrings.KLF_InputAction_TranslateDown,
            ["Camera Up"] = GameStrings.KLF_InputAction_CameraUp,
            ["Camera Down"] = GameStrings.KLF_InputAction_CameraDown,
            ["Sprint"] = GameStrings.KLF_InputAction_Sprint,
            ["Toggle Ui"] = GameStrings.KLF_InputAction_ToggleUi,
            ["Camera Mode"] = GameStrings.KLF_InputAction_CameraMode,
            ["Sim Speed Increase"] = GameStrings.KLF_InputAction_SimSpeedIncrease,
            ["Sim Speed Decrease"] = GameStrings.KLF_InputAction_SimSpeedDecrease,
            ["Sim Speed Default"] = GameStrings.KLF_InputAction_SimSpeedDefault,
            ["Field Of View Increase"] = GameStrings.KLF_InputAction_FieldOfViewIncrease,
            ["Field Of View Decrease"] = GameStrings.KLF_InputAction_FieldOfViewDecrease,
            ["Cancel"] = GameStrings.KLF_InputAction_Cancel,
            ["Toggle Console"] = GameStrings.KLF_InputAction_ToggleConsole,
            ["Toggle Map"] = GameStrings.KLF_InputAction_ToggleMap,
            ["Open Game Settings"] = GameStrings.KLF_InputAction_OpenGameSettings,
            ["Camera Reference Frame"] = GameStrings.KLF_InputAction_CameraReferenceFrame,
            ["Vessel Next"] = GameStrings.KLF_InputAction_VesselNext,
            ["Vessel Previous"] = GameStrings.KLF_InputAction_VesselPrevious,
            ["Celestial Next"] = GameStrings.KLF_InputAction_CelestialNext,
            ["Celestial Previous"] = GameStrings.KLF_InputAction_CelestialPrevious,
            ["Toggle Fps"] = GameStrings.KLF_InputAction_ToggleFps,
            ["Toggle Stabilization"] = GameStrings.KLF_InputAction_ToggleStabilization,
            ["Toggle Manual Thrust Mode"] = GameStrings.KLF_InputAction_ToggleManualThrustMode,
            ["Toggle Transfer Plan"] = GameStrings.KLF_InputAction_ToggleTransferPlan,
            ["Toggle Flight Plan"] = GameStrings.KLF_InputAction_ToggleFlightPlan,
            ["Map Up"] = GameStrings.KLF_InputAction_MapUp,
            ["Map Down"] = GameStrings.KLF_InputAction_MapDown,
            ["Map Left"] = GameStrings.KLF_InputAction_MapLeft,
            ["Map Right"] = GameStrings.KLF_InputAction_MapRight,
            ["Map Forward"] = GameStrings.KLF_InputAction_MapForward,
            ["Map Backward"] = GameStrings.KLF_InputAction_MapBackward,
            ["Map Center"] = GameStrings.KLF_InputAction_MapCenter,
            ["Map Control Vehicle Toggle"] = GameStrings.KLF_InputAction_MapControlVehicleToggle,
            ["Toggle Ground Tracking"] = GameStrings.KLF_InputAction_ToggleGroundTracking,
            ["Main Engine Startup"] = GameStrings.KLF_InputAction_MainEngineStartup,
            ["Main Engine Shutdown"] = GameStrings.KLF_InputAction_MainEngineShutdown,
            ["Main Engine Throttle Up"] = GameStrings.KLF_InputAction_MainEngineThrottleUp,
            ["Main Engine Throttle Down"] = GameStrings.KLF_InputAction_MainEngineThrottleDown,
            ["Toggle Editor"] = GameStrings.KLF_InputAction_ToggleEditor,
            ["Toggle Resources"] = GameStrings.KLF_InputAction_ToggleResources,
            ["Toggle Save Menu"] = GameStrings.KLF_InputAction_ToggleSaveMenu,
            ["Editor Camera Focus"] = GameStrings.KLF_InputAction_EditorCameraFocus,
            ["Toggle Staging Window"] = GameStrings.KLF_InputAction_ToggleStagingWindow,
            ["Toggle Editor Part Window"] = GameStrings.KLF_InputAction_ToggleEditorPartWindow,
            ["I V A Switch To Next Seat"] = GameStrings.KLF_InputAction_IVASwitchToNextSeat,
            ["Toggle Thread Profiler"] = GameStrings.KLF_InputAction_ToggleThreadProfiler,
            ["Toggle Universe Manifest"] = GameStrings.KLF_InputAction_ToggleUniverseManifest,
            ["Toggle Target Tracking"] = GameStrings.KLF_InputAction_ToggleTargetTracking,
            ["Toggle Lights"] = GameStrings.KLF_InputAction_ToggleLights,
            ["Editor Part Pitch Down"] = GameStrings.KLF_InputAction_EditorPartPitchDown,
            ["Editor Part Pitch Up"] = GameStrings.KLF_InputAction_EditorPartPitchUp,
            ["Editor Yaw Part Left"] = GameStrings.KLF_InputAction_EditorYawPartLeft,
            ["Editor Yaw Part Right"] = GameStrings.KLF_InputAction_EditorYawPartRight,
            ["Editor Roll Part Left"] = GameStrings.KLF_InputAction_EditorRollPartLeft,
            ["Editor Roll Part Right"] = GameStrings.KLF_InputAction_EditorRollPartRight,
            ["Editor Reset Part Rotation"] = GameStrings.KLF_InputAction_EditorResetPartRotation,
            ["Editor Place Mode"] = GameStrings.KLF_InputAction_EditorPlaceMode,
            ["Editor Translate Mode"] = GameStrings.KLF_InputAction_EditorTranslateMode,
            ["Editor Rotate Mode"] = GameStrings.KLF_InputAction_EditorRotateMode,
            ["Editor Scale Mode"] = GameStrings.KLF_InputAction_EditorScaleMode,
            ["Editor Toggle Symmetry"] = GameStrings.KLF_InputAction_EditorToggleSymmetry,
            ["Editor Prev Symmetry Mode"] = GameStrings.KLF_InputAction_EditorPrevSymmetryMode,
            ["Editor Next Symmetry Mode"] = GameStrings.KLF_InputAction_EditorNextSymmetryMode,
            ["Editor Toggle Snapping"] = GameStrings.KLF_InputAction_EditorToggleSnapping,
            ["Editor Toggle Symmetry Inheritance"] = GameStrings.KLF_InputAction_EditorToggleSymmetryInheritance,
            ["Editor Delete"] = GameStrings.KLF_InputAction_EditorDelete,
            ["Editor Duplicate"] = GameStrings.KLF_InputAction_EditorDuplicate,
            ["Editor Toggle Free Camera"] = GameStrings.KLF_InputAction_EditorToggleFreeCamera,
            ["Editor Free Cam Up"] = GameStrings.KLF_InputAction_EditorFreeCamUp,
            ["Editor Free Cam Down"] = GameStrings.KLF_InputAction_EditorFreeCamDown,
            ["Editor Free Cam Fast"] = GameStrings.KLF_InputAction_EditorFreeCamFast,
            ["Editor Free Cam Slow"] = GameStrings.KLF_InputAction_EditorFreeCamSlow,
            ["Default H U D Layout"] = GameStrings.KLF_InputAction_DefaultHUDLayout,
            ["Previous H U D Layout"] = GameStrings.KLF_InputAction_PreviousHUDLayout,
            ["Next H U D Layout"] = GameStrings.KLF_InputAction_NextHUDLayout,
            ["H U D Layout0"] = GameStrings.KLF_InputAction_HUDLayout0,
            ["H U D Layout1"] = GameStrings.KLF_InputAction_HUDLayout1,
            ["H U D Layout2"] = GameStrings.KLF_InputAction_HUDLayout2,
            ["H U D Layout3"] = GameStrings.KLF_InputAction_HUDLayout3,
            ["H U D Layout4"] = GameStrings.KLF_InputAction_HUDLayout4,
            ["H U D Layout5"] = GameStrings.KLF_InputAction_HUDLayout5,
            ["H U D Layout6"] = GameStrings.KLF_InputAction_HUDLayout6,
            ["H U D Layout7"] = GameStrings.KLF_InputAction_HUDLayout7,
            ["H U D Layout8"] = GameStrings.KLF_InputAction_HUDLayout8,
            ["H U D Layout9"] = GameStrings.KLF_InputAction_HUDLayout9,
            ["Screenshot"] = GameStrings.KLF_InputAction_Screenshot,
            ["Toggle R C S"] = GameStrings.KLF_InputAction_ToggleRCS,
            ["Toggle Kitten Roster"] = GameStrings.KLF_InputAction_ToggleKittenRoster,
            ["Toggle Crew Assignment"] = GameStrings.KLF_InputAction_ToggleCrewAssignment,
            ["Grab"] = GameStrings.KLF_InputAction_Grab,
            ["Board"] = GameStrings.KLF_InputAction_Board
        };

    public static string[] NavRailLabels()
    {
        string[] labels = new string[NavLabelTexts.Length];
        for (int i = 0; i < NavLabelTexts.Length; i++)
        {
            labels[i] = NavLabelTexts[i].Format();
        }

        return labels;
    }

    public static string LocalizeTabName(string name)
    {
        if (name != null && TabNameTexts.TryGetValue(name, out LString text))
        {
            return text.Format();
        }

        return name;
    }

    public static string LocalizeEnumName(string name)
    {
        if (name != null && EnumNameTexts.TryGetValue(name, out LString text))
        {
            return text.Format();
        }

        return name;
    }

    public static string LocalizeInputActionName(string name)
    {
        if (name != null && InputActionNameTexts.TryGetValue(name, out LString text))
        {
            return text.Format();
        }

        return name;
    }
}
