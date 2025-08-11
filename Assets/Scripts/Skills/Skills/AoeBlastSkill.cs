using UnityEngine;

namespace LastBastion.Skills.Skills
{
    [CreateAssetMenu(menuName = "LastBastion/Skills/AoE Blast")] 
    public class AoeBlastSkill : SkillBase
    {
        public float radius = 3.5f;
        public int damage = 40;
        public LayerMask enemyMask;

        protected override bool Activate(LastBastion.Player.PlayerController player)
        {
            if (player == null) return false;
            var hits = Physics2D.OverlapCircleAll(player.transform.position, radius, enemyMask);
            for (int i = 0; i < hits.Length; i++)
            {
                var dmg = hits[i].GetComponent<LastBastion.Player.IDamageable>();
                if (dmg != null) dmg.TakeDamage(damage);
            }
            return true;
        }
    }
}