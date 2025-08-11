using System;
using System.Collections.Generic;
using UnityEngine;

namespace LastBastion.Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        public List<UpgradeOption> pool = new List<UpgradeOption>();
        public LastBastion.UI.UpgradeUI ui;

        private Action onSelectedCallback;

        public void ShowChoices(int count, Action onSelected)
        {
            onSelectedCallback = onSelected;
            var picks = PickRandomDistinct(count);
            ui?.Show(picks, OnOptionSelected);
        }

        private void OnOptionSelected(UpgradeOption opt)
        {
            if (opt != null) opt.Apply();
            ui?.Hide();
            onSelectedCallback?.Invoke();
            onSelectedCallback = null;
        }

        private List<UpgradeOption> PickRandomDistinct(int count)
        {
            List<UpgradeOption> result = new List<UpgradeOption>();
            List<UpgradeOption> copy = new List<UpgradeOption>(pool);
            for (int i = 0; i < count && copy.Count > 0; i++)
            {
                int idx = UnityEngine.Random.Range(0, copy.Count);
                result.Add(copy[idx]);
                copy.RemoveAt(idx);
            }
            return result;
        }
    }
}