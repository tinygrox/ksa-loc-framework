using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch]
public static class UniverseManifestWindowDrawPatch
{
    private static MethodBase TargetMethod()
    {
        return AccessTools.Method(AccessTools.Inner(typeof(UniverseManifest), "Window"), "Draw");
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Manifest_Debris));
        return codes;
    }
}

[HarmonyPatch]
public static class UniverseManifestWindowMenuBarPatch
{
    private static MethodBase TargetMethod()
    {
        return AccessTools.Method(AccessTools.Inner(typeof(UniverseManifest), "Window"), "DrawMenuBar");
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}

[HarmonyPatch]
public static class KittenRosterWindowDrawPatch
{
    private static MethodBase TargetMethod()
    {
        return AccessTools.Method(AccessTools.Inner(typeof(KittenRosterWindow), "Window"), "Draw");
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}

[HarmonyPatch]
public static class KittenRosterWindowMenuBarPatch
{
    private static MethodBase TargetMethod()
    {
        return AccessTools.Method(AccessTools.Inner(typeof(KittenRosterWindow), "Window"), "DrawMenuBar");
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}

[HarmonyPatch]
public static class GameSavesWindowMenuBarPatch
{
    private static MethodBase TargetMethod()
    {
        return AccessTools.Method(AccessTools.Inner(typeof(GameSaves), "GameSavesWindow"), "DrawMenuBar");
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}

[HarmonyPatch]
public static class LayoutSavesWindowMenuBarPatch
{
    private static MethodBase TargetMethod()
    {
        return AccessTools.Method(AccessTools.Inner(typeof(LayoutSaves), "LayoutSavesWindow"), "DrawMenuBar");
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}

[HarmonyPatch]
public static class GaugeInspectorWindowContentPatch
{
    private static MethodBase TargetMethod()
    {
        return AccessTools.Method(AccessTools.Inner(typeof(GaugeInspector), "InspectorWindow"), "DrawContent");
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
