using HarmonyLib;
using KSA;
using KsaLocFramework.XmlPatch;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(GaugeXmlWriter), nameof(GaugeXmlWriter.SaveAll))]
public static class GaugeXmlWriterPatch
{
    [HarmonyPrefix]
    public static bool Prefix(ref string path, ref int replaced, ref bool __result)
    {
        if (!XmlPatchEngine.Enabled)
        {
            return true;
        }
        path = string.Empty;
        replaced = 0;
        __result = false;
        Console.WriteLine(
            "KSA localization framework: 'Save XML' is disabled while XML localization is active.");
        return false;
    }
}
