using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(ConfigOnStartPopup))]
public static class ConfigOnStartPopupPatch
{
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_UI_AlwaysShow));
        return codes;
    }

    [HarmonyPatch(MethodType.StaticConstructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> StaticConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_ConfigOnStartPopup_Start));
        return codes;
    }

    [HarmonyPatch("OnDrawUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnDrawUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_ConfigOnStartPopup_Title));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_ConfigOnStartPopup_VramMessage));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_ConfigOnStartPopup_VramUsagePrefix));
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }
}
