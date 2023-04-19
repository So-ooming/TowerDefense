using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveViewer : MonoBehaviour
{
    [SerializeField] private Image image;
    private string path = "EnemyImage/";
    private GameObject enemy;
    public Sprite chgimage;

    private void Awake()
    {
        enemy = GetComponent<GameObject>();
        chgimage = GetComponent<Sprite>();
    }
    public void ImageSet(GameObject enemy)
    {
        enemy = GameObject.FindWithTag("Enemy");
        string enemyName = enemy.name.Replace("(Clone)", "");
        chgimage = Resources.Load<Sprite>(path + enemyName);
        image.sprite = chgimage;
    }
}