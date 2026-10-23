using System.IO;
using Brutal.ImGuiApi;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(FontManager), nameof(FontManager.LoadFont))]
public static class GaugeFontPatch
{
    private const string GaugeFontName = "MilSpec33558";

    [HarmonyPrefix]
    public static bool Prefix(
        string filename,
        ImFontAtlasPtr atlasPtr,
        float sizePixels,
        bool pixelSnapH,
        float fontDensity,
        ref ImFontPtr __result)
    {
        unsafe
        {
            ImGui.GetStyle().FontScaleMain = 1f;
            ImFontConfig fontConfig = CreateConfig(sizePixels, pixelSnapH, fontDensity);
            __result = atlasPtr.AddFontFromFileTTF(filename, sizePixels, &fontConfig);

            if (!LocFrameworkMod.IsCjkActive || Path.GetFileNameWithoutExtension(filename) != GaugeFontName)
            {
                return false;
            }

            string cjkFontPath = LocFrameworkMod.GaugeCjkFontPath;
            if (cjkFontPath == null || !File.Exists(cjkFontPath))
            {
                return false;
            }

            ImFontConfig mergeConfig = CreateConfig(sizePixels, pixelSnapH, fontDensity);
            mergeConfig.MergeMode = true;
            atlasPtr.AddFontFromFileTTF(cjkFontPath, sizePixels, &mergeConfig, FontManagerPatch.CjkGlyphRanges);
            return false;
        }
    }

    private static ImFontConfig CreateConfig(float sizePixels, bool pixelSnapH, float fontDensity)
    {
        return new ImFontConfig
        {
            OversampleH = 3,
            OversampleV = 2,
            PixelSnapH = pixelSnapH,
            GlyphMaxAdvanceX = float.MaxValue,
            RasterizerMultiply = 1f,
            RasterizerDensity = fontDensity,
            SizePixels = sizePixels
        };
    }
}
