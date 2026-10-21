using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Brutal.ImGuiApi;
using Brutal.Numerics;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

// 修复高亮文本不会滚动跟随的问题
[HarmonyPatch(typeof(ActionStringToken), nameof(ActionStringToken.Draw))]
public static class ActionStringTokenPatch
{
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        MethodInfo cursorPosition = AccessTools.Method(typeof(ImGui), nameof(ImGui.GetCursorPos), Type.EmptyTypes);
        MethodInfo windowPosition = AccessTools.Method(typeof(ImGui), nameof(ImGui.GetWindowPos), Type.EmptyTypes);
        MethodInfo screenPosition = AccessTools.Method(typeof(ImGui), nameof(ImGui.GetCursorScreenPos), Type.EmptyTypes);

        for (int i = 0; i + 2 < codes.Count; i++)
        {
            if (!IsCall(codes[i], cursorPosition) ||
                !IsCall(codes[i + 1], windowPosition) ||
                !IsFloat2Addition(codes[i + 2]))
            {
                continue;
            }

            codes[i].operand = screenPosition;
            ToNop(codes[i + 1]);
            ToNop(codes[i + 2]);
            i += 2;
        }

        return codes;
    }

    private static bool IsCall(CodeInstruction code, MethodInfo target)
    {
        return code.opcode == OpCodes.Call && code.operand is MethodInfo method && method == target;
    }

    private static bool IsFloat2Addition(CodeInstruction code)
    {
        return code.opcode == OpCodes.Call &&
               code.operand is MethodInfo method &&
               method.IsStatic &&
               method.Name == "op_Addition" &&
               method.DeclaringType == typeof(float2);
    }

    private static void ToNop(CodeInstruction code)
    {
        code.opcode = OpCodes.Nop;
        code.operand = null;
    }
}
