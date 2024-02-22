using Microsoft.Xna.Framework;
using StardewValley;
using System;
using System.Runtime.CompilerServices;

namespace MessyCrops
{
	internal class CropPatch
	{
		const float pixelDepth = .0001f;

		internal static readonly ConditionalWeakTable<Crop, Tuple<Vector2>> offsets = new();

		internal static void Setup()
		{
			ModEntry.harmony.Patch(typeof(Crop).GetMethod(nameof(Crop.updateDrawMath)), postfix: new(typeof(CropPatch), nameof(AddToList)));
		}

		public static void AddToList(Crop __instance, Vector2 tileLocation)
		{
			if (!offsets.TryGetValue(__instance, out var offset))
				offsets.Add(__instance, offset = new(GetOffset(__instance)));

			if (ModEntry.config.ApplyToTrellis || !__instance.raisedSeeds.Value)
			{
				__instance.layerDepth = ((tileLocation.Y * 64f + 32f + offset.Item1.Y) * pixelDepth + (tileLocation.X % 5) * .00001f) / 
					((__instance.currentPhase.Value == 0 && __instance.shouldDrawDarkWhenWatered()) ? 2f : 1f);
				__instance.drawPosition += offset.Item1;
			}
		}

		public static Vector2 GetOffset(Crop _)
		{
			int amt = ModEntry.config.Amount;
			return new(Game1.random.Next(-amt, amt) * 4, Game1.random.Next(-amt, amt) * 4);
		}
	}
}
