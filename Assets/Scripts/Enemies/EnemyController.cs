using UnityEngine;

namespace LastBastion.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour, LastBastion.Player.IDamageable
    {
        public float moveSpeed = 2.5f;
        public int contactDamage = 10;
        public int maxHealth = 30;
        public float attackInterval = 1.0f;

        private int currentHealth;
        private Rigidbody2D rb;
        private Transform bastion;
        private float nextAttackTime;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            currentHealth = maxHealth;
        }

        public void Initialize(Transform bastionTarget, float hpMultiplier)
        {
            bastion = bastionTarget;
            currentHealth = Mathf.RoundToInt(maxHealth * hpMultiplier);
        }

        private void FixedUpdate()
        {
            if (bastion == null) return;
            Vector2 dir = ((Vector2)bastion.position - rb.position).normalized;
            rb.velocity = dir * moveSpeed;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform == bastion && Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + attackInterval;
                var bastionHealth = collision.gameObject.GetComponent<LastBastion.Structures.BastionHealth>();
                if (bastionHealth != null)
                {
                    bastionHealth.TakeDamage(contactDamage);
                }
            }
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            LastBastion.Core.GameManager.Instance.enemySpawner.NotifyEnemyKilled(this);
            Destroy(gameObject);
        }
    }
}