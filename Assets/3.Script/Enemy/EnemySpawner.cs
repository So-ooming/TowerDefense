using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyHPPrefab;
    //[SerializeField] private float spawnTime;
    [SerializeField] private PlayerHP playerHP;
    [SerializeField] private Transform CanvasTransfrom;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private PlayerGold playerGold;
    private Wave currentWave;

    private List<EnemyControl> enemyList;
    public List<EnemyControl> EnemyList => enemyList;

    void Awake()
    {
        enemyList = new List<EnemyControl>();
        //StartCoroutine("SpawnEnemy");
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave;
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        int spawnEnemyCount = 0;
        //while (true)
        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            // GameObject clone = Instantiate(enemyPrefab);
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            EnemyControl enemy = clone.GetComponent<EnemyControl>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);

            SpawnEnemyHP(clone);
            spawnEnemyCount++;

            yield return new WaitForSeconds(currentWave.spawnTime);
 
        }
    }

    public void DestroyEnemy(EnemyDestroyType type, EnemyControl enemy, int gold)
    {
        // Enemy가 목표 지점에 골인했을 때
        if(type == EnemyDestroyType.Arrive)
        {
            playerHP.TakeDamage(1);
        }

        else if(type == EnemyDestroyType.Kill)
        {
            playerGold.CurrentGold += gold;
        }

        // 리스트에서 사망한 enemy 정보 삭제
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