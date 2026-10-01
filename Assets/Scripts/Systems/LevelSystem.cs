using UnityEngine;
using VampireSurvivorsStarter.Enemies;

namespace VampireSurvivorsStarter
{
    public class ChainLightningSpellDefinition : SpellDefinition
    {
        public ChainLightningSpellDefinition()
        {
            Name = "Chain Lightning";
            Combo = "EEE";
            Damage = 18f;
            Radius = 8f;
            Cooldown = 3.5f;
        }

        public override void Cast(Transform caster, Vector3 targetPosition, float spellLevel)
        {
            var enemies = FindObjectsOfType<Enemy>();
            foreach (var enemy in enemies)
            {
                var distance = Vector3.Distance(enemy.transform.position, targetPosition);
                if (distance <= Radius)
                {
                    enemy.TakeDamage(Damage * spellLevel);
                }
            }

            var effect = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            effect.transform.position = targetPosition;
            effect.transform.localScale = new Vector3(Radius * 2f, Radius * 2f, Radius * 2f);
            Object.Destroy(effect.GetComponent<Collider>());

            var renderer = effect.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = new Color(1f, 1f, 0.2f, 0.2f);
            }

            Object.Destroy(effect, 0.2f);
        }
    }
}
