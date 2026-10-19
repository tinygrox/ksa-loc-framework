using Brutal.Localization;

namespace KsaLocFramework;

public static class GameStrings
{
    public static readonly LString KLF_UI_Language = LStrings.Create("KLF_UI_Language", "Language");
    public static readonly LString KLF_UI_Confirm = LStrings.Create("KLF_UI_Confirm", "CONFIRM");
    public static readonly LString KLF_UI_Cancel = LStrings.Create("KLF_UI_Cancel", "CANCEL");
    public static readonly LString KLF_UI_Okay = LStrings.Create("KLF_UI_Okay", "OKAY");
    public static readonly LString KLF_UI_AlwaysShow = LStrings.Create("KLF_UI_AlwaysShow", "Always Show");
    public static readonly LString KLF_UI_No = LStrings.Create("KLF_UI_No", "No");
    public static readonly LString KLF_WelcomePopup_Title = LStrings.Create("KLF_WelcomePopup_Title", "WELCOME");
    public static readonly LString KLF_WelcomePopup_Text =
        LStrings.Create("KLF_WelcomePopup_Text", "Welcome to the KSA pre-alpha.<Separator/>The game currently has you controlling a craft named 'Rocket'. Press <Action:VesselNext />, <Action:VesselPrevious/> to swap between several available craft.<NewLine/>You can press <Action:CameraReferenceFrame/> to switch between the camera modes, or hold right click to pan the camera around. Most UI elements can be repositioned, resized and hidden, by clicking and dragging.<NewLine/>KSA uses fairly traditional craft controls, which can be viewed in the 'Controls' section of the settings menus by pressing <Action:OpenGameSettings/>.<NewLine/>Press <Action:ToggleMap/> to toggle between craft view, and map view. The map view can be useful for planning burns for encounters and exploring the solar system, but controls slightly change while in this mode. You can also use this to prepare burns by left clicking on a point in the orbit.<NewLine/>In the menu bar, you can use 'Universe' -> 'Goto' and you can instantly move to other celestial objects as well as using 'View' -> 'Orbit Lines' to enable or disable orbit lines for the many available celestials. Many other in progress features can be accessed by exploring the menu bar.<NewLine/>Time compression can be increased using <Action:SimSpeedIncrease/> and decreased with <Action:SimSpeedDecrease/>. Ground Tracking can be toggled with <Action:ToggleGroundTracking/> and transfer planning with <Action:ToggleTransferPlan/>.<Separator/>");
    public static readonly LString KLF_AbandonPopup_Title = LStrings.Create("KLF_AbandonPopup_Title", "ABANDON VEHICLE");
    public static readonly LString KLF_AbandonPopup_Body =
        LStrings.Create("KLF_AbandonPopup_Body", "Are you sure you want to abandon this vehicle?");
    public static readonly LString KLF_ExitPopup_Title = LStrings.Create("KLF_ExitPopup_Title", "EXIT");
    public static readonly LString KLF_ExitPopup_Body =
        LStrings.Create("KLF_ExitPopup_Body", "Are you sure you want to exit the game?");
    public static readonly LString KLF_ConfigOnStartPopup_Title = LStrings.Create("KLF_ConfigOnStartPopup_Title", "CONFIGURATION");
    public static readonly LString KLF_ConfigOnStartPopup_VramMessage =
        LStrings.Create("KLF_ConfigOnStartPopup_VramMessage", "Some systems may require more Video Memory (VRAM) than others due to texture size for color and heightmap data, and may not be available or crash the game if you do not have enough VRAM available.");
    public static readonly LString KLF_ConfigOnStartPopup_VramUsagePrefix =
        LStrings.Create("KLF_ConfigOnStartPopup_VramUsagePrefix", " Reported usage and budget: ~");
    public static readonly LString KLF_ConfigOnStartPopup_Start = LStrings.Create("KLF_ConfigOnStartPopup_Start", "Start KSA");
    public static readonly LString KLF_ConfigOnStartPopup_SelectSystem = LStrings.Create("KLF_ConfigOnStartPopup_SelectSystem", "Select System");
    public static readonly LString KLF_ConfigOnStartPopup_System = LStrings.Create("KLF_ConfigOnStartPopup_System", "System");
    public static readonly LString KLF_ConfigOnStartPopup_GameType = LStrings.Create("KLF_ConfigOnStartPopup_GameType", "Game Type");
    public static readonly LString KLF_ConfigOnStartPopup_Type = LStrings.Create("KLF_ConfigOnStartPopup_Type", "Type");
    public static readonly LString KLF_ConfigOnStartPopup_StartingSituation = LStrings.Create("KLF_ConfigOnStartPopup_StartingSituation", "Starting Situation");
    public static readonly LString KLF_ConfigOnStartPopup_Vehicle = LStrings.Create("KLF_ConfigOnStartPopup_Vehicle", "Vehicle");
    public static readonly LString KLF_ConfigOnStartPopup_Location = LStrings.Create("KLF_ConfigOnStartPopup_Location", "Location");
    public static readonly LString KLF_ConfigOnStartPopup_Vessels = LStrings.Create("KLF_ConfigOnStartPopup_Vessels", "Vessels");
    public static readonly LString KLF_ConfigOnStartPopup_Textures = LStrings.Create("KLF_ConfigOnStartPopup_Textures", "Textures");
    public static readonly LString KLF_ConfigOnStartPopup_Terrain = LStrings.Create("KLF_ConfigOnStartPopup_Terrain", "Terrain");
    public static readonly LString KLF_ConfigOnStartPopup_Shadows = LStrings.Create("KLF_ConfigOnStartPopup_Shadows", "Shadows");
    public static readonly LString KLF_ConfigOnStartPopup_TextureStreaming = LStrings.Create("KLF_ConfigOnStartPopup_TextureStreaming", "Texture Streaming");
    public static readonly LString KLF_ConfigOnStartPopup_Enabled = LStrings.Create("KLF_ConfigOnStartPopup_Enabled", "Enabled");
    public static readonly LString KLF_ConfigOnStartPopup_CascadedShadows = LStrings.Create("KLF_ConfigOnStartPopup_CascadedShadows", "Cascaded Shadows");
    public static readonly LString KLF_ConfigOnStartPopup_Filter = LStrings.Create("KLF_ConfigOnStartPopup_Filter", "Filter");
    public static readonly LString KLF_ConfigOnStartPopup_LightSystem = LStrings.Create("KLF_ConfigOnStartPopup_LightSystem", "Light System");
    public static readonly LString KLF_ConfigOnStartPopup_EnableShadows = LStrings.Create("KLF_ConfigOnStartPopup_EnableShadows", "Enable Shadows");
    public static readonly LString KLF_ConfigOnStartPopup_Casters = LStrings.Create("KLF_ConfigOnStartPopup_Casters", "Casters");
    public static readonly LString KLF_ConfigOnStartPopup_Thumbnails = LStrings.Create("KLF_ConfigOnStartPopup_Thumbnails", "Thumbnails");
    public static readonly LString KLF_ConfigOnStartPopup_Parts = LStrings.Create("KLF_ConfigOnStartPopup_Parts", "Parts");
    public static readonly LString KLF_ConfirmModPopup_Title = LStrings.Create("KLF_ConfirmModPopup_Title", "MOD FOUND");
    public static readonly LString KLF_ConfirmModPopup_Body =
        LStrings.Create("KLF_ConfirmModPopup_Body", "A new mod with id '{Id}' has been found, do you want it enabled?");
    public static readonly LString KLF_ConfirmModPopup_Enable = LStrings.Create("KLF_ConfirmModPopup_Enable", "Enable");
    public static readonly LString KLF_Settings_Title = LStrings.Create("KLF_Settings_Title", "SETTINGS");
    public static readonly LString KLF_Settings_Output = LStrings.Create("KLF_Settings_Output", "OUTPUT");
    public static readonly LString KLF_Settings_CameraAndFrame = LStrings.Create("KLF_Settings_CameraAndFrame", "CAMERA AND FRAME");
    public static readonly LString KLF_Settings_Display = LStrings.Create("KLF_Settings_Display", "DISPLAY");
    public static readonly LString KLF_Settings_Unlimited = LStrings.Create("KLF_Settings_Unlimited", "Unlimited");
    public static readonly LString KLF_Settings_Fps = LStrings.Create("KLF_Settings_Fps", " FPS");
    public static readonly LString KLF_Settings_Apply = LStrings.Create("KLF_Settings_Apply", "APPLY");
    public static readonly LString KLF_Settings_Reset = LStrings.Create("KLF_Settings_Reset", "RESET");
    public static readonly LString KLF_Settings_Discard = LStrings.Create("KLF_Settings_Discard", "DISCARD");
    public static readonly LString KLF_Settings_ResetKeys = LStrings.Create("KLF_Settings_ResetKeys", "RESET KEYS");
    public static readonly LString KLF_Settings_Close = LStrings.Create("KLF_Settings_Close", "CLOSE");
    public static readonly LString KLF_Settings_NoPendingChanges = LStrings.Create("KLF_Settings_NoPendingChanges", "NO PENDING CHANGES");
    public static readonly LString KLF_Settings_TabPending = LStrings.Create("KLF_Settings_TabPending", " TAB PENDING");
    public static readonly LString KLF_Settings_TabsPending = LStrings.Create("KLF_Settings_TabsPending", " TABS PENDING");
    public static readonly LString KLF_Settings_Pending = LStrings.Create("KLF_Settings_Pending", " PENDING");
    public static readonly LString KLF_Settings_RestartRequired = LStrings.Create("KLF_Settings_RestartRequired", "RESTART REQUIRED ON APPLY");
    public static readonly LString KLF_SettingsTab_Display = LStrings.Create("KLF_SettingsTab_Display", "DISPLAY");
    public static readonly LString KLF_SettingsTab_Graphics = LStrings.Create("KLF_SettingsTab_Graphics", "GRAPHICS");
    public static readonly LString KLF_SettingsTab_Audio = LStrings.Create("KLF_SettingsTab_Audio", "AUDIO");
    public static readonly LString KLF_SettingsTab_Interface = LStrings.Create("KLF_SettingsTab_Interface", "INTERFACE");
    public static readonly LString KLF_SettingsTab_Controls = LStrings.Create("KLF_SettingsTab_Controls", "CONTROLS");
    public static readonly LString KLF_SettingsTab_Simulation = LStrings.Create("KLF_SettingsTab_Simulation", "SIMULATION");
    public static readonly LString KLF_SettingsTab_Console = LStrings.Create("KLF_SettingsTab_Console", "CONSOLE");
    public static readonly LString KLF_SettingsTab_Mods = LStrings.Create("KLF_SettingsTab_Mods", "MODS");
    public static readonly LString KLF_Settings_ModsRegion = LStrings.Create("KLF_Settings_ModsRegion", "Mods");
    public static readonly LString KLF_Settings_Enum_Windowed = LStrings.Create("KLF_Settings_Enum_Windowed", "Windowed");
    public static readonly LString KLF_Settings_Enum_Borderless = LStrings.Create("KLF_Settings_Enum_Borderless", "Borderless");
    public static readonly LString KLF_Settings_Enum_Fullscreen = LStrings.Create("KLF_Settings_Enum_Fullscreen", "Fullscreen");
    public static readonly LString KLF_Settings_Enum_Disabled = LStrings.Create("KLF_Settings_Enum_Disabled", "Disabled");
    public static readonly LString KLF_Settings_Enum_Cmaa2 = LStrings.Create("KLF_Settings_Enum_Cmaa2", "Cmaa2");
    public static readonly LString KLF_Settings_Enum_MultisampleX2 = LStrings.Create("KLF_Settings_Enum_MultisampleX2", "Multisample X2");
    public static readonly LString KLF_Settings_Enum_MultisampleX4 = LStrings.Create("KLF_Settings_Enum_MultisampleX4", "Multisample X4");
    public static readonly LString KLF_Settings_Enum_MultisampleX8 = LStrings.Create("KLF_Settings_Enum_MultisampleX8", "Multisample X8");
    public static readonly LString KLF_Settings_Enum_Unlimited = LStrings.Create("KLF_Settings_Enum_Unlimited", "Unlimited");
    public static readonly LString KLF_Settings_Enum_OneFrame = LStrings.Create("KLF_Settings_Enum_OneFrame", "One Frame");
    public static readonly LString KLF_Settings_Enum_Off = LStrings.Create("KLF_Settings_Enum_Off", "Off");
    public static readonly LString KLF_Settings_Enum_Low = LStrings.Create("KLF_Settings_Enum_Low", "Low");
    public static readonly LString KLF_Settings_Enum_Medium = LStrings.Create("KLF_Settings_Enum_Medium", "Medium");
    public static readonly LString KLF_Settings_Enum_High = LStrings.Create("KLF_Settings_Enum_High", "High");
    public static readonly LString KLF_Settings_Enum_Ultra = LStrings.Create("KLF_Settings_Enum_Ultra", "Ultra");
    public static readonly LString KLF_Settings_Enum_Extreme = LStrings.Create("KLF_Settings_Enum_Extreme", "Extreme");
    public static readonly LString KLF_Settings_Enum_VeryLow = LStrings.Create("KLF_Settings_Enum_VeryLow", "Very Low");
    public static readonly LString KLF_Settings_Enum_Hard = LStrings.Create("KLF_Settings_Enum_Hard", "Hard");
    public static readonly LString KLF_Settings_Enum_Pcf = LStrings.Create("KLF_Settings_Enum_Pcf", "Pcf");
    public static readonly LString KLF_Settings_Enum_Pcss = LStrings.Create("KLF_Settings_Enum_Pcss", "Pcss");
    public static readonly LString KLF_Settings_Enum_Uncharted = LStrings.Create("KLF_Settings_Enum_Uncharted", "Uncharted");
    public static readonly LString KLF_Settings_Enum_Agx = LStrings.Create("KLF_Settings_Enum_Agx", "Agx");
    public static readonly LString KLF_Settings_Enum_AcesFitted = LStrings.Create("KLF_Settings_Enum_AcesFitted", "Aces Fitted");
    public static readonly LString KLF_Settings_Enum_Aces = LStrings.Create("KLF_Settings_Enum_Aces", "Aces");
    public static readonly LString KLF_Settings_Enum_Filmic = LStrings.Create("KLF_Settings_Enum_Filmic", "Filmic");
    public static readonly LString KLF_Settings_Enum_RomBinDaHouse = LStrings.Create("KLF_Settings_Enum_RomBinDaHouse", "Rom Bin Da House");
    public static readonly LString KLF_Settings_Enum_ReinhardSimple = LStrings.Create("KLF_Settings_Enum_ReinhardSimple", "Reinhard Simple");
    public static readonly LString KLF_Settings_Enum_ReinhardLuma = LStrings.Create("KLF_Settings_Enum_ReinhardLuma", "Reinhard Luma");
    public static readonly LString KLF_Settings_Enum_GranTurismo = LStrings.Create("KLF_Settings_Enum_GranTurismo", "Gran Turismo");
    public static readonly LString KLF_Settings_Enum_Linear = LStrings.Create("KLF_Settings_Enum_Linear", "Linear");
    public static readonly LString KLF_Settings_Enum_Hable = LStrings.Create("KLF_Settings_Enum_Hable", "Hable");
}
