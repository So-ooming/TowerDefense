using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private EnemySpawner enemySpawner;
    private int currentWaveIndex = -1;

    public void StartWave()
    {
        if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length)
        {
            currentWaveIndex++;
            enemySpawner.StartWave(waves[currentWaveIndex]);
        }
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}