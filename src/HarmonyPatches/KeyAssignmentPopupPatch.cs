using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(KeyAssignmentPopup))]
public static class KeyAssignmentPopupPatch
{
    [HarmonyPatch(MethodType.StaticConstructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> StaticConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_UI_Cancel));
        return codes;
    }

    [HarmonyPatch(MethodType.Constructor, new[] { typeof(GameSettings), typeof(InputAction) })]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        SettingsEnumLocalization.LocalizeInputActionNameCalls(codes);
        return codes;
    }

    [HarmonyPatch("OnDrawUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnDrawUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_KeyAssignmentPopup_Input));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_KeyAssignmentPopup_Prompt));
        return codes;
    }

    [HarmonyPatch("DrawModifierReadout")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawModifierReadoutTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_KeyAssignmentPopup_Held));
        return codes;
    }
}
