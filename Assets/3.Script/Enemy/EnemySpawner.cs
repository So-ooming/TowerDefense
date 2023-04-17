using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyHPPrefab;
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform CanvasTransfrom;
    [SerializeField] private Transform[] wayPoints;

    private List<EnemyControl> enemyList;
    public List<EnemyControl> EnemyList => enemyList;

    void Awake()
    {
        enemyList = new List<EnemyControl>();
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemyPrefab);
            EnemyControl enemy = clone.GetComponent<EnemyControl>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);

            SpawnEnemyHP(clone);

            yield return new WaitForSeconds(spawnTime);

        }
    }

    public void DestroyEnemy(EnemyControl enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemyHP(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPPrefab);


        sliderClone.transform.SetParent(CanvasTransfrom);
        sliderClone.transform.localScale = Vector3.one;


        sliderClone.GetComponent<EnemyHPPositionSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyInfo>());

    }
}