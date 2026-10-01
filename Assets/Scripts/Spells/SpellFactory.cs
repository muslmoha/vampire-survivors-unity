using UnityEngine;

namespace VampireSurvivorsStarter
{
    public abstract class SpellDefinition
    {
        public string Name { get; protected set; }
        public string Combo { get; protected set; }
        public float Damage { get; protected set; }
        public float Radius { get; protected set; }
        public float Cooldown { get; protected set; }

        public abstract void Cast(Transform caster, Vector3 targetPosition, float spellLevel);
    }
}
