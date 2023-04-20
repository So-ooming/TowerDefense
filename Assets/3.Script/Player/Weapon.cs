using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackToTarget}

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;       // 불릿 프리팹
    [SerializeField] private float attackRate;              // 공격 주기
    [SerializeField] private float attackRange;             // 공격 범위
    [SerializeField] private Transform spawnPoint;          // 스폰 포인트
    [SerializeField] private float attackDamage = 1;        // 공격력
    private int level = 0;                                  // 타워 레벨
    private WeaponState weaponState = WeaponState.SearchTarget;
    private Transform attackTarget = null;
    private EnemySpawner enemySpawner;
    [SerializeField] private GameObject head;

    private float improvedDamage = 0f;
    private float improvedRate = 0f;
    private float improvedRange = 0f;

    public float ImprovedDamage => improvedDamage;          // 향상된 데미지
    public float ImprovedRate => improvedRate;              // 향상된 공격 주기
    public float ImprovedRange => improvedRange;            // 향상된 공격 범위

    public float AttackDamage => attackDamage;
    public float AttackRate => attackRate;
    public float AttackRange => attackRange;
    public int Level => level + 1;

    public void Setup(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;

        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        StopCoroutine(weaponState.ToString());
        weaponState = newState;
        StartCoroutine(weaponState.ToString());
    }

    private void Update()
    {
        if(attackTarget != null)
        {
            RotateTarget();
        }
    }

    public void RotateTarget()
    {
        float dx = attackTarget.position.x - transform.position.x;
        float dy = attackTarget.position.y - transform.position.y;

        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        head.transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            // 제일 가까이 있는 적을 찾기 위해 최초 거리를 최대한 크게 설정
            float closestDistSqr = Mathf.Infinity; 

            // EnemySpawner의 EnemyList에 있는 현재 맵에 존재하는 모든 적 검사
            for(int i = 0; i < enemySpawner.EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                // 현재 검사 중인 적과의 거리가 공격 범위 내에 있고, 현재까지 검사한 적보다 거리가 가까우면
                if (distance <= attackRange && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    attackTarget = enemySpawner.EnemyList[i].transform;
                }
            }

            if (attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }

            yield return null;
        }
    }

    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            // target이 있는지 검사 (다른 발사체에 의해 제거, Goal 지점까지 이동해 삭제 등)
            if (attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // target이 공격 범위 안에 있는지 검사 (공격 범위를 벗어나면 새로운 적 탐색)
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if(distance > attackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // Attack_Rate 시간만큼 대기
            yield return new WaitForSeconds(attackRate);

            Fire();
        }
    }

    private void Fire()
    {
        GameObject clone = Instantiate(bulletPrefab, spawnPoint.position, head.transform.rotation);
        clone.GetComponent<BulletControl>().Setup(attackTarget, attackDamage);
    }
    
    public void UpgradeTower(Weapon currentTower)
    {
        currentTower.level += 1;
        currentTower.improvedDamage += 2;
        currentTower.improvedRange += 0.02f;

        currentTower.attackDamage += 2;
        if (!(currentTower.attackRate - 0.005f <= 0.3f))
        {
            currentTower.improvedRate -= 0.005f;
            currentTower.attackRate -= 0.005f;
        }
        currentTower.attackRange += 0.02f;
    }
}
