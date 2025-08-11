using UnityEngine;

namespace LastBastion.Skills
{
    public abstract class SkillBase : ScriptableObject
    {
        public string skillName;
        public Sprite icon;
        public float cooldown = 6f;

        private float nextReadyTime;

        public bool TryActivate(LastBastion.Player.PlayerController player)
        {
            if (Time.time < nextReadyTime) return false;
            bool ok = Activate(player);
            if (ok) nextReadyTime = Time.time + cooldown;
            return ok;
        }

        protected abstract bool Activate(LastBastion.Player.PlayerController player);
    }
}