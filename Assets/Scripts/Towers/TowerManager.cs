using System.Collections.Generic;
using UnityEngine;

namespace LastBastion.Towers
{
    public class TowerManager : MonoBehaviour
    {
        [Header("Placement")]        
        public KeyCode placeKey = KeyCode.Alpha1;
        public TowerBase selectedTowerPrefab;
        public LayerMask slotMask;
        public bool placementEnabled = true;

        [Header("Pool")]
        public List<TowerBase> towerPrefabs = new List<TowerBase>();

        public void EnablePlacement(bool enabled)
        {
            placementEnabled = enabled;
        }

        private void Update()
        {
            if (!placementEnabled) return;

            if (Input.GetKeyDown(placeKey))
            {
                // Toggle placement mode by selecting first in pool if not set
                if (selectedTowerPrefab == null && towerPrefabs.Count > 0)
                {
                    selectedTowerPrefab = towerPrefabs[0];
                }
            }

            if (selectedTowerPrefab != null && Input.GetMouseButtonDown(0))
            {
                TryPlaceAtMouse();
            }
        }

        private void TryPlaceAtMouse()
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.OverlapPoint(mouse, slotMask);
            if (hit == null) return;

            var slot = hit.GetComponent<TowerSlot>();
            if (slot != null && slot.CanPlace)
            {
                slot.Place(selectedTowerPrefab);
            }
        }
    }
}