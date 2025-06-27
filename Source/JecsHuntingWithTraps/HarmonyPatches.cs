using System.Reflection;
using HarmonyLib;
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
        new Harmony("rimworld.jecrell.huntingwithtraps").PatchAll(Assembly.GetExecutingAssembly());
    }
}