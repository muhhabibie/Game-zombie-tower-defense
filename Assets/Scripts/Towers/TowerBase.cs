using System.Linq;
using UnityEngine;

namespace LastBastion.Towers
{
    public abstract class TowerBase : MonoBehaviour
    {
        [Header("Tower")]
        public float range = 5f;
        public float fireRate = 1.0f;
        public Transform muzzle;
        public LastBastion.Combat.Projectile projectilePrefab;
        public LayerMask enemyMask;

        [Header("Synergy")]
        public bool requirePlayerNearby = false;
        public float playerSynergyRadius = 4f;

        protected float nextFireTime;

        protected virtual void Update()
        {
            if (Time.time < nextFireTime) return;
            if (requirePlayerNearby && !IsPlayerNearby()) return;

            var target = AcquireTarget();
            if (target != null)
            {
                ShootAt(target);
                nextFireTime = Time.time + 1f / Mathf.Max(0.01f, fireRate);
            }
        }

        protected bool IsPlayerNearby()
        {
            var player = LastBastion.Core.GameManager.Instance.player;
            if (player == null) return false;
            return Vector2.Distance(player.transform.position, transform.position) <= playerSynergyRadius;
        }

        protected Transform AcquireTarget()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);
            if (colliders.Length == 0) return null;
            var closest = colliders
                .OrderBy(c => Vector2.SqrMagnitude(c.transform.position - transform.position))
                .First();
            return closest.transform;
        }

        protected virtual void ShootAt(Transform target)
        {
            if (projectilePrefab == null || muzzle == null) return;
            Vector2 dir = (target.position - muzzle.position).normalized;
            var proj = Instantiate(projectilePrefab, muzzle.position, Quaternion.identity);
            proj.Launch(dir);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, range);
            if (requirePlayerNearby)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, playerSynergyRadius);
            }
        }
#endif
    }
}