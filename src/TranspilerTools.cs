using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Brutal.ImGuiApi;
using Brutal.Localization;
using HarmonyLib;

namespace KsaLocFramework;

public static class TranspilerTools
{
    public static string FormatLocalized(LString template, string replacement)
    {
        return template.Format(replacement);
    }

    public static void ReplaceLocalizedLiteral(List<CodeInstruction> codes, string gameStringFieldName)
    {
        string template = TemplateOf(gameStringFieldName);
        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Ldstr || (string)codes[i].operand != template)
            {
                continue;
            }

            codes[i].opcode = OpCodes.Ldsfld;
            codes[i].operand = AccessTools.Field(typeof(GameStrings), gameStringFieldName);
            codes.Insert(i + 1, new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(LString), nameof(LString.Localized))));
            i++;
        }
    }

    public static void ReplaceLocalizedConcatenation(List<CodeInstruction> codes, string gameStringFieldName)
    {
        if (!TrySplitReplacement(TemplateOf(gameStringFieldName), out string prefix, out string suffix))
        {
            return;
        }

        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Ldstr || (string)codes[i].operand != prefix)
            {
                continue;
            }

            int concatIndex = IndexOfCall(
                codes,
                i,
                AccessTools.Method(typeof(string), nameof(string.Concat), new[] { typeof(string), typeof(string), typeof(string) }));

            if (concatIndex < 1 || codes[concatIndex - 1].opcode != OpCodes.Ldstr || (string)codes[concatIndex - 1].operand != suffix)
            {
                continue;
            }

            codes[i].opcode = OpCodes.Ldsfld;
            codes[i].operand = AccessTools.Field(typeof(GameStrings), gameStringFieldName);
            codes[concatIndex - 1].opcode = OpCodes.Nop;
            codes[concatIndex - 1].operand = null;
            codes[concatIndex].operand = AccessTools.Method(typeof(TranspilerTools), nameof(FormatLocalized));
        }
    }

    public static void ReplaceUtf8ImStringLabels(List<CodeInstruction> codes)
    {
        ReplaceCall(
            codes,
            AccessTools.Method(typeof(ImString), "op_Implicit", new[] { typeof(ReadOnlySpan<byte>) }),
            AccessTools.Method(typeof(Utf8LabelLocalizer), nameof(Utf8LabelLocalizer.Localize)));
    }

    private static string TemplateOf(string gameStringFieldName)
    {
        return Traverse.Create(typeof(GameStrings)).Field(gameStringFieldName).GetValue<LString>().Template;
    }

    private static bool TrySplitReplacement(string template, out string prefix, out string suffix)
    {
        int placeholderStart = template.IndexOf('{');
        int placeholderEnd = template.IndexOf('}', placeholderStart + 1);
        if (placeholderStart < 0 || placeholderEnd < 0)
        {
            prefix = null;
            suffix = null;
            return false;
        }

        prefix = template.Substring(0, placeholderStart);
        suffix = template.Substring(placeholderEnd + 1);
        return true;
    }

    private static int IndexOfCall(List<CodeInstruction> codes, int startIndex, MethodInfo target)
    {
        for (int i = startIndex + 1; i < codes.Count; i++)
        {
            if (IsCall(codes[i], target))
            {
                return i;
            }
        }

        return -1;
    }

    private static void ReplaceCall(List<CodeInstruction> codes, MethodInfo target, MethodInfo replacement)
    {
        for (int i = 0; i < codes.Count; i++)
        {
            if (IsCall(codes[i], target))
            {
                codes[i].operand = replacement;
            }
        }
    }

    private static bool IsCall(CodeInstruction code, MethodInfo target)
    {
        return code.opcode == OpCodes.Call && code.operand is MethodInfo method && method == target;
    }
}
