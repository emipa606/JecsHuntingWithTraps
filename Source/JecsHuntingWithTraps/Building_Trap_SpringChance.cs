using HarmonyLib;
using RimWorld;
using Verse;

namespace JecsHuntingWithTraps;

[HarmonyPatch(typeof(Building_Trap), "SpringChance")]
public static class Building_Trap_SpringChance
{
    public static void Postfix(ref float __result, Pawn p)
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