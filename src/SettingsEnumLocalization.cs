using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Core;
using HarmonyLib;
using KSA;
using KSA.Rendering.Lighting;

namespace KsaLocFramework;

internal static class SettingsEnumLocalization
{
    public static readonly Type[] Types =
    {
        typeof(GameSettings.DisplayMode),
        typeof(GameSettings.AntiAliasing),
        typeof(FrameQueueLimit),
        typeof(GameSettings.Tonemapper),
        typeof(GameSettings.CloudShadows),
        typeof(CascadedShadowSystem.Filter),
        typeof(GameSettings.KittenFurQuality),
        typeof(GameSettings.ParticleQuality),
        typeof(GameSettings.ExplosionQuality),
        typeof(GameSettings.DeformationQuality),
        typeof(GameSettings.AmbientOcclusionQuality),
        typeof(GameSettings.ShadowSlots),
        typeof(GameSettings.OceanSimulationQuality),
        typeof(GameSettings.ExhaustQuality),
        typeof(GameSettings.CloudQuality),
        typeof(GameSettings.MilkyWayQuality)
    };

    private static readonly HashSet<MethodInfo> EnumNameMethods = CreateEnumNameMethods();

    public static void LocalizeNameCalls(List<CodeInstruction> codes)
    {
        InsertCalls(
            codes,
            EnumNameMethods.Contains,
            AccessTools.Method(typeof(SettingsUiText), nameof(SettingsUiText.LocalizeEnumName)));
    }

    public static void LocalizeInputActionNameCalls(List<CodeInstruction> codes)
    {
        MethodInfo getInputActionName = AccessTools.Method(
            typeof(EnumCollection<InputAction, string>),
            "GetName",
            new[] { typeof(InputAction) });
        InsertCalls(
            codes,
            method => method == getInputActionName,
            AccessTools.Method(typeof(SettingsUiText), nameof(SettingsUiText.LocalizeInputActionName)));
    }

    private static void InsertCalls(List<CodeInstruction> codes, Predicate<MethodInfo> isTarget, MethodInfo replacement)
    {
        for (int i = 0; i < codes.Count; i++)
        {
            if ((codes[i].opcode == OpCodes.Call || codes[i].opcode == OpCodes.Callvirt) &&
                codes[i].operand is MethodInfo method && isTarget(method))
            {
                codes.Insert(i + 1, new CodeInstruction(OpCodes.Call, replacement));
                i++;
            }
        }
    }

    private static HashSet<MethodInfo> CreateEnumNameMethods()
    {
        HashSet<MethodInfo> methods = new HashSet<MethodInfo>();
        foreach (Type enumType in Types)
        {
            Type collectionType = typeof(EnumCollection<,>).MakeGenericType(enumType, typeof(string));
            Type stringCollectionType = typeof(StringEnumCollection<>).MakeGenericType(enumType);
            methods.Add(AccessTools.Method(collectionType, "GetName", new[] { enumType }));
            methods.Add(AccessTools.Method(collectionType, "GetNameFromIndex", new[] { typeof(int) }));
            methods.Add(AccessTools.Method(stringCollectionType, "GetNameFromIndex", new[] { typeof(int) }));
        }
        return methods;
    }
}
