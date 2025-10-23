using RimWorld;
using Verse;

namespace Zeus
{
    public class Projectile_ZeusShock: Projectile
    {
        #region Properties

        private ThingDef_ZeusShock Def => def as ThingDef_ZeusShock;

        #endregion Properties

        #region Overrides
        protected override void Impact(Thing hitThing)
        {
            /* This is a call to the Impact method of the class we're inheriting from.
                * You can use your favourite decompiler to see what it does, but suffice to say
                * there are useful things in there, like damaging/killing the hitThing.
                */
            try
            {
                base.Impact(hitThing);
            }
            catch (System.MissingMethodException mme)
            {
                Log.Warning("[ZeusTaserCoding] base.Impact missing at runtime; skipping base call to avoid crash. This may indicate an unexpected game API change. " + mme);
                // Minimal fallback: destroy projectile so it doesn't linger
                this.Destroy(DestroyMode.Vanish);
            }
            catch (System.Exception ex)
            {
                Log.Error("[ZeusTaserCoding] Unexpected error calling base.Impact: " + ex);
            }
            /*
            * Null checking is very important in RimWorld.
            * 99% of errors reported are from NullReferenceExceptions (NREs).
            * Make sure your code checks if things actually exist, before they
            * try to use the code that belongs to said things.
            */
            if (Def == null || !(hitThing is Pawn hitPawn)) return;
            float rand = Rand.Value;
            if (rand <= ThingDef_ZeusShock.AddHediffChance)
            {
                Messages.Message("Bullet_ZeusShock_SuccessMessage".Translate(
                    launcher.Label, hitPawn.Label
                ), MessageTypeDefOf.NeutralEvent);

                Hediff taserShockOnPawn = hitPawn.health?.hediffSet?.GetFirstHediffOfDef(Def.HediffToAdd);
                float randomSeverity = Rand.Range(0.15f, 0.30f);
                if (taserShockOnPawn != null)
                {
                    taserShockOnPawn.Severity += randomSeverity;
                }
                else
                {
                    Hediff hediff = HediffMaker.MakeHediff(Def.HediffToAdd, hitPawn);
                    hediff.Severity = randomSeverity;
                    hitPawn.health?.AddHediff(hediff);
                }
            }
            else
            {
                MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "Bullet_ZeusShock_FailureMote".Translate(ThingDef_ZeusShock.AddHediffChance), 12f);
            }
        }
        #endregion Overrides
    }
}
