using System;
using System.Collections.Generic;
using UnityEngine;

namespace LastBastion.UI
{
    public class UpgradeUI : MonoBehaviour
    {
        private Action<LastBastion.Upgrades.UpgradeOption> onPick;
        private List<LastBastion.Upgrades.UpgradeOption> currentOpts;

        public void Show(List<LastBastion.Upgrades.UpgradeOption> options, Action<LastBastion.Upgrades.UpgradeOption> onPicked)
        {
            currentOpts = options;
            onPick = onPicked;
            gameObject.SetActive(true);
            // TODO: populate UI buttons
        }

        public void PickIndex(int idx)
        {
            if (currentOpts == null || idx < 0 || idx >= currentOpts.Count) return;
            onPick?.Invoke(currentOpts[idx]);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            currentOpts = null;
            onPick = null;
        }
    }
}