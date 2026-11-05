using UnityEngine;

public class TowerSelectionUI : MonoBehaviour
{
    public static GameObject SelectedTowerPrefab;

    public void SelectTower(GameObject towerPrefab)
    {
        if(towerPrefab == SelectedTowerPrefab)
        {
            SelectedTowerPrefab = null;
            return;
        }
        
        if(towerPrefab.GetComponent<Tower>().TowerPrice <= CoinManager.instance.Coins)
        {
            SelectedTowerPrefab = towerPrefab;
        }
    }
}
