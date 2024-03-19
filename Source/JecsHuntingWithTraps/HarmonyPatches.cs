using HarmonyLib;
using RimWorld;
using Verse;

namespace JecsHuntingWithTraps;

/*
 *
 *  Harmony Classes
 *  ===============
 *  Harmony is a system developed by pardeike (aka Brrainz).
 *  It allows us to use pre- / post-method patches instead of using detours.
 *
 */
[StaticConstructorOnStartup]
internal static class HarmonyPatches
{
    //Static Constructor
    /*
     * Contains 1 Harmony patch.
     * ===================
     *
     * [POSTFIX] Building_Trap -> KnowsOfTrap
     *
     */
    static HarmonyPatches()
    {
        var harmony = new Harmony("rimworld.jecrell.huntingwithtraps");
        harmony.Patch(AccessTools.Method(typeof(Building_Trap), "KnowsOfTrap"), null,
            new HarmonyMethod(typeof(HarmonyPatches).GetMethod("KnowsOfTrap_PostFix")));
        harmony.Patch(AccessTools.Method(typeof(Building_Trap), "SpringChance"), null,
            new HarmonyMethod(typeof(HarmonyPatches).GetMethod("SpringChance_PostFix")));
        //harmony.Patch(AccessTools.Method(typeof(Building_TrapDamager), "SpringSub"), new HarmonyMethod(typeof(HarmonyPatches).GetMethod("SpringSub_PreFix")), null);
    }

    /// <summary>
    ///     Makes animals return "false" for knows of trap.
    ///     Runs through a KnowsOfTrap check twice.
    /// </summary>
    public static void KnowsOfTrap_PostFix(ref bool __result, Pawn p)
    {
        if (p.Faction != null || !p.RaceProps.Animal || p.InAggroMentalState)
        {
            return;
        }

        //Log.Message("Triggered2");
        __result = false; // Was true
    }

    public static void SpringChance_PostFix(ref float __result, Pawn p)
    {
        if (!(p?.RaceProps?.Animal ?? false))
        {
            return;
        }

        if (p.Faction != Faction.OfPlayerSilentFail)
        {
            __result = 0.95f;
        }
    }
}