using HarmonyLib;
using RimWorld;
using Verse;

namespace JecsHuntingWithTraps;

[HarmonyPatch(typeof(Building_Trap), nameof(Building_Trap.KnowsOfTrap))]
public static class Building_Trap_KnowsOfTrap
{
    /// <summary>
    ///     Makes animals return "false" for knows of trap.
    ///     Runs through a KnowsOfTrap check twice.
    /// </summary>
    public static void Postfix(ref bool __result, Pawn p)
    {
        if (p.Faction != null || !p.RaceProps.Animal || p.InAggroMentalState)
        {
            return;
        }

        //Log.Message("Triggered2");
        __result = false; // Was true
    }
}