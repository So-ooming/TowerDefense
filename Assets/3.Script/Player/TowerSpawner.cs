using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private EnemySpawner enemySpawner;

    public void SpawnTower(Transform tiletransform)
    {
        Tile tile = tiletransform.GetComponent<Tile>();

        if (tile.isBuildTower) return;

        tile.isBuildTower = true;

        GameObject clone = Instantiate(towerPrefab, tiletransform.position, Quaternion.identity);
        clone.GetComponent<Weapon>().Setup(enemySpawner);
    }
}
