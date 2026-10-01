using System.Collections.Generic;
using UnityEngine;

namespace VampireSurvivorsStarter
{
    public class SpellInputController : MonoBehaviour
    {
        [SerializeField] private Transform castOrigin;
        [SerializeField] private float castRange = 18f;

        private readonly Queue<string> inputBuffer = new Queue<string>();
        private readonly SpellSlot[] slots = new SpellSlot[2];

        private void Awake()
        {
            slots[0] = new SpellSlot();
            slots[1] = new SpellSlot();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                RegisterInput("Q");

            if (Input.GetKeyDown(KeyCode.W))
                RegisterInput("W");

            if (Input.GetKeyDown(KeyCode.E))
                RegisterInput("E");

            if (Input.GetKeyDown(KeyCode.R))
                CastQueuedSpell();

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Tick(Time.deltaTime);
            }
        }

        private void RegisterInput(string key)
        {
            if (inputBuffer.Count >= 3)
            {
                inputBuffer.Dequeue();
            }

            inputBuffer.Enqueue(key);
        }

        private void CastQueuedSpell()
        {
            var combo = string.Concat(inputBuffer);
            if (string.IsNullOrEmpty(combo))
                return;

            if (!SpellFactory.TryCreate(combo, out var spell))
            {
                Debug.LogWarning($"Unknown spell combo: {combo}");
                inputBuffer.Clear();
                return;
            }

            var targetPoint = GetCastTarget();
            AssignToSlot(spell, targetPoint);
            inputBuffer.Clear();
        }

        private Vector3 GetCastTarget()
        {
            var castPoint = castOrigin != null ? castOrigin.position : transform.position;
            var forward = Vector3.forward;
            var target = castPoint + forward * castRange;

            var ray = Camera.main != null ? Camera.main.ScreenPointToRay(Input.mousePosition) : new Ray(castPoint, forward);
            if (Physics.Raycast(ray, out var hit, castRange * 2f))
            {
                target = hit.point;
            }

            return target;
        }

        private void AssignToSlot(SpellDefinition spell, Vector3 targetPoint)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (!slots[i].IsActive)
                {
                    slots[i].Assign(spell, this.transform, targetPoint);
                    return;
                }
            }

            slots[0].Assign(spell, this.transform, targetPoint);
        }
    }

    public class SpellSlot
    {
        public bool IsActive => currentSpell != null && cooldownRemaining > 0f;

        private SpellDefinition currentSpell;
        private float cooldownRemaining;
        private Transform caster;
        private Vector3 targetPoint;

        public void Assign(SpellDefinition spell, Transform newCaster, Vector3 castTarget)
        {
            currentSpell = spell;
            caster = newCaster;
            targetPoint = castTarget;
            cooldownRemaining = spell.Cooldown;
            spell.Cast(caster, targetPoint, 1f);
        }

        public void Tick(float deltaTime)
        {
            if (cooldownRemaining > 0f)
            {
                cooldownRemaining -= deltaTime;
            }

            if (cooldownRemaining <= 0f)
            {
                currentSpell = null;
                caster = null;
            }
        }
    }
}
