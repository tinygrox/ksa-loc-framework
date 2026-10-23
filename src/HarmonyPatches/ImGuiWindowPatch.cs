using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(ImGuiWindow))]
public static class ImGuiWindowPatch
{
    [HarmonyPatch("DrawMenuBar")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawMenuBarTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawViewMenu")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawViewMenuTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
