using UnityEngine;

namespace LastBastion.Relics
{
    [CreateAssetMenu(menuName = "LastBastion/Relic")] 
    public class Relic : ScriptableObject
    {
        public string relicId;
        public string title;
        [TextArea]
        public string description;

        public void ApplyPermanent()
        {
            // Example: grant +10 starting HP permanently
            // Real behavior can be per relic and read on new run
        }
    }
}