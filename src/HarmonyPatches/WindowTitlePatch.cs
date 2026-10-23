using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(ImGuiWindow), nameof(ImGuiWindow.SetWindowTitle))]
public static class WindowTitlePatch
{
    [HarmonyPrefix]
    public static void Prefix(ref string name)
    {
        name = WindowTitleLocalizer.Localize(name);
    }
}
