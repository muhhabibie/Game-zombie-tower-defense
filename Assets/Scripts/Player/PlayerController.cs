using System.Collections;
using UnityEngine;

namespace LastBastion.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 6f;
        public float dashSpeed = 12f;
        public float dashDuration = 0.15f;
        public float dashCooldown = 1.5f;

        [Header("Combat")]
        public float meleeRange = 1.2f;
        public int meleeDamage = 20;
        public float meleeCooldown = 0.5f;
        public LayerMask enemyMask;

        [Header("Health")]
        public Health health;

        private Rigidbody2D rb;
        private Vector2 movementInput;
        private Vector2 lastAim = Vector2.right;
        private float nextMeleeTime;
        private bool isDashing;
        private float nextDashTime;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            if (health == null) health = GetComponent<Health>();
        }

        private void Update()
        {
            if (LastBastion.Core.GameManager.Instance.phase == LastBastion.Core.GamePhase.Defeat) return;

            movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (mouseWorld - transform.position);
            if (dir.sqrMagnitude > 0.01f) lastAim = dir.normalized;

            if (Input.GetMouseButton(0))
            {
                TryMelee();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                TryDash();
            }
        }

        private void FixedUpdate()
        {
            float speed = isDashing ? dashSpeed : moveSpeed;
            rb.velocity = movementInput * speed;
        }

        private void TryMelee()
        {
            if (Time.time < nextMeleeTime) return;
            nextMeleeTime = Time.time + meleeCooldown;

            Vector2 center = (Vector2)transform.position + lastAim * meleeRange * 0.5f;
            float radius = meleeRange * 0.6f;
            var hits = Physics2D.OverlapCircleAll(center, radius, enemyMask);
            for (int i = 0; i < hits.Length; i++)
            {
                var dmg = hits[i].GetComponent<IDamageable>();
                if (dmg != null)
                {
                    dmg.TakeDamage(meleeDamage);
                }
            }
        }

        private void TryDash()
        {
            if (Time.time < nextDashTime) return;
            StartCoroutine(DashRoutine());
        }

        private IEnumerator DashRoutine()
        {
            nextDashTime = Time.time + dashCooldown;
            isDashing = true;
            yield return new WaitForSeconds(dashDuration);
            isDashing = false;
        }

        public void OnDeath()
        {
            LastBastion.Core.GameManager.Instance.TriggerDefeat();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Vector2 center = Application.isPlaying ? (Vector2)transform.position + lastAim * meleeRange * 0.5f : (Vector2)transform.position + Vector2.right * meleeRange * 0.5f;
            Gizmos.DrawWireSphere(center, meleeRange * 0.6f);
        }
#endif
    }
}