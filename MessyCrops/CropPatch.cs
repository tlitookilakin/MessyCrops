using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewModdingAPI.Utilities;
using StardewValley;
using System;
using System.Reflection;

namespace MessyCrops
{
    [HarmonyPatch(typeof(Crop))]
    internal class CropPatch
    {
        private static readonly FieldInfo drawpos = typeof(Crop).FieldNamed("drawPosition");
        private static readonly FieldInfo layerdepth = typeof(Crop).FieldNamed("layerDepth");

        [HarmonyPatch("updateDrawMath")]
        [HarmonyPostfix]
        public static void AddToList(Crop __instance, Vector2 tileLocation)
        {
            layerdepth.SetValue(__instance, (float)layerdepth.GetValue(__instance) + tileLocation.X * .0001f);
            if(ModEntry.config.ApplyToTrellis || !__instance.raisedSeeds.Value)
                drawpos.SetValue(__instance, (Vector2)drawpos.GetValue(__instance) + GetOffset(tileLocation));
        }

        public static Vector2 GetOffset(Vector2 pos)
        {
            int amt = ModEntry.config.Amount;
            Random random = new((int)(pos.X * pos.Y + pos.Y) * 77);
            return new(random.Next(-amt, amt) * 4, random.Next(-amt, amt) * 4);
        }
    }
}
