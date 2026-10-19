using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(Popup))]
public static class PopupPatch
{
    [HarmonyPatch(MethodType.StaticConstructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> StaticConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_UI_Okay));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_UI_Cancel));
        return codes;
    }
}
