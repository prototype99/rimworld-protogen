using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using HediffDefOfStuff;
using Verse;

namespace Zeus
{
    public class ThingDef_ZeusShock : ThingDef
    {
        public float AddHediffChance = 1f; //Default chance of adding hediff.
        public HediffDef HediffToAdd;

        public override void ResolveReferences()
        {
            HediffToAdd = HediffDefOfStuff.HediffDefOf.TaserShock;
        }
    }
}
