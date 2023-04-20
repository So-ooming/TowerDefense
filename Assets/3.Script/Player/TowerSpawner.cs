using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private int towerBuildGold = 20;
    [SerializeField] private PlayerGold playerGold;
    
    public int TowerBuildGold => towerBuildGold;

    public void SpawnTower(Transform tileTransform)
    {
        if(towerBuildGold > playerGold.CurrentGold)
        {
            return;
        }

        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.isBuildTower) return;            // 설치할 타일에 이미 타워가 있으면 return;

        tile.isBuildTower = true;                 // 현재 타일에 타워 건설 여부 true로 변경

        playerGold.CurrentGold -= towerBuildGold; // 현재 골드 - 타워 건설 비용

        //Vector3 position = tileTransform.position + Vector3.back;

        GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);
        clone.GetComponent<Weapon>().Setup(enemySpawner);
    }

    public void IsBuildSetFalse()
    {
        Tile tile = GetComponent<Tile>();
        tile.isBuildTower = false;
    }
}
