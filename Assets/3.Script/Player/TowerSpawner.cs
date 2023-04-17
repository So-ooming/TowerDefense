using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private int towerBuildGold = 20;
    [SerializeField] private PlayerGold playerGold;

    public void SpawnTower(Transform tiletransform)
    {
        if(towerBuildGold > playerGold.CurrentGold)
        {
            return;
        }

        Tile tile = tiletransform.GetComponent<Tile>();

        if (tile.isBuildTower) return;            // 설치할 타일에 이미 타워가 있으면 return;

        tile.isBuildTower = true;                 // 현재 타일에 타워 건설 여부 true로 변경

        playerGold.CurrentGold -= towerBuildGold; // 현재 골드 - 타워 건설 비용

        GameObject clone = Instantiate(towerPrefab, tiletransform.position, Quaternion.identity);
        clone.GetComponent<Weapon>().Setup(enemySpawner);
    }
}
