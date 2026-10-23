using System.IO;
using HarmonyLib;
using KSA;
using KsaLocFramework.XmlPatch;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(Mod), nameof(Mod.LoadAssetBundles))]
public static class AssetBundleLoadPatch
{
    private static readonly AccessTools.FieldRef<Mod, string[]> AssetsField =
        AccessTools.FieldRefAccess<Mod, string[]>("Assets");

    [HarmonyPrefix]
    public static bool Prefix(Mod __instance)
    {
        if (!XmlPatchEngine.Enabled)
        {
            return true;
        }
        foreach (string asset in AssetsField(__instance))
        {
            string filePath = Path.Combine(__instance.DirectoryPath, asset);
            AssetBundle assetBundle = XmlPatchEngine.Load<AssetBundle>(filePath, __instance);
            if (assetBundle != null)
            {
                __instance.Register(assetBundle);
            }
        }
        return false;
    }
}
