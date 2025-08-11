using UnityEngine;

namespace LastBastion.Upgrades
{
    public enum UpgradeType
    {
        Tower,
        Skill,
        Relic,
        StatBuff
    }

    [CreateAssetMenu(menuName = "LastBastion/Upgrade Option")] 
    public class UpgradeOption : ScriptableObject
    {
        public string title;
        [TextArea]
        public string description;
        public Sprite icon;
        public UpgradeType type;

        public LastBastion.Towers.TowerBase towerPrefab;
        public LastBastion.Skills.SkillBase skill;
        public LastBastion.Relics.Relic relic;

        public int bonusPlayerMaxHp;
        public float bonusMoveSpeed;
        public float bonusMeleeDamage;

        public void Apply()
        {
            var gm = LastBastion.Core.GameManager.Instance;
            switch (type)
            {
                case UpgradeType.Tower:
                    if (towerPrefab != null) gm.towerManager.towerPrefabs.Add(towerPrefab);
                    break;
                case UpgradeType.Skill:
                    if (skill != null)
                    {
                        // simple: fill Q then E
                        if (gm.skillManager.slotQ == null) gm.skillManager.slotQ = skill;
                        else gm.skillManager.slotE = skill;
                    }
                    break;
                case UpgradeType.Relic:
                    if (relic != null) gm.GetComponent<LastBastion.Relics.RelicManager>()?.Acquire(relic);
                    break;
                case UpgradeType.StatBuff:
                    var p = gm.player;
                    if (bonusPlayerMaxHp != 0)
                    {
                        p.health.maxHealth += bonusPlayerMaxHp;
                        p.health.Heal(bonusPlayerMaxHp);
                    }
                    if (bonusMoveSpeed != 0) p.moveSpeed += bonusMoveSpeed;
                    if (bonusMeleeDamage != 0) p.meleeDamage += Mathf.RoundToInt(bonusMeleeDamage);
                    break;
            }
        }
    }
}