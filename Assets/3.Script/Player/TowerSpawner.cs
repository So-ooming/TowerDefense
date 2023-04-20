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

        if (tile.isBuildTower) return;            // ��ġ�� Ÿ�Ͽ� �̹� Ÿ���� ������ return;

        tile.isBuildTower = true;                 // ���� Ÿ�Ͽ� Ÿ�� �Ǽ� ���� true�� ����

        playerGold.CurrentGold -= towerBuildGold; // ���� ��� - Ÿ�� �Ǽ� ���

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
