using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;
using System.Reflection;
using UnityEngine;
using Verse.AI.Group;

namespace JecsHuntingWithTraps
{
    /*
     * 
     *  Harmony Classes
     *  ===============
     *  Harmony is a system developed by pardeike (aka Brrainz).
     *  It allows us to use pre/post method patches instead of using detours.
     * 
     */
    [StaticConstructorOnStartup]
    static class HarmonyPatches
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
            HarmonyInstance harmony = HarmonyInstance.Create("rimworld.jecrell.huntingwithtraps");
            harmony.Patch(AccessTools.Method(typeof(Building_Trap), "KnowsOfTrap"), null, new HarmonyMethod(typeof(HarmonyPatches).GetMethod("KnowsOfTrap_PostFix")), null);
            harmony.Patch(AccessTools.Method(typeof(Building_Trap), "SpringChance"), null, new HarmonyMethod(typeof(HarmonyPatches).GetMethod("SpringChance_PostFix")), null);
            harmony.Patch(AccessTools.Method(typeof(Building_TrapRearmable), "DamagePawn"), new HarmonyMethod(typeof(HarmonyPatches).GetMethod("DamagePawn_PreFix")), null);
        }

        /// <summary>
        /// Makes animals return "false" for knows of trap.
        /// Runs through a KnowsOfTrap check twice.
        /// </summary>
        public static void KnowsOfTrap_PostFix(Building_Trap __instance, ref bool __result, Pawn p)
        {
            if (p.Faction != null && !p.Faction.HostileTo(__instance.Faction))
            {
                __result = true;
                return;
            }
            if (p.Faction == null && p.RaceProps.Animal && !p.InAggroMentalState)
            {
                //Log.Message("Triggered2");
                __result = false; // Was true
                return;
            }
            if (p.guest != null && p.guest.Released)
            {
                __result = true;
                return;
            }
            Lord lord = p.GetLord();
            __result = p.guest != null && lord != null && lord.LordJob is LordJob_FormAndSendCaravan;
        }

        public static void SpringChance_PostFix(Building_Trap __instance, ref float __result, Pawn p)
        {
            if (p.Faction != null && !p.Faction.HostileTo(__instance.Faction))
            {
                __result = 0.001f;
                return;
            }           
            if ((p?.RaceProps?.Animal ?? false))
            {
                if (p?.Faction != Faction.OfPlayerSilentFail)
                {
                    __result = 0.95f;   
                }
            }
        }

        public static void DamagePawn_PreFix(Building_TrapRearmable __instance, Pawn p)
        {
            if ((p?.RaceProps?.Animal ?? false) && (p?.Faction != Faction.OfPlayerSilentFail))
            {
                BodyPartHeight height = (Rand.Value >= 0.666f) ? BodyPartHeight.Middle : BodyPartHeight.Top;
                int num = Mathf.RoundToInt(__instance.GetStatValue(StatDefOf.TrapMeleeDamage, true) * new FloatRange(6.0f, 8.0f).RandomInRange);
                int randomInRange = new IntRange(1, 3).RandomInRange;
                for (int i = 0; i < randomInRange; i++)
                {
                    if (num <= 0)
                    {
                        break;
                    }
                    int num2 = Mathf.Max(1, Mathf.RoundToInt(Rand.Value * (float)num));
                    num -= num2;
                    DamageInfo dinfo = new DamageInfo(DamageDefOf.Stab, num2, -1f, __instance, null, null, DamageInfo.SourceCategory.ThingOrUnknown);
                    if (Rand.Value > 0.05f)
                    {
                        dinfo.SetHitPart(p?.health?.hediffSet?.GetNotMissingParts()?.FirstOrDefault(
                        x => (x?.def?.tags?.Contains("MovingLimbCore") ?? false) ||
                             (x?.def?.tags?.Contains("MovingLimbSegment") ?? false)
                        ));
                    }
                    else
                    {
                        dinfo.SetHitPart(p?.health?.hediffSet?.GetNotMissingParts()?.FirstOrDefault(
                        x => (x?.def?.tags?.Contains("MovingLimbDigit") ?? false)
                        ));
                    }
                    if (dinfo.HitPart == null)
                    {
                        //Log.Message("Null");
                        dinfo.SetBodyRegion(height, BodyPartDepth.Outside);
                    }
                    p.TakeDamage(dinfo);
                }
            }
        }
    }
}
