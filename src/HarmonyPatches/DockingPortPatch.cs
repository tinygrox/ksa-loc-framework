using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(DockingPort))]
public static class DockingPortPatch
{
    [HarmonyPatch("ShowContextMenu")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ShowContextMenuTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
