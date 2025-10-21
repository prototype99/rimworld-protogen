using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Zeus;
using HediffDefOfStuff;
using Verse;

namespace Zeus
{
    public class Projectile_ZeusShock: Bullet
    {
        #region Properties
        public ThingDef_ZeusShock Def
        {
            get
            {
                return this.def as ThingDef_ZeusShock;
            }
        }
        #endregion Properties

        #region Overrides
        protected override void Impact(Thing hitThing)
        {
            /* This is a call to the Impact method of the class we're inheriting from.
                * You can use your favourite decompiler to see what it does, but suffice to say
                * there are useful things in there, like damaging/killing the hitThing.
                */
            base.Impact(hitThing);
            /*
            * Null checking is very important in RimWorld.
            * 99% of errors reported are from NullReferenceExceptions (NREs).
            * Make sure your code checks if things actually exist, before they
            * try to use the code that belongs to said things.
            */
            if (Def != null && hitThing != null && hitThing is Pawn hitPawn)
            {
                var rand = Rand.Value;
                if (rand <= Def.AddHediffChance)
                {
                    Messages.Message("Bullet_ZeusShock_SuccessMessage".Translate(
                        this.launcher.Label, hitPawn.Label
                        ), MessageTypeDefOf.NeutralEvent);

                    var taserShockOnPawn = hitPawn.health?.hediffSet?.GetFirstHediffOfDef(Def.HediffToAdd);
                    var randomSeverity = Rand.Range(0.50f, 1f);
                    if (taserShockOnPawn != null)
                    {
                        taserShockOnPawn.Severity += randomSeverity;
                    }
                    else
                    {
                        Hediff hediff = HediffMaker.MakeHediff(Def.HediffToAdd, hitPawn);
                        hediff.Severity = randomSeverity;
                        hitPawn.health.AddHediff(hediff);
                    }
                }
                else
                {
                    MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "Bullet_ZeusShock_FailureMote".Translate(Def.AddHediffChance), 12f);
                }
            }
        }
        #endregion Overrides
    }
}
