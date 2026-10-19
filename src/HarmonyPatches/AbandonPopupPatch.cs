using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(AbandonPopup))]
public static class AbandonPopupPatch
{
    [HarmonyPatch(MethodType.StaticConstructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ButtonTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_UI_Confirm));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_UI_Cancel));
        return codes;
    }

    [HarmonyPatch("OnDrawUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> TextTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_AbandonPopup_Title));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_AbandonPopup_Body));
        return codes;
    }
}
