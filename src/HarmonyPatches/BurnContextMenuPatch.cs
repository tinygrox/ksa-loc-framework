using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(BurnContextMenu))]
public static class BurnContextMenuPatch
{
    [HarmonyPatch("Draw")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("BurnActionMenuItem")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> BurnActionMenuItemTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawBurnActions")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawBurnActionsTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    [HarmonyPatch("DrawParentDepartureAction")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawParentDepartureActionTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
