using System.Collections;
using UnityEngine;

namespace LastBastion.Skills.Skills
{
    [CreateAssetMenu(menuName = "LastBastion/Skills/Buff Tower Radius")] 
    public class BuffTowerRadiusSkill : SkillBase
    {
        public float buffMultiplier = 1.4f;
        public float buffDuration = 8f;
        public float affectRadius = 6f;

        protected override bool Activate(LastBastion.Player.PlayerController player)
        {
            if (player == null) return false;
            var towers = GameObject.FindObjectsOfType<LastBastion.Towers.TowerBase>();
            foreach (var t in towers)
            {
                if (Vector2.Distance(t.transform.position, player.transform.position) <= affectRadius)
                {
                    player.StartCoroutine(ApplyBuff(t));
                }
            }
            return true;
        }

        private IEnumerator ApplyBuff(LastBastion.Towers.TowerBase tower)
        {
            float originalRange = tower.range;
            tower.range = originalRange * buffMultiplier;
            yield return new WaitForSeconds(buffDuration);
            if (tower != null)
            {
                tower.range = originalRange;
            }
        }
    }
}