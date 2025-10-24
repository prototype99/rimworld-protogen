using HarmonyLib;
using Verse;

namespace Zeus
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            Harmony harmony = new Harmony("com.zeus.taser");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(Projectile))]
    [HarmonyPatch("Impact")]
    public static class Projectile_Impact_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Projectile __instance, Thing hitThing)
        {
            if (__instance is Projectile_ZeusShock zeusProjectile)
            {
                zeusProjectile.DoTaserEffect(hitThing);
            }
        }
    }
}
