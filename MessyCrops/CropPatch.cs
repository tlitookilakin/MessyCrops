using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewModdingAPI.Utilities;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MessyCrops
{
    [HarmonyPatch(typeof(Crop))]
    internal class CropPatch
    {
        private static readonly FieldInfo drawpos = typeof(Crop).FieldNamed("drawPosition");
        private static readonly FieldInfo layerdepth = typeof(Crop).FieldNamed("layerDepth");
        internal static readonly Dictionary<Crop, Vector2> offsets = new();

        [HarmonyPatch("updateDrawMath")]
        [HarmonyPostfix]
        public static void AddToList(Crop __instance, Vector2 tileLocation)
        {
            var offset = offsets.GetOrAdd(__instance, GetOffset);
            if (ModEntry.config.ApplyToTrellis || !__instance.raisedSeeds.Value)
            {
                layerdepth.SetValue(__instance, (float)layerdepth.GetValue(__instance) + (offset.Y + ModEntry.config.Amount) * .0001f + tileLocation.X * .00001f);
                drawpos.SetValue(__instance, (Vector2)drawpos.GetValue(__instance) + offset);
            }
        }

        public static Vector2 GetOffset(Crop _)
        {
            int amt = ModEntry.config.Amount;
            return new(Game1.random.Next(-amt, amt) * 4, Game1.random.Next(-amt, amt) * 4);
        }
    }
}
