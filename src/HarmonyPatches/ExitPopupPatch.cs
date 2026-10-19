using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(ExitPopup))]
public static class ExitPopupPatch
{
    [HarmonyPatch(MethodType.StaticConstructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> StaticConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_UI_Confirm));
        return codes;
    }

    [HarmonyPatch("OnDrawUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnDrawUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_ExitPopup_Title));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_ExitPopup_Body));
        return codes;
    }
}
