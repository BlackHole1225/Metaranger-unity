using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("Maximum amount of health")] public float MaxHealth = 10f;
        [Tooltip("Maximum amount of armour")] public float MaxArmour;
        [Tooltip("Maximum amount of shields")] public float MaxShields;

        [Tooltip("Differentiates player from other Actors that use this script")]
        public bool isPlayer;


        [Tooltip("Health ratio at which the critical health vignette starts appearing")]
        public float CriticalHealthRatio = 0.3f;

        public UnityAction<float, GameObject> OnDamaged;
        public UnityAction<float> OnHealed;
        public UnityAction OnDie;

        public float CurrentHealth { get; set; }
        public float CurrentArmour { get; set; }
        public float CurrentShields { get; set; }
        public bool Invincible { get; set; }
        public bool CanPickup() => CurrentHealth < MaxHealth;

        public float GetRatio() => CurrentHealth / MaxHealth;
        public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

        bool m_IsDead;

        void Start()
        {
            CurrentHealth = MaxHealth;
            if (isPlayer)
            {
                // At this point, you would query the blockchain / user's tokens to see what they armour / shields are
                // Instead of setting them in the inspector like here
                if (MaxArmour > 0f) { CurrentArmour = 1f; };
                if (MaxShields > 0f) { CurrentShields = 1f; };
            }

        }

        public void Heal(float healAmount)
        {
            float healthBefore = CurrentHealth;
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            // call OnHeal action
            float trueHealAmount = CurrentHealth - healthBefore;
            if (trueHealAmount > 0f)
            {
                OnHealed?.Invoke(trueHealAmount);
            }
        }

        float CalculateDamage(string damageSourceName, string enemyVariation, float damage)
        {
            if (damageSourceName.Contains(enemyVariation))
            {
                return damage / 2;
            }

            return damage;
        }

        public void TakeDamage(float damage, GameObject damageSource)
        {

            string enemyName = damageSource.name;

            if (CurrentShields > 0f)
            {
                // Shields are resistant to the SpecialOps enemy type
                float shieldDamageTaken = CalculateDamage(enemyName, "SpecialOps", damage);
                CurrentShields -= shieldDamageTaken;
                CurrentShields = Mathf.Clamp(CurrentShields, 0f, MaxShields);
            }

            if (CurrentArmour > 0f)
            {

                // Armour is resistant to the Swarmer enemy type
                float armourDamageTaken = CalculateDamage(enemyName, "Swarmer", damage);
                CurrentArmour -= armourDamageTaken;
                CurrentArmour = Mathf.Clamp(CurrentArmour, 0f, MaxArmour);
            }

            float healthBefore = CurrentHealth;

            // Health is resistant to the Roller enemy type
            float damageTaken = CalculateDamage(enemyName, "Roller", damage);
            CurrentHealth -= damageTaken;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            // call OnDamage action
            float trueDamageAmount = healthBefore - CurrentHealth;
            if (trueDamageAmount > 0f)
            {
                OnDamaged?.Invoke(trueDamageAmount, damageSource);
            }

            HandleDeath();
        }

        public void Kill()
        {
            CurrentHealth = 0f;

            // call OnDamage action
            OnDamaged?.Invoke(MaxHealth, null);

            HandleDeath();
        }

        void HandleDeath()
        {
            if (m_IsDead)
                return;

            // call OnDie action
            if (CurrentHealth <= 0f)
            {
                m_IsDead = true;
                OnDie?.Invoke();
            }
        }
    }
}