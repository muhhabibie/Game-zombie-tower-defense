using System.Collections.Generic;
using UnityEngine;

namespace LastBastion.Utilities
{
    public static class WeightedRandomPicker
    {
        public static T Pick<T>(IList<T> items, IList<float> weights)
        {
            if (items == null || weights == null || items.Count == 0 || items.Count != weights.Count) return default;
            float total = 0f;
            for (int i = 0; i < weights.Count; i++) total += Mathf.Max(0f, weights[i]);
            float r = Random.value * total;
            float acc = 0f;
            for (int i = 0; i < items.Count; i++)
            {
                acc += Mathf.Max(0f, weights[i]);
                if (r <= acc) return items[i];
            }
            return items[items.Count - 1];
        }
    }
}