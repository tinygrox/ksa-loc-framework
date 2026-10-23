using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(Program))]
public static class ProgramMenuPatch
{
    [HarmonyPatch("DrawMenuBar")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawMenuBarTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
