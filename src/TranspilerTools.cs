using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Brutal.ImGuiApi;
using Brutal.Localization;
using Core;
using HarmonyLib;

namespace KsaLocFramework;

public static class TranspilerTools
{
    public static string FormatLocalized(LString template, string replacement)
    {
        return template.Format(replacement);
    }

    public static string FormatLocalized(string first, LString template, string second)
    {
        return template.Format(first, second);
    }

    public static string FormatLocalized(string first, LString template)
    {
        return template.Format(first);
    }

    public static string FormatLocalized(LString template, string first, string second)
    {
        return template.Format(first, second);
    }

    public static string FormatLocalized(LString template, string first, string second, string third, string fourth)
    {
        return template.Format(first, second, third, fourth);
    }

    public static string FormatLocalized(LString template, ReadOnlySpan<char> value)
    {
        return template.Format(value.ToString());
    }

    public static string FormatLocalized(LString template, ReadOnlySpan<char> first, ReadOnlySpan<char> second)
    {
        return template.Format(first.ToString(), second.ToString());
    }

    public static void AppendLocalized(ref SpanBuilder builder, LString template, ReadOnlySpan<char> value)
    {
        builder.Append(template.Format(value.ToString()));
    }

    public static void AppendLocalized(ref SpanBuilder builder, LString template, ReadOnlySpan<char> first, ReadOnlySpan<char> second)
    {
        builder.Append(template.Format(first.ToString(), second.ToString()));
    }

    public static void ReplaceLocalizedLiteral(List<CodeInstruction> codes, string gameStringFieldName)
    {
        ReplaceLocalizedLiteral(codes, TemplateOf(gameStringFieldName), gameStringFieldName);
    }

    public static void ReplaceLocalizedLiteral(List<CodeInstruction> codes, string literal, string gameStringFieldName)
    {
        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Ldstr || (string)codes[i].operand != literal)
            {
                continue;
            }

            ReplaceWithLocalized(codes, i, gameStringFieldName);
            i++;
        }
    }

    public static void ReplaceLocalizedLiteral(List<CodeInstruction> codes, string literal, string gameStringFieldName, int occurrence)
    {
        int matches = 0;
        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Ldstr || (string)codes[i].operand != literal)
            {
                continue;
            }

            if (matches++ != occurrence)
            {
                continue;
            }

            ReplaceWithLocalized(codes, i, gameStringFieldName);
            return;
        }
    }

    private static void ReplaceWithLocalized(List<CodeInstruction> codes, int index, string gameStringFieldName)
    {
        codes[index].opcode = OpCodes.Ldsfld;
        codes[index].operand = AccessTools.Field(typeof(GameStrings), gameStringFieldName);
        codes.Insert(index + 1, new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(LString), nameof(LString.Localized))));
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
            codes[concatIndex].operand = AccessTools.Method(typeof(TranspilerTools), nameof(FormatLocalized), new[] { typeof(LString), typeof(string) });
        }
    }

    public static void ReplaceLocalizedLabelBuilder(List<CodeInstruction> codes, string gameStringFieldName)
    {
        string template = TemplateOf(gameStringFieldName);
        List<string> literals = new List<string>();
        int cursor = 0;
        int placeholders = 0;
        while (true)
        {
            int open = template.IndexOf('{', cursor);
            if (open < 0)
            {
                literals.Add(template.Substring(cursor));
                break;
            }

            literals.Add(template.Substring(cursor, open - cursor));
            int close = template.IndexOf('}', open + 1);
            if (close < 0)
            {
                return;
            }

            placeholders++;
            cursor = close + 1;
        }

        if (placeholders == 0 || placeholders > 2)
        {
            return;
        }

        MethodInfo asSpan = AccessTools.Method(typeof(MemoryExtensions), nameof(MemoryExtensions.AsSpan), new[] { typeof(string) });
        MethodInfo appendLiteral = AccessTools.Method(typeof(ImString), nameof(ImString.AppendLiteral), new[] { typeof(ReadOnlySpan<char>) });
        MethodInfo appendString = AccessTools.Method(typeof(ImString), nameof(ImString.AppendFormatted), new[] { typeof(string), typeof(int), typeof(string) });
        MethodInfo appendSpan = AccessTools.Method(typeof(ImString), nameof(ImString.AppendFormatted), new[] { typeof(ReadOnlySpan<char>), typeof(int), typeof(string) });
        MethodInfo toImString = AccessTools.Method(typeof(ImString), "op_Implicit", new[] { typeof(string) });

        for (int i = 1; i < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Ldstr || (string)codes[i].operand != literals[0])
            {
                continue;
            }

            if (!IsBuilderAddress(codes[i - 1]) || codes[i - 1].operand is not LocalBuilder builder || !IsCall(codes[i + 1], asSpan) || !IsCall(codes[i + 2], appendLiteral))
            {
                continue;
            }

            List<(int Start, int End)> values = new List<(int, int)>();
            int appendIndex = i + 2;
            bool matched = true;
            bool spans = false;
            for (int k = 0; k < placeholders && matched; k++)
            {
                int valueStart = appendIndex + 2;
                if (appendIndex + 2 >= codes.Count || !IsBuilderAddress(codes[appendIndex + 1]))
                {
                    matched = false;
                    break;
                }

                int formatted = -1;
                bool spanValue = false;
                for (int j = valueStart; j + 2 < codes.Count; j++)
                {
                    if (IsCall(codes[j], appendString))
                    {
                        formatted = j;
                        break;
                    }

                    if (IsCall(codes[j], appendSpan))
                    {
                        formatted = j;
                        spanValue = true;
                        break;
                    }
                }

                if (formatted < 3 || codes[formatted - 1].opcode != OpCodes.Ldnull || codes[formatted - 2].opcode != OpCodes.Ldc_I4_0 || formatted - 3 < valueStart || (k > 0 && spanValue != spans))
                {
                    matched = false;
                    break;
                }

                spans = spanValue;
                values.Add((valueStart, formatted - 3));
                appendIndex = formatted;

                string trailing = literals[k + 1];
                if (trailing.Length > 0)
                {
                    if (appendIndex + 4 >= codes.Count ||
                        !IsBuilderAddress(codes[appendIndex + 1]) ||
                        codes[appendIndex + 2].opcode != OpCodes.Ldstr ||
                        (string)codes[appendIndex + 2].operand != trailing ||
                        !IsCall(codes[appendIndex + 3], asSpan) ||
                        !IsCall(codes[appendIndex + 4], appendLiteral))
                    {
                        matched = false;
                        break;
                    }

                    appendIndex += 4;
                }
            }

            if (!matched)
            {
                continue;
            }

            MethodInfo format;
            if (spans)
            {
                format = placeholders == 1
                    ? AccessTools.Method(typeof(TranspilerTools), nameof(FormatLocalized), new[] { typeof(LString), typeof(ReadOnlySpan<char>) })
                    : AccessTools.Method(typeof(TranspilerTools), nameof(FormatLocalized), new[] { typeof(LString), typeof(ReadOnlySpan<char>), typeof(ReadOnlySpan<char>) });
            }
            else
            {
                format = placeholders == 1
                    ? AccessTools.Method(typeof(TranspilerTools), nameof(FormatLocalized), new[] { typeof(LString), typeof(string) })
                    : AccessTools.Method(typeof(TranspilerTools), nameof(FormatLocalized), new[] { typeof(LString), typeof(string), typeof(string) });
            }

            int creation = -1;
            for (int b = i - 1; b >= Math.Max(0, i - 8); b--)
            {
                if (codes[b].operand is ConstructorInfo constructor && constructor.DeclaringType == typeof(ImString))
                {
                    creation = b;
                    break;
                }
            }

            if (creation < 3)
            {
                continue;
            }

            int start = codes[creation].opcode == OpCodes.Newobj ? creation - 2 : creation - 3;
            if (start < 0)
            {
                continue;
            }

            int removedEnd = appendIndex;
            if (codes[creation].opcode == OpCodes.Newobj)
            {
                removedEnd = Math.Max(removedEnd, creation + 1);
            }

            bool removable = true;
            for (int r = start; r <= removedEnd && removable; r++)
            {
                bool insideValue = false;
                foreach ((int valueStartIndex, int valueEndIndex) in values)
                {
                    if (r >= valueStartIndex && r <= valueEndIndex)
                    {
                        insideValue = true;
                        break;
                    }
                }

                if (!insideValue && (codes[r].labels.Count > 0 || codes[r].blocks.Count > 0))
                {
                    removable = false;
                }
            }

            if (!removable)
            {
                continue;
            }

            List<CodeInstruction> replacement = new List<CodeInstruction>
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(GameStrings), gameStringFieldName))
            };
            foreach ((int valueStartIndex, int valueEndIndex) in values)
            {
                replacement.AddRange(codes.GetRange(valueStartIndex, valueEndIndex - valueStartIndex + 1));
            }

            replacement.Add(new CodeInstruction(OpCodes.Call, format));
            replacement.Add(new CodeInstruction(OpCodes.Call, toImString));
            replacement.Add(new CodeInstruction(OpCodes.Stloc, builder));

            codes.RemoveRange(start, removedEnd - start + 1);
            codes.InsertRange(start, replacement);
            return;
        }
    }

    public static void ReplaceLocalizedCountLabel(List<CodeInstruction> codes, string gameStringFieldName)
    {
        string template = TemplateOf(gameStringFieldName);
        int open = template.IndexOf('{');
        int close = open < 0 ? -1 : template.IndexOf('}', open + 1);
        if (open != 0 || close < 0)
        {
            return;
        }

        ReplaceLocalizedCountLabel(codes, gameStringFieldName, template.Substring(close + 1));
    }

    public static void ReplaceLocalizedCountLabel(List<CodeInstruction> codes, string gameStringFieldName, string literalSuffix)
    {
        if (literalSuffix.Length == 0)
        {
            return;
        }

        MethodInfo asSpan = AccessTools.Method(typeof(MemoryExtensions), nameof(MemoryExtensions.AsSpan), new[] { typeof(string) });
        MethodInfo appendLiteral = AccessTools.Method(typeof(ImString), nameof(ImString.AppendLiteral), new[] { typeof(ReadOnlySpan<char>) });
        MethodInfo toImString = AccessTools.Method(typeof(ImString), "op_Implicit", new[] { typeof(string) });
        MethodInfo format = AccessTools.Method(typeof(TranspilerTools), nameof(FormatLocalized), new[] { typeof(string), typeof(LString) });

        for (int i = 2; i < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Ldstr || (string)codes[i].operand != literalSuffix)
            {
                continue;
            }

            if (i + 2 >= codes.Count || !IsBuilderAddress(codes[i - 1]) || !IsCall(codes[i + 1], asSpan) || !IsCall(codes[i + 2], appendLiteral))
            {
                continue;
            }

            int formatted = i - 2;
            if (formatted < 0 || !TryGetAppendFormatted(codes[formatted], out Type valueType, out CodeInstruction formatOperand))
            {
                continue;
            }

            int valueEnd = formatted - 3;
            int valueStart = -1;
            for (int b = valueEnd; b >= 0; b--)
            {
                if (IsBuilderAddress(codes[b]))
                {
                    valueStart = b + 1;
                    break;
                }
            }

            if (valueStart < 0 || valueStart > valueEnd)
            {
                continue;
            }

            int creation = -1;
            for (int b = valueStart - 1; b >= Math.Max(0, valueStart - 8); b--)
            {
                if (codes[b].operand is ConstructorInfo constructor && constructor.DeclaringType == typeof(ImString))
                {
                    creation = b;
                    break;
                }
            }

            if (creation < 3)
            {
                continue;
            }

            int start = codes[creation].opcode == OpCodes.Newobj ? creation - 2 : creation - 3;
            int end = i + 2;

            int trailing = end + 1;
            if (trailing + 1 < codes.Count && IsBuilderAddress(codes[trailing]))
            {
                for (int j = trailing + 1; j + 2 < codes.Count; j++)
                {
                    if (TryGetAppendFormatted(codes[j], out _, out _))
                    {
                        end = j;
                        break;
                    }

                    if (codes[j].opcode == OpCodes.Ldloc || codes[j].opcode == OpCodes.Ldloc_S)
                    {
                        break;
                    }
                }
            }

            List<CodeInstruction> replacement = new List<CodeInstruction>();
            replacement.AddRange(codes.GetRange(valueStart, valueEnd - valueStart + 1));
            AppendCountToString(replacement, valueType, formatOperand);
            replacement.Add(new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(GameStrings), gameStringFieldName)));
            replacement.Add(new CodeInstruction(OpCodes.Call, format));
            replacement.Add(new CodeInstruction(OpCodes.Call, toImString));
            replacement.Add(new CodeInstruction(OpCodes.Stloc, codes[i - 1].operand as LocalBuilder));

            for (int r = start; r <= end; r++)
            {
                if (codes[r].labels.Count == 0 || r >= valueStart && r <= valueEnd)
                {
                    continue;
                }

                CodeInstruction keep = new CodeInstruction(OpCodes.Nop);
                keep.labels.AddRange(codes[r].labels);
                codes[r].labels.Clear();
                replacement.Insert(0, keep);
            }

            codes.RemoveRange(start, end - start + 1);
            codes.InsertRange(start, replacement);
            return;
        }
    }

    private static void AppendCountToString(List<CodeInstruction> codes, Type valueType, CodeInstruction formatOperand)
    {
        if (valueType == typeof(string))
        {
            return;
        }

        if (valueType == typeof(ReadOnlySpan<char>))
        {
            codes.Add(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ReadOnlySpan<char>), nameof(ReadOnlySpan<char>.ToString), Type.EmptyTypes)));
            return;
        }

        MethodInfo toString = formatOperand == null
            ? AccessTools.Method(valueType, nameof(ToString), Type.EmptyTypes)
            : AccessTools.Method(valueType, nameof(ToString), new[] { typeof(string), typeof(IFormatProvider) });
        if (formatOperand != null)
        {
            codes.Add(new CodeInstruction(OpCodes.Ldstr, formatOperand.operand));
            codes.Add(new CodeInstruction(OpCodes.Ldnull));
        }

        codes.Add(new CodeInstruction(OpCodes.Callvirt, toString));
    }

    private static bool TryGetAppendFormatted(CodeInstruction code, out Type valueType, out CodeInstruction formatOperand)
    {
        valueType = null;
        formatOperand = null;
        if (code.opcode != OpCodes.Call && code.opcode != OpCodes.Callvirt)
        {
            return false;
        }

        if (code.operand is not MethodInfo method || method.Name != nameof(ImString.AppendFormatted) || method.DeclaringType != typeof(ImString))
        {
            return false;
        }

        ParameterInfo[] parameters = method.GetParameters();
        if (parameters.Length != 3)
        {
            return false;
        }

        valueType = parameters[0].ParameterType;
        if (valueType.IsGenericParameter && method.IsGenericMethod)
        {
            valueType = method.GetGenericArguments()[0];
        }

        return true;
    }

    public static void ReplaceLocalizedInterpolatedAlert(List<CodeInstruction> codes, string gameStringFieldName)
    {
        string template = TemplateOf(gameStringFieldName);
        List<string> literals = new List<string>();
        int cursor = 0;
        while (true)
        {
            int open = template.IndexOf('{', cursor);
            if (open < 0)
            {
                literals.Add(template.Substring(cursor));
                break;
            }

            literals.Add(template.Substring(cursor, open - cursor));
            int close = template.IndexOf('}', open + 1);
            if (close < 0)
            {
                return;
            }

            cursor = close + 1;
        }

        if (literals.Count != 3)
        {
            return;
        }

        MethodInfo appendLiteral = AccessTools.Method(typeof(DefaultInterpolatedStringHandler), nameof(DefaultInterpolatedStringHandler.AppendLiteral), new[] { typeof(string) });
        MethodInfo toText = AccessTools.Method(typeof(double), nameof(double.ToString), new[] { typeof(string), typeof(IFormatProvider) });
        MethodInfo format = AccessTools.Method(typeof(TranspilerTools), nameof(FormatLocalized), new[] { typeof(LString), typeof(string), typeof(string) });

        for (int i = 6; i + 17 < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Ldstr || (string)codes[i].operand != literals[0])
            {
                continue;
            }

            if (!IsCall(codes[i + 1], appendLiteral) ||
                codes[i + 7].opcode != OpCodes.Ldstr || (string)codes[i + 7].operand != literals[1] ||
                !IsCall(codes[i + 8], appendLiteral) ||
                codes[i + 14].opcode != OpCodes.Ldstr || (string)codes[i + 14].operand != literals[2] ||
                !IsCall(codes[i + 15], appendLiteral))
            {
                continue;
            }

            if (codes[i + 4].opcode != OpCodes.Ldstr || codes[i + 11].opcode != OpCodes.Ldstr)
            {
                continue;
            }

            if (codes[i + 17].operand is not MethodInfo finish || finish.Name != nameof(DefaultInterpolatedStringHandler.ToStringAndClear))
            {
                continue;
            }

            int start = -1;
            for (int b = i - 1; b >= Math.Max(0, i - 6); b--)
            {
                if (codes[b].operand is ConstructorInfo constructor && constructor.DeclaringType == typeof(DefaultInterpolatedStringHandler))
                {
                    start = b - 3;
                    break;
                }
            }

            if (start < 0)
            {
                continue;
            }

            List<CodeInstruction> replacement = new List<CodeInstruction>
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(GameStrings), gameStringFieldName))
            };
            replacement.Add(codes[i + 3]);
            replacement.Add(new CodeInstruction(OpCodes.Ldstr, codes[i + 4].operand));
            replacement.Add(new CodeInstruction(OpCodes.Ldnull));
            replacement.Add(new CodeInstruction(OpCodes.Callvirt, toText));
            replacement.Add(codes[i + 10]);
            replacement.Add(new CodeInstruction(OpCodes.Ldstr, codes[i + 11].operand));
            replacement.Add(new CodeInstruction(OpCodes.Ldnull));
            replacement.Add(new CodeInstruction(OpCodes.Callvirt, toText));
            replacement.Add(new CodeInstruction(OpCodes.Call, format));

            codes.RemoveRange(start, i + 17 - start + 1);
            codes.InsertRange(start, replacement);
            return;
        }
    }

    public static void ReplaceLocalizedShortConcatenation(List<CodeInstruction> codes, string gameStringFieldName)
    {
        string template = TemplateOf(gameStringFieldName);
        int open = template.IndexOf('{');
        int close = open < 0 ? -1 : template.IndexOf('}', open + 1);
        if (close < 0)
        {
            return;
        }

        string prefix = template.Substring(0, open);
        string suffix = template.Substring(close + 1);
        if ((prefix.Length == 0 && suffix.Length == 0) || (prefix.Length > 0 && suffix.Length > 0))
        {
            return;
        }

        bool prefixed = prefix.Length > 0;
        string literal = prefixed ? prefix : suffix;
        MethodInfo format = prefixed
            ? AccessTools.Method(typeof(TranspilerTools), nameof(FormatLocalized), new[] { typeof(LString), typeof(string) })
            : AccessTools.Method(typeof(TranspilerTools), nameof(FormatLocalized), new[] { typeof(string), typeof(LString) });
        MethodInfo concat = AccessTools.Method(typeof(string), nameof(string.Concat), new[] { typeof(string), typeof(string) });

        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Ldstr || (string)codes[i].operand != literal)
            {
                continue;
            }

            int concatIndex = IndexOfCall(codes, i, concat);
            if (concatIndex < 0)
            {
                continue;
            }

            codes[i].opcode = OpCodes.Ldsfld;
            codes[i].operand = AccessTools.Field(typeof(GameStrings), gameStringFieldName);
            codes[concatIndex].operand = format;
            return;
        }
    }

    public static void ReplaceLocalizedTwoValueConcatenation(List<CodeInstruction> codes, string gameStringFieldName)
    {
        string template = TemplateOf(gameStringFieldName);
        int firstEnd = template.IndexOf('}');
        int secondStart = firstEnd < 0 ? -1 : template.IndexOf('{', firstEnd + 1);
        int secondEnd = secondStart < 0 ? -1 : template.IndexOf('}', secondStart + 1);
        if (secondStart < 0 || secondEnd < 0)
        {
            return;
        }

        string middle = template.Substring(firstEnd + 1, secondStart - firstEnd - 1);
        string suffix = template.Substring(secondEnd + 1);
        if (middle.Length == 0)
        {
            return;
        }

        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Ldstr || (string)codes[i].operand != middle)
            {
                continue;
            }

            int concatIndex = IndexOfCall(
                codes,
                i,
                AccessTools.Method(typeof(string), nameof(string.Concat), new[] { typeof(string), typeof(string), typeof(string), typeof(string) }));

            if (concatIndex < 1 || codes[concatIndex - 1].opcode != OpCodes.Ldstr || (string)codes[concatIndex - 1].operand != suffix)
            {
                continue;
            }

            codes[i].opcode = OpCodes.Ldsfld;
            codes[i].operand = AccessTools.Field(typeof(GameStrings), gameStringFieldName);
            codes[concatIndex - 1].opcode = OpCodes.Nop;
            codes[concatIndex - 1].operand = null;
            codes[concatIndex].operand = AccessTools.Method(typeof(TranspilerTools), nameof(FormatLocalized), new[] { typeof(string), typeof(LString), typeof(string) });
        }
    }

    public static void ReplaceLocalizedSpanHint(List<CodeInstruction> codes, string gameStringFieldName)
    {
        string template = TemplateOf(gameStringFieldName);
        int open = template.IndexOf('{');
        int close = open < 0 ? -1 : template.IndexOf('}', open + 1);
        if (close < 0)
        {
            return;
        }

        string prefix = template.Substring(0, open);
        string suffix = template.Substring(close + 1);
        MethodInfo asSpan = AccessTools.Method(typeof(MemoryExtensions), nameof(MemoryExtensions.AsSpan), new[] { typeof(string) });
        MethodInfo appendSpan = AccessTools.Method(typeof(SpanBuilder), nameof(SpanBuilder.Append), new[] { typeof(ReadOnlySpan<char>) });
        MethodInfo appendChar = AccessTools.Method(typeof(SpanBuilder), nameof(SpanBuilder.Append), new[] { typeof(char) });
        MethodInfo appendLocalized = AccessTools.Method(
            typeof(TranspilerTools),
            nameof(AppendLocalized),
            new[] { typeof(SpanBuilder).MakeByRefType(), typeof(LString), typeof(ReadOnlySpan<char>) });

        for (int i = 1; i < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Ldstr || (string)codes[i].operand != prefix)
            {
                continue;
            }

            if (i + 3 >= codes.Count || !IsBuilderAddress(codes[i - 1]) || !IsCall(codes[i + 1], asSpan) || !IsCall(codes[i + 2], appendSpan))
            {
                continue;
            }

            int valueEnd = IndexOfSpanAppend(codes, i + 3, asSpan, appendSpan);
            if (valueEnd < 0)
            {
                continue;
            }

            int appendIndex = valueEnd + 1;
            int closeIndex = appendIndex;
            if (suffix.Length > 0)
            {
                if (suffix.Length != 1 || appendIndex + 3 >= codes.Count ||
                    !IsBuilderAddress(codes[appendIndex + 1]) ||
                    !IsIntConstant(codes[appendIndex + 2], suffix[0]) ||
                    !IsCall(codes[appendIndex + 3], appendChar))
                {
                    continue;
                }

                closeIndex = appendIndex + 3;
            }

            if (!IsRemovable(codes, i + 1, i + 3) || !IsRemovable(codes, appendIndex, closeIndex))
            {
                continue;
            }

            codes[i].opcode = OpCodes.Ldsfld;
            codes[i].operand = AccessTools.Field(typeof(GameStrings), gameStringFieldName);
            Nop(codes, i + 1, i + 3);
            codes[appendIndex].operand = appendLocalized;
            Nop(codes, appendIndex + 1, closeIndex);
            return;
        }
    }

    public static void ReplaceLocalizedSpanStateHint(List<CodeInstruction> codes, string gameStringFieldName)
    {
        string template = TemplateOf(gameStringFieldName);
        int firstOpen = template.IndexOf('{');
        int firstClose = firstOpen < 0 ? -1 : template.IndexOf('}', firstOpen + 1);
        int secondOpen = firstClose < 0 ? -1 : template.IndexOf('{', firstClose + 1);
        int secondClose = secondOpen < 0 ? -1 : template.IndexOf('}', secondOpen + 1);
        if (secondClose < 0)
        {
            return;
        }

        string prefix = template.Substring(0, firstOpen);
        string middle = template.Substring(firstClose + 1, secondOpen - firstClose - 1);
        string suffix = template.Substring(secondClose + 1);
        if (middle.Length == 0 || suffix.Length != 1)
        {
            return;
        }

        MethodInfo asSpan = AccessTools.Method(typeof(MemoryExtensions), nameof(MemoryExtensions.AsSpan), new[] { typeof(string) });
        MethodInfo appendSpan = AccessTools.Method(typeof(SpanBuilder), nameof(SpanBuilder.Append), new[] { typeof(ReadOnlySpan<char>) });
        MethodInfo appendChar = AccessTools.Method(typeof(SpanBuilder), nameof(SpanBuilder.Append), new[] { typeof(char) });
        MethodInfo appendLocalized = AccessTools.Method(
            typeof(TranspilerTools),
            nameof(AppendLocalized),
            new[] { typeof(SpanBuilder).MakeByRefType(), typeof(LString), typeof(ReadOnlySpan<char>), typeof(ReadOnlySpan<char>) });

        for (int i = 1; i < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Ldstr || (string)codes[i].operand != prefix)
            {
                continue;
            }

            if (i + 3 >= codes.Count || !IsBuilderAddress(codes[i - 1]) || !IsCall(codes[i + 1], asSpan) || !IsCall(codes[i + 2], appendSpan))
            {
                continue;
            }

            int stateEnd = IndexOfSpanAppend(codes, i + 3, asSpan, appendSpan);
            if (stateEnd < 0)
            {
                continue;
            }

            int nextSegment = stateEnd + 2;
            if (nextSegment + 1 >= codes.Count || !IsBuilderAddress(codes[nextSegment]))
            {
                continue;
            }

            int middleEnd = nextSegment;
            int keyStart = nextSegment + 1;
            if (codes[nextSegment + 1].opcode == OpCodes.Ldstr && (string)codes[nextSegment + 1].operand == middle)
            {
                if (nextSegment + 4 >= codes.Count ||
                    !IsCall(codes[nextSegment + 2], asSpan) ||
                    !IsCall(codes[nextSegment + 3], appendSpan) ||
                    !IsBuilderAddress(codes[nextSegment + 4]))
                {
                    continue;
                }

                middleEnd = nextSegment + 4;
                keyStart = nextSegment + 5;
            }

            int keyEnd = IndexOfSpanAppend(codes, keyStart, asSpan, appendSpan);
            if (keyEnd < 0)
            {
                continue;
            }

            int appendIndex = keyEnd + 1;
            if (appendIndex + 3 >= codes.Count ||
                !IsBuilderAddress(codes[appendIndex + 1]) ||
                !IsIntConstant(codes[appendIndex + 2], suffix[0]) ||
                !IsCall(codes[appendIndex + 3], appendChar))
            {
                continue;
            }

            if (!IsRemovable(codes, i + 1, i + 3) ||
                !IsRemovable(codes, stateEnd + 1, stateEnd + 1) ||
                !IsRemovable(codes, nextSegment, middleEnd) ||
                !IsRemovable(codes, appendIndex, appendIndex + 3))
            {
                continue;
            }

            codes[i].opcode = OpCodes.Ldsfld;
            codes[i].operand = AccessTools.Field(typeof(GameStrings), gameStringFieldName);
            Nop(codes, i + 1, i + 3);
            Nop(codes, stateEnd + 1, stateEnd + 1);
            Nop(codes, nextSegment, middleEnd);
            codes[appendIndex].operand = appendLocalized;
            Nop(codes, appendIndex + 1, appendIndex + 3);
            return;
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

    private static bool IsBuilderAddress(CodeInstruction code)
    {
        return code.opcode == OpCodes.Ldloca || code.opcode == OpCodes.Ldloca_S;
    }

    private static int IndexOfSpanAppend(List<CodeInstruction> codes, int startIndex, MethodInfo asSpan, MethodInfo appendSpan)
    {
        for (int i = startIndex; i + 1 < codes.Count; i++)
        {
            if (IsCall(codes[i], asSpan) && IsCall(codes[i + 1], appendSpan))
            {
                return i;
            }
        }

        return -1;
    }

    private static bool IsIntConstant(CodeInstruction code, int value)
    {
        return (code.opcode == OpCodes.Ldc_I4 || code.opcode == OpCodes.Ldc_I4_S) && Convert.ToInt32(code.operand) == value;
    }

    private static bool IsRemovable(List<CodeInstruction> codes, int startIndex, int endIndex)
    {
        for (int i = startIndex; i <= endIndex; i++)
        {
            if (codes[i].labels.Count > 0 || codes[i].blocks.Count > 0)
            {
                return false;
            }
        }

        return true;
    }

    private static void Nop(List<CodeInstruction> codes, int startIndex, int endIndex)
    {
        for (int i = startIndex; i <= endIndex; i++)
        {
            codes[i].opcode = OpCodes.Nop;
            codes[i].operand = null;
        }
    }
}
