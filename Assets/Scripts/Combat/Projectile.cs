using UnityEngine;

namespace LastBastion.Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        public float speed = 12f;
        public int damage = 10;
        public float lifetime = 4f;
        public LayerMask hitMask;

        private Rigidbody2D rb;
        private float spawnTime;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Launch(Vector2 direction)
        {
            spawnTime = Time.time;
            rb.velocity = direction.normalized * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & hitMask.value) == 0) return;
            var dmg = other.GetComponent<LastBastion.Player.IDamageable>();
            if (dmg != null)
            {
                dmg.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (Time.time - spawnTime > lifetime)
            {
                Destroy(gameObject);
            }
        }
    }
}