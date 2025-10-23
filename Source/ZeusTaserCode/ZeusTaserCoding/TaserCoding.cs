using Verse;

namespace Zeus
{
    public class ThingDef_ZeusShock : ThingDef
    {
        public const float AddHediffChance = 0.9f; //Default chance of adding hediff.
        public HediffDef HediffToAdd;

        public override void ResolveReferences()
        {
            HediffToAdd = HediffDefOfStuff.HediffDefOf.TaserShock;
        }
    }
}
