using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(ConfirmModPopup))]
public static class ConfirmModPopupPatch
{
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedConcatenation(codes, nameof(GameStrings.KLF_ConfirmModPopup_Body));
        return codes;
    }

    [HarmonyPatch(MethodType.StaticConstructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> StaticConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_ConfirmModPopup_Enable));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_UI_No));
        return codes;
    }

    [HarmonyPatch("OnDrawUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnDrawUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_ConfirmModPopup_Title));
        return codes;
    }
}
