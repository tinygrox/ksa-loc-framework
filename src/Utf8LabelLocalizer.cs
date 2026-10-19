using System;
using System.Collections.Generic;
using System.Text;
using Brutal.ImGuiApi;
using Brutal.Localization;

namespace KsaLocFramework;

public static class Utf8LabelLocalizer
{
    private static readonly Dictionary<string, LString> Labels = new Dictionary<string, LString>
    {
        ["Select System"] = GameStrings.KLF_ConfigOnStartPopup_SelectSystem,
        ["System"] = GameStrings.KLF_ConfigOnStartPopup_System,
        ["Game Type"] = GameStrings.KLF_ConfigOnStartPopup_GameType,
        ["Type"] = GameStrings.KLF_ConfigOnStartPopup_Type,
        ["Starting Situation"] = GameStrings.KLF_ConfigOnStartPopup_StartingSituation,
        ["Vehicle"] = GameStrings.KLF_ConfigOnStartPopup_Vehicle,
        ["Location"] = GameStrings.KLF_ConfigOnStartPopup_Location,
        ["Vessels"] = GameStrings.KLF_ConfigOnStartPopup_Vessels,
        ["Textures"] = GameStrings.KLF_ConfigOnStartPopup_Textures,
        ["Terrain"] = GameStrings.KLF_ConfigOnStartPopup_Terrain,
        ["Shadows"] = GameStrings.KLF_ConfigOnStartPopup_Shadows,
        ["Texture Streaming"] = GameStrings.KLF_ConfigOnStartPopup_TextureStreaming,
        ["Enabled"] = GameStrings.KLF_ConfigOnStartPopup_Enabled,
        ["Cascaded Shadows"] = GameStrings.KLF_ConfigOnStartPopup_CascadedShadows,
        ["Filter"] = GameStrings.KLF_ConfigOnStartPopup_Filter,
        ["Light System"] = GameStrings.KLF_ConfigOnStartPopup_LightSystem,
        ["Enable Shadows"] = GameStrings.KLF_ConfigOnStartPopup_EnableShadows,
        ["Casters"] = GameStrings.KLF_ConfigOnStartPopup_Casters,
        ["Thumbnails"] = GameStrings.KLF_ConfigOnStartPopup_Thumbnails,
        ["Parts"] = GameStrings.KLF_ConfigOnStartPopup_Parts
    };

    public static ImString Localize(ReadOnlySpan<byte> label)
    {
        string key = Encoding.UTF8.GetString(label);
        if (Labels.TryGetValue(key, out LString localized))
        {
            return localized;
        }
        return new ImString(label);
    }
}
