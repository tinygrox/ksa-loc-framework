using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch]
public static class ResourceGroupsPanelWindowPatch
{
    private static MethodBase TargetMethod()
    {
        return AccessTools.Method(AccessTools.Inner(typeof(ResourceGroupsPanel), "ResourceGroupsPanelWindow"), "DrawContent");
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
