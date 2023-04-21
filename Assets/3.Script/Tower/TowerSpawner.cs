using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    /*[SerializeField] private GameObject towerPrefab;
    [SerializeField] private int towerBuildGold = 20;*/
    [SerializeField] private TowerTemplate towerTemplate;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerGold playerGold;

    //private SystemTextViewer 
    private bool isOnTowerButton = false;
    private GameObject followTowerClone = null;
    
    public int TowerBuildGold => towerTemplate.weapon[0].cost;

    public void SpawnTower(Transform tileTransform)
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
        if (!isOnTowerButton) return;
        /*if(TowerBuildGold > playerGold.CurrentGold)
        {
            return;
        }*/
        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.isBuildTower) return;            // ��ġ�� Ÿ�Ͽ� �̹� Ÿ���� ������ return;

        tile.isBuildTower = true;                 // ���� Ÿ�Ͽ� Ÿ�� �Ǽ� ���� true�� ����

        playerGold.CurrentGold -= TowerBuildGold; // ���� ��� - Ÿ�� �Ǽ� ���

        //Vector3 position = tileTransform.position + Vector3.back;

        GameObject clone = Instantiate(towerTemplate.towerPrefab, tileTransform.position, Quaternion.identity);
        clone.GetComponent<Weapon>().Setup(enemySpawner, tile);
        isOnTowerButton = false;
        Destroy(followTowerClone);
        StopCoroutine("OnTowerCancleSystem");
    }

    public void ReadyToSpawnTower()
    {
        if(towerTemplate.weapon[0].cost > playerGold.CurrentGold)
        {
            return;
        }

        isOnTowerButton = true;

        followTowerClone = Instantiate(towerTemplate.followTowerPrefab);
        StartCoroutine("OnTowerCancleSystem");
    }

    private IEnumerator OnTowerCancleSystem()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnTowerButton = false;

                Destroy(followTowerClone);
                break;
            }

            yield return null;
        }
    }
}
