using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(EscMenu))]
public static class EscMenuPatch
{
    [HarmonyPatch(nameof(EscMenu.Draw))]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_AbandonPopup_Title));
        return codes;
    }
}
