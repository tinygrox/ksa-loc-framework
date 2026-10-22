using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Brutal.Localization;
using Core;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(StringInputPopup))]
public static class StringInputPopupPatch
{
    [HarmonyPatch(MethodType.StaticConstructor)]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> StaticConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_UI_Save));
        return codes;
    }

    [HarmonyPatch("OnDrawUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnDrawUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_StringInputPopup_Prompt));
        return codes;
    }

    [HarmonyPatch("DrawSanitizedHint")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawSanitizedHintTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_StringInputPopup_InvalidName));

        int index = codes.FindIndex(code => code.opcode == OpCodes.Ldstr && (string)code.operand == "Will be saved as \"");
        if (index < 1 || index + 10 >= codes.Count)
        {
            return codes;
        }

        MethodInfo asSpan = AccessTools.Method(typeof(MemoryExtensions), nameof(MemoryExtensions.AsSpan), new[] { typeof(string) });
        MethodInfo appendSpan = AccessTools.Method(typeof(SpanBuilder), nameof(SpanBuilder.Append), new[] { typeof(ReadOnlySpan<char>) });
        object builder = codes[index - 1].operand;

        codes.RemoveRange(index - 1, 12);
        codes.InsertRange(index - 1, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldloca_S, builder),
            new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(GameStrings), nameof(GameStrings.KLF_StringInputPopup_WillSaveAs))),
            new CodeInstruction(OpCodes.Ldarg_0),
            new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(StringInputPopup), "_sanitized")),
            new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(TranspilerTools), nameof(TranspilerTools.FormatLocalized), new[] { typeof(LString), typeof(string) })),
            new CodeInstruction(OpCodes.Call, asSpan),
            new CodeInstruction(OpCodes.Call, appendSpan)
        });
        return codes;
    }
}
