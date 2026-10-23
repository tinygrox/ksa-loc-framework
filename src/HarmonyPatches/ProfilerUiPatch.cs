using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(ProfilerUi))]
public static class ProfilerUiPatch
{
    [HarmonyPatch("DrawMenuItems")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawMenuItemsTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
