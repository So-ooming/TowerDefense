using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Stage_Data stagedata;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private GameObject[] Enemys;

    private List<EnemyControl> enemyList;
    public List<EnemyControl> EnemyList => enemyList;

    private int enemyCount = 10;
    // Start is called before the first frame update
    void Awake()
    {
        enemyList = new List<EnemyControl>();
        Instantiate_Enemy();
        StartCoroutine("SpawnEnemy");
    }

    private void Instantiate_Enemy()
    {
        Enemys = new GameObject[enemyCount];

        for (int i = 0; i < Enemys.Length; i++)
        {
            Vector3 poolPosition = new Vector3(stagedata.LimitMax.x + 1f, stagedata.LimitMax.y + 1f, 0f);
            GameObject enemy_ob = Instantiate(enemyPrefab, poolPosition, Quaternion.identity);
            EnemyControl enemy1 = enemy_ob.GetComponent<EnemyControl>();
            Enemys[i] = enemy_ob;
            enemyList.Add(enemy1); // 府胶飘俊 规陛 积己等 利 沥焊 历厘
            Enemys[i].SetActive(false);
        }
    }

    private IEnumerator SpawnEnemy()
    {
        for(int i = 0; i < enemyCount; i++)
        {
            Enemys[i].SetActive(true);
            GameObject clone = Enemys[i];
            EnemyControl enemy = clone.GetComponent<EnemyControl>();

            enemy.Setup(this, wayPoints);

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void DestroyEnemy()
    {

    }
}