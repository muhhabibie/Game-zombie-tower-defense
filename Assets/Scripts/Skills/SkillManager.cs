using UnityEngine;

namespace LastBastion.Skills
{
    public class SkillManager : MonoBehaviour
    {
        public SkillBase slotQ;
        public SkillBase slotE;
        private LastBastion.Player.PlayerController player;
        private bool inputEnabled = true;

        private void Awake()
        {
            player = GetComponent<LastBastion.Player.PlayerController>();
            if (player == null)
            {
                player = LastBastion.Core.GameManager.Instance.player;
            }
        }

        public void EnableInput(bool enabled)
        {
            inputEnabled = enabled;
        }

        private void Update()
        {
            if (!inputEnabled) return;
            if (slotQ != null && Input.GetKeyDown(KeyCode.Q)) slotQ.TryActivate(player);
            if (slotE != null && Input.GetKeyDown(KeyCode.E)) slotE.TryActivate(player);
        }
    }
}