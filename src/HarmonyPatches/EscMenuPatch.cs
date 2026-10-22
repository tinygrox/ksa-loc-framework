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
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_EscMenu_Title));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_EscMenu_Resume));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_EscMenu_SaveLoad));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_EscMenu_ExitEditor));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_EscMenu_NewVehicle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_EscMenu_LaunchVehicle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_EscMenu_RecoverVehicle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_AbandonPopup_Title));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_Settings_Title));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_EscMenu_Quit));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_EscMenu_SimulationPaused));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_EscMenu_RecoverTitle));
        TranspilerTools.ReplaceLocalizedConcatenation(codes, nameof(GameStrings.KLF_EscMenu_RecoverMessage));
        return codes;
    }
}
