using UnityEngine;

namespace LastBastion.Skills.Skills
{
    [CreateAssetMenu(menuName = "LastBastion/Skills/DashSkill")] 
    public class DashSkill : SkillBase
    {
        protected override bool Activate(LastBastion.Player.PlayerController player)
        {
            // Simulate immediate dash by briefly increasing velocity towards aim
            if (player == null) return false;
            // Let the PlayerController handle dash via Space normally; here we nudge with small impulse
            var rb = player.GetComponent<Rigidbody2D>();
            if (rb == null) return false;
            Vector2 aim = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).normalized;
            rb.AddForce(aim * 400f, ForceMode2D.Impulse);
            return true;
        }
    }
}