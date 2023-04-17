using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] private float MaxHP = 5;
    private float CurrentHP;
    private bool isDie = false;
    private EnemyControl enemy;
    private SpriteRenderer spriter;

    public float maxHP => MaxHP;
    public float currentHP => CurrentHP;

    private void Awake()
    {
        CurrentHP = MaxHP;
        enemy = GetComponent<EnemyControl>();
        spriter = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (isDie) return;

        CurrentHP -= damage;
        StopCoroutine("HitAnimation");
        StartCoroutine("HitAnimation");

        if(currentHP <= 0)
        {
            isDie = true;
            enemy.OnDie();
        }
    }

    private IEnumerator HitAnimation()
    {
        spriter.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriter.color = Color.white;
    }

}
