using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(Language), nameof(Language.Append))]
public static class LanguagePatch
{
    private static void Postfix(Language __instance)
    {
        if (__instance.Id == LocFrameworkMod.SelectedLanguage)
        {
            LocFrameworkMod.ApplyLanguage(__instance);
        }
    }
}
