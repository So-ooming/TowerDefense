using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPViewer : MonoBehaviour
{
    private EnemyInfo enemy;
    private Slider HPslider;

    public void Setup(EnemyInfo enemy)
    {
        this.enemy = enemy;
        TryGetComponent(out HPslider);
    }

    private void Update()
    {
        HPslider.value = enemy.currentHP / enemy.maxHP;
    }
}
