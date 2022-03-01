using HarmonyLib;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace MessyCrops
{
    internal class HoeDirtPatch
    {
        public static void Setup()
        {
            HarmonyMethod prefix = new(typeof(HoeDirtPatch).MethodNamed("CropDestroyPrefix"));
            ModEntry.harmony.Patch(typeof(HoeDirt).MethodNamed("destroyCrop"), prefix);
            ModEntry.harmony.Patch(typeof(HoeDirt).MethodNamed("OnRemoved"), prefix);
        }
        public static void CropDestroyPrefix(HoeDirt __instance)
        {
            if(__instance.crop != null)
                CropPatch.offsets.Remove(__instance.crop);
        }
    }
}
