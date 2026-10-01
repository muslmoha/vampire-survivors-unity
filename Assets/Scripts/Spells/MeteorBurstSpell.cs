using UnityEngine;
using VampireSurvivorsStarter.Enemies;

namespace VampireSurvivorsStarter
{
    public class FrostNovaSpellDefinition : SpellDefinition
    {
        public FrostNovaSpellDefinition()
        {
            Name = "Frost Nova";
            Combo = "QWE";
            Damage = 20f;
            Radius = 5f;
            Cooldown = 3f;
        }

        public override void Cast(Transform caster, Vector3 targetPosition, float spellLevel)
        {
            var hits = Physics.OverlapSphere(targetPosition, Radius);
            foreach (var collider in hits)
            {
                if (collider.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.TakeDamage(Damage * spellLevel);
                }
            }

            var effect = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            effect.name = "FrostNovaEffect";
            effect.transform.position = targetPosition;
            effect.transform.localScale = new Vector3(Radius * 2f, Radius * 2f, Radius * 2f);
            Object.Destroy(effect.GetComponent<Collider>());
            var renderer = effect.GetComponent<Renderer>();
            if (renderer != null)
            {
                var color = new Color(0.4f, 0.7f, 1f, 0.4f);
                renderer.material.color = color;
            }

            Object.Destroy(effect, 0.35f);
        }
    }
}
