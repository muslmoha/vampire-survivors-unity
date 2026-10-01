using UnityEngine;
using VampireSurvivorsStarter.Enemies;

namespace VampireSurvivorsStarter
{
    public class MeteorBurstSpellDefinition : SpellDefinition
    {
        public MeteorBurstSpellDefinition()
        {
            Name = "Meteor Burst";
            Combo = "WWW";
            Damage = 42f;
            Radius = 4f;
            Cooldown = 5f;
        }

        public override void Cast(Transform caster, Vector3 targetPosition, float spellLevel)
        {
            for (int i = 0; i < 5; i++)
            {
                var offset = Random.insideUnitCircle * Radius;
                var meteorPosition = new Vector3(targetPosition.x + offset.x, targetPosition.y + 8f, targetPosition.z + offset.y);
                var meteor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                meteor.transform.position = meteorPosition;
                meteor.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                var rb = meteor.AddComponent<Rigidbody>();
                rb.useGravity = true;
                rb.mass = 5f;

                var impact = meteor.AddComponent<MeteorImpact>();
                impact.Initialize(Damage * spellLevel, Radius, 1.2f);
            }
        }
    }

    public class MeteorImpact : MonoBehaviour
    {
        private float damage;
        private float radius;
        private float lifetime;

        public void Initialize(float strikeDamage, float strikeRadius, float duration)
        {
            damage = strikeDamage;
            radius = strikeRadius;
            lifetime = duration;
        }

        private void Update()
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0f)
            {
                Detonate();
            }

            if (transform.position.y <= 0.5f)
            {
                Detonate();
            }
        }

        private void Detonate()
        {
            var hits = Physics.OverlapSphere(transform.position, radius);
            foreach (var collider in hits)
            {
                if (collider.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.TakeDamage(damage);
                }
            }

            Destroy(gameObject);
        }
    }
}
