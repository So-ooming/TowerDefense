using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerTemplate : ScriptableObject
{
    public GameObject towerPrefab;
    public GameObject followTowerPrefab;
    public TowerWeapon[] weapon;

    [System.Serializable]
    public struct TowerWeapon
    {
        public Sprite sprite;
        public float damage;
        public float rate;
        public float range;
        public int cost;
    }
}
