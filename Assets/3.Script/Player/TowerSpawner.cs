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

        if (tile.isBuildTower) return;            // ��ġ�� Ÿ�Ͽ� �̹� Ÿ���� ������ return;

        tile.isBuildTower = true;                 // ���� Ÿ�Ͽ� Ÿ�� �Ǽ� ���� true�� ����

        playerGold.CurrentGold -= towerBuildGold; // ���� ��� - Ÿ�� �Ǽ� ���

        GameObject clone = Instantiate(towerPrefab, tiletransform.position, Quaternion.identity);
        clone.GetComponent<Weapon>().Setup(enemySpawner);
    }
}
