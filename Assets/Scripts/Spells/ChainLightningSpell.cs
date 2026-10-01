using UnityEngine;
using VampireSurvivorsStarter.Enemies;

namespace VampireSurvivorsStarter
{
    public class GravityWellSpellDefinition : SpellDefinition
    {
        public GravityWellSpellDefinition()
        {
            Name = "Gravity Well";
            Combo = "WQE";
            Damage = 25f;
            Radius = 6f;
            Cooldown = 4f;
        }

        public override void Cast(Transform caster, Vector3 targetPosition, float spellLevel)
        {
            var well = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            well.name = "GravityWell";
            well.transform.position = targetPosition;
            well.transform.localScale = new Vector3(Radius * 2f, Radius * 2f, Radius * 2f);
            Object.Destroy(well.GetComponent<Collider>());

            var renderer = well.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = new Color(0.7f, 0.2f, 1f, 0.2f);
            }

            var controller = well.AddComponent<GravityWellEffect>();
            controller.Initialize(Damage * spellLevel, Radius, 2f);
        }
    }

    public class GravityWellEffect : MonoBehaviour
    {
        private float damage;
        private float radius;
        private float lifetime;

        public void Initialize(float spellDamage, float spellRadius, float duration)
        {
            damage = spellDamage;
            radius = spellRadius;
            lifetime = duration;
        }

        private void Update()
        {
            lifetime -= Time.deltaTime;
            var hits = Physics.OverlapSphere(transform.position, radius);
            foreach (var collider in hits)
            {
                if (collider.TryGetComponent<Enemy>(out var enemy))
                {
                    var direction = (transform.position - enemy.transform.position).normalized;
                    enemy.transform.position += direction * 2f * Time.deltaTime;
                    enemy.TakeDamage(damage * Time.deltaTime);
                }
            }

            if (lifetime <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
