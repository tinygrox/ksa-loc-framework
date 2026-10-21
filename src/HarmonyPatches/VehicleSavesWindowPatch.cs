using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch]
public static class VehicleSavesWindowPatch
{
    [HarmonyTargetMethod]
    public static MethodBase DrawMenuBarTarget()
    {
        return AccessTools.Method(AccessTools.TypeByName("KSA.VehicleSaves+VehicleSavesWindow"), "DrawMenuBar");
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawMenuBarTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_PartTreeSelectionPopup_Description));
        return codes;
    }
}
