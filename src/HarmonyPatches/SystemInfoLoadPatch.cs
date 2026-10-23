using System.IO;
using HarmonyLib;
using KSA;
using KsaLocFramework.XmlPatch;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(Mod), nameof(Mod.PrepareSystems))]
public static class SystemInfoLoadPatch
{
    [HarmonyPrefix]
    public static bool Prefix(Mod __instance)
    {
        if (!XmlPatchEngine.Enabled)
        {
            return true;
        }
        foreach (string template in __instance.SystemTemplates)
        {
            string filePath = Path.Combine(__instance.DirectoryPath, template);
            SystemInfo systemInfo = XmlPatchEngine.Load<SystemInfo>(filePath, __instance);
            if (systemInfo != null)
            {
                systemInfo.XmlName = template;
                SelectSystem.Register(systemInfo);
            }
        }
        return false;
    }
}
