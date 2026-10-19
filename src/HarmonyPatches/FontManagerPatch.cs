using System.IO;
using System.Linq;
using Brutal.ImGuiApi;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(FontManager))]
public static class FontManagerPatch
{
    public static readonly uint[] CjkGlyphRanges =
    {
        0x0020, 0x00FF,
        0x2000, 0x206F,
        0x3000, 0x30FF,
        0x31F0, 0x31FF,
        0xFF00, 0xFFEF,
        0xFFFD, 0xFFFD,
        0x4E00, 0x9FAF,
        0
    };

    [HarmonyPatch(nameof(FontManager.RegenerateFonts))]
    [HarmonyPostfix]
    public static void RegenerateFontsPostfix()
    {
        ImFontAtlasPtr atlas = ImGui.GetIO().Fonts;
        float fontSize = GameSettings.GetFontSize();
        foreach (string fontPath in LocFrameworkMod.FontPaths)
        {
            FontManager.Fonts[Path.GetFileNameWithoutExtension(fontPath)] = atlas.AddFontFromFileTTF(fontPath, fontSize, glyphRanges: CjkGlyphRanges);
        }
        string smallPath = LocFrameworkMod.FontPaths.FirstOrDefault(p => Path.GetFileNameWithoutExtension(p) == GameSettings.GetFontName());
        if (smallPath != null)
        {
            FontManager.SmallFont = atlas.AddFontFromFileTTF(smallPath, fontSize * 0.8f, glyphRanges: CjkGlyphRanges);
            ImGuiHelper.SetTooltipFont(FontManager.SmallFont);
        }
    }

    [HarmonyPatch(nameof(FontManager.RegisterFont))]
    [HarmonyPostfix]
    public static void RegisterFontPostfix(string font)
    {
        LocFrameworkMod.AddFontPath(font);
        FontManager.RegenerateFonts();
    }
}
