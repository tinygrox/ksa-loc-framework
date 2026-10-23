using System;
using System.Collections.Generic;
using Brutal.Localization;

namespace KsaLocFramework;

public static class WindowTitleLocalizer
{
    private const string CpuProfilerPrefix = "CPU Profiler - ";

    private static readonly Dictionary<string, LString> Titles = new Dictionary<string, LString>(StringComparer.Ordinal)
    {
        ["Parachute Tuning"] = GameStrings.KLF_Title_ParachuteTuning,
        ["Crew Assignment"] = GameStrings.KLF_Menu_CrewAssignment,
        ["Distant Glint Editor"] = GameStrings.KLF_Title_DistantGlintEditor,
        ["Explosion Editor"] = GameStrings.KLF_Title_ExplosionEditor,
        ["GAME SAVES"] = GameStrings.KLF_Title_GameSaves,
        ["Context Assignments"] = GameStrings.KLF_Menu_ContextAssignments,
        ["Gauge Hierarchy"] = GameStrings.KLF_Title_GaugeHierarchy,
        ["Gauge Inspector"] = GameStrings.KLF_Title_GaugeInspector,
        ["Kitten Roster"] = GameStrings.KLF_Menu_KittenRoster,
        ["Kitten Locomotion Tuning"] = GameStrings.KLF_Title_KittenLocomotionTuning,
        ["LAYOUTS"] = GameStrings.KLF_Title_Layouts,
        ["Particle Emitter Editor"] = GameStrings.KLF_Title_ParticleEmitterEditor,
        ["Profiler"] = GameStrings.KLF_Menu_Profiler,
        ["Resource Groups"] = GameStrings.KLF_Title_ResourceGroups,
        ["Thread Profiler"] = GameStrings.KLF_Title_ThreadProfiler,
        ["Universe Manifest"] = GameStrings.KLF_Title_UniverseManifest,
        ["VEHICLE SAVES"] = GameStrings.KLF_Title_VehicleSaves,
        ["VERSION HISTORY"] = GameStrings.KLF_Title_VersionHistoryUpper
    };

    public static string Localize(string name)
    {
        if (name == null)
        {
            return null;
        }

        if (Titles.TryGetValue(name, out LString title))
        {
            return title.Format();
        }

        if (name.StartsWith(CpuProfilerPrefix, StringComparison.Ordinal))
        {
            return GameStrings.KLF_Title_CpuProfilerPrefix.Format() + name.Substring(CpuProfilerPrefix.Length);
        }

        return name;
    }
}
