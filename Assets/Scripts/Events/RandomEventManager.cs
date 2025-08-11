using UnityEngine;

namespace LastBastion.Events
{
    public class RandomEventManager : MonoBehaviour
    {
        [Range(0f,1f)] public float chancePerWave = 0.25f;

        public void TryTriggerRandomEvent(int wave)
        {
            if (Random.value > chancePerWave) return;
            int pick = Random.Range(0, 3);
            switch (pick)
            {
                case 0: StartCoroutine(MeteorShower()); break;
                case 1: StartCoroutine(Blackout()); break;
                case 2: StartCoroutine(BossSpawn()); break;
            }
        }

        private System.Collections.IEnumerator MeteorShower()
        {
            Debug.Log("Meteor Shower event!");
            yield return null;
        }

        private System.Collections.IEnumerator Blackout()
        {
            Debug.Log("Blackout event!");
            yield return null;
        }

        private System.Collections.IEnumerator BossSpawn()
        {
            Debug.Log("Boss Wave event!");
            yield return null;
        }
    }
}