using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(Parachute))]
public static class ParachutePatch
{
    [HarmonyPatch("ShowContextMenu")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ShowContextMenuTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Parachute_State));
        return codes;
    }
}
