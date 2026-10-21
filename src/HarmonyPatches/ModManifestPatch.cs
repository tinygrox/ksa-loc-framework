using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

// 修复游戏启用Mod不生效的问题
[HarmonyPatch]
public static class ModManifestPatch
{
    [HarmonyPatch(typeof(GameSettings), nameof(GameSettings.SaveChanges))]
    [HarmonyPostfix]
    public static void SaveChangesPostfix()
    {
        ModManifest currentManifest = GameSettings.Current?.Manifest;
        if (currentManifest == null || ReferenceEquals(currentManifest, ModLibrary.Manifest))
        {
            return;
        }

        ModLibrary.Manifest = currentManifest;
        ModLibrary.Manifest.Save();
    }

    [HarmonyPatch(typeof(ModLibrary), nameof(ModLibrary.PrepareAll))]
    [HarmonyPostfix]
    public static void PrepareAllPostfix()
    {
        ModManifest manifest = ModLibrary.Manifest;
        if (manifest == null)
        {
            return;
        }

        for (int i = 0; i < manifest.Mods.Count; i++)
        {
            manifest.Mods[i].Index = i;
        }
    }
}
