using UnityEngine;
using VampireSurvivorsStarter.Enemies;

namespace VampireSurvivorsStarter
{
    public class StoneSlabSpellDefinition : SpellDefinition
    {
        public StoneSlabSpellDefinition()
        {
            Name = "Stone Slab";
            Combo = "QQQ";
            Damage = 35f;
            Radius = 3.5f;
            Cooldown = 4f;
        }

        public override void Cast(Transform caster, Vector3 targetPosition, float spellLevel)
        {
            var slab = GameObject.CreatePrimitive(PrimitiveType.Cube);
            slab.name = "StoneSlab";
            slab.transform.position = targetPosition + Vector3.up * 4f;
            slab.transform.localScale = new Vector3(4f, 1f, 4f);

            var rb = slab.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.mass = 100f;
            rb.drag = 0.2f;

            var hitbox = slab.AddComponent<BoxCollider>();
            hitbox.isTrigger = false;

            float strikeDamage = Damage * spellLevel;
            float damageRadius = Radius;

            var strike = new GameObject("StoneSlabStrike");
            strike.transform.position = targetPosition;
            var hit = strike.AddComponent<StoneSlabHit>();
            hit.Initialize(strikeDamage, damageRadius, slab, 1.25f);
        }
    }

    public class StoneSlabHit : MonoBehaviour
    {
        private float damage;
        private float radius;
        private GameObject slab;
        private float lifetime;

        public void Initialize(float strikeDamage, float strikeRadius, GameObject slabObject, float duration)
        {
            damage = strikeDamage;
            radius = strikeRadius;
            slab = slabObject;
            lifetime = duration;
        }

        private void Update()
        {
            if (slab == null)
            {
                Destroy(gameObject);
                return;
            }

            lifetime -= Time.deltaTime;
            if (lifetime <= 0f)
            {
                Destroy(slab);
                Destroy(gameObject);
                return;
            }

            if (slab.transform.position.y <= 0.5f)
            {
                var hits = Physics.OverlapSphere(slab.transform.position, radius);
                foreach (var col in hits)
                {
                    if (col.TryGetComponent<Enemy>(out var enemy))
                    {
                        enemy.TakeDamage(damage);
                    }
                }

                Destroy(slab);
                Destroy(gameObject);
            }
        }
    }
}
