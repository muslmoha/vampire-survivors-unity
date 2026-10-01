using System;
using System.Collections.Generic;

namespace VampireSurvivorsStarter
{
    public static class SpellFactory
    {
        private static readonly Dictionary<string, Func<SpellDefinition>> Registry = new Dictionary<string, Func<SpellDefinition>>
        {
            ["QQQ"] = () => new StoneSlabSpellDefinition(),
            ["QWE"] = () => new FrostNovaSpellDefinition(),
            ["WWW"] = () => new MeteorBurstSpellDefinition(),
            ["WQE"] = () => new GravityWellSpellDefinition(),
            ["EEE"] = () => new ChainLightningSpellDefinition()
        };

        public static bool TryCreate(string combo, out SpellDefinition spell)
        {
            if (Registry.TryGetValue(combo, out var factory))
            {
                spell = factory();
                return true;
            }

            spell = null;
            return false;
        }
    }
}
