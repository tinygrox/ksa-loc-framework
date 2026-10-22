using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Brutal.Localization;
using Core;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

[HarmonyPatch(typeof(VehicleLaunchMenu))]
public static class VehicleLaunchMenuPatch
{
    [HarmonyPatch("DrawLaunchUi")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawLaunchUiTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_LaunchVehicle));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleLaunchMenu_FillAllSeats));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleLaunchMenu_FillSeats));
        TranspilerTools.ReplaceLocalizedTwoValueConcatenation(codes, nameof(GameStrings.KLF_VehicleLaunchMenu_SeatLabel));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_NoCrewAssigned));
        TranspilerTools.ReplaceLocalizedLiteral(codes, nameof(GameStrings.KLF_VehicleEditor_NoCrewMessage));
        ReplaceSaveCount(codes);
        TranspilerTools.ReplaceUtf8ImStringLabels(codes);
        return codes;
    }

    private static void ReplaceSaveCount(List<CodeInstruction> codes)
    {
        MethodInfo asSpan = AccessTools.Method(typeof(MemoryExtensions), nameof(MemoryExtensions.AsSpan), new[] { typeof(string) });
        MethodInfo appendSpan = AccessTools.Method(typeof(SpanBuilder), nameof(SpanBuilder.Append), new[] { typeof(ReadOnlySpan<char>) });
        MethodInfo format = AccessTools.Method(typeof(TranspilerTools), nameof(TranspilerTools.FormatLocalized), new[] { typeof(string), typeof(LString) });
        MethodInfo toText = AccessTools.Method(typeof(int), nameof(int.ToString), Type.EmptyTypes);

        for (int i = 6; i + 2 < codes.Count; i++)
        {
            if (codes[i].opcode != OpCodes.Ldstr || (string)codes[i].operand != " SAVES")
            {
                continue;
            }

            if (!IsCall(codes[i + 1], asSpan) || !IsCall(codes[i + 2], appendSpan))
            {
                continue;
            }

            LocalBuilder builder = codes[i - 1].operand as LocalBuilder;
            if (builder == null || codes[i - 1].opcode != OpCodes.Ldloca_S)
            {
                continue;
            }

            if (codes[i - 2].opcode != OpCodes.Call || codes[i - 2].operand is not MethodInfo append || append.Name != nameof(SpanBuilder.Append) || append.DeclaringType != typeof(SpanBuilder))
            {
                continue;
            }

            int start = -1;
            for (int b = i - 2; b >= 0; b--)
            {
                if (codes[b].opcode == OpCodes.Ldloca_S && codes[b].operand is LocalBuilder local && local == builder)
                {
                    start = b;
                    break;
                }
            }

            if (start < 0)
            {
                continue;
            }

            int valueEnd = i - 6;
            if (valueEnd <= start)
            {
                continue;
            }

            List<CodeInstruction> replacement = new List<CodeInstruction>
            {
                codes[start]
            };
            replacement.AddRange(codes.GetRange(start + 1, valueEnd - start));
            replacement.Add(new CodeInstruction(OpCodes.Call, toText));
            replacement.Add(new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(GameStrings), nameof(GameStrings.KLF_VehicleLaunchMenu_SavesSuffix))));
            replacement.Add(new CodeInstruction(OpCodes.Call, format));
            replacement.Add(new CodeInstruction(OpCodes.Call, asSpan));
            replacement.Add(new CodeInstruction(OpCodes.Call, appendSpan));

            codes.RemoveRange(start, i + 2 - start + 1);
            codes.InsertRange(start, replacement);
            return;
        }
    }

    private static bool IsCall(CodeInstruction code, MethodInfo target)
    {
        return code.opcode == OpCodes.Call && code.operand is MethodInfo method && method == target;
    }
}
