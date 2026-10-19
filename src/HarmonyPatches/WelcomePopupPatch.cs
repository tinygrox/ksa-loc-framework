using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(WelcomePopup))]
public static class WelcomePopupPatch
{
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_WelcomePopup_Text));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_UI_AlwaysShow));
        return codes;
    }

    [HarmonyPatch("OnDrawUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> TitleTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_WelcomePopup_Title));
        return codes;
    }
}
