using StardewModdingAPI;
using System;

namespace MessyCrops
{
    internal class Config
    {
        public int Amount { get; set; } = 4;
        public bool ApplyToTrellis { get; set; } = false;

        public void ResetToDefault()
        {
            Amount = 4;
            ApplyToTrellis = false;
        }
        public void ApplyConfig()
        {
            ModEntry.helper.WriteConfig(this);
        }
        public void RegisterConfig(IManifest manifest)
        {
            if (!ModEntry.helper.ModRegistry.IsLoaded("spacechase0.GenericModConfigMenu"))
                return;

            var api = ModEntry.helper.ModRegistry.GetApi<IGMCMAPI>("spacechase0.GenericModConfigMenu");

            api.Register(manifest, ResetToDefault, ApplyConfig, true);
            api.AddNumberOption(manifest,
                () => Amount,
                (s) => Amount = s,
                () => ModEntry.i18n.Get("config.offset.name"),
                () => ModEntry.i18n.Get("config.offset.desc"),
                0, 8
            );
            api.AddBoolOption(manifest,
                () => ApplyToTrellis,
                (s) => ApplyToTrellis = s,
                () => ModEntry.i18n.Get("config.applyTrellis.name"),
                () => ModEntry.i18n.Get("config.applyTrellis.desc")
            );
        }
    }
}
