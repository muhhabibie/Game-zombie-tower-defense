using System.Collections.Generic;
using UnityEngine;

namespace LastBastion.Relics
{
    public class RelicManager : MonoBehaviour
    {
        private const string PlayerPrefsKey = "LastBastion.Relics";
        public List<Relic> acquired = new List<Relic>();

        public void Load()
        {
            string data = PlayerPrefs.GetString(PlayerPrefsKey, string.Empty);
            // For brevity, not implementing full serialization of ScriptableObjects
        }

        public void Save()
        {
            PlayerPrefs.SetString(PlayerPrefsKey, "");
            PlayerPrefs.Save();
        }

        public void Acquire(Relic relic)
        {
            if (relic == null) return;
            if (!acquired.Contains(relic))
            {
                acquired.Add(relic);
                relic.ApplyPermanent();
                Save();
            }
        }
    }
}