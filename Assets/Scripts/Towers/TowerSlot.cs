using UnityEngine;

namespace LastBastion.Towers
{
    public class TowerSlot : MonoBehaviour
    {
        public bool isOccupied;
        public TowerBase placedTower;

        public bool CanPlace => !isOccupied;

        public TowerBase Place(TowerBase towerPrefab)
        {
            if (!CanPlace || towerPrefab == null) return null;
            placedTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isOccupied = true;
            return placedTower;
        }
    }
}