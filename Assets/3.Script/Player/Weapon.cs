using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackToTarget}

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject Player_bullet;
    [SerializeField] private float Attack_Rate;
    [SerializeField] private float Attack_Range;
    private WeaponState weaponState = WeaponState.SearchTarget;
    private Transform attackTarget = null;
    private EnemySpawner enemySpawner;

    public void Setup(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;

        ChangeState(WeaponState.SearchTarget);
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            // ���� ������ �ִ� ���� ã�� ���� ���� �Ÿ��� �ִ��� ũ�� ����
            float closestDistSqr = Mathf.Infinity; 

            // EnemySpawner�� EnemyList�� �ִ� ���� �ʿ� �����ϴ� ��� �� �˻�
            for(int i = 0; i < enemySpawner.EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                // ���� �˻� ���� ������ �Ÿ��� ���� ���� ���� �ְ�, ������� �˻��� ������ �Ÿ��� ������
                if (distance <= Attack_Range && distance <= closestDistSqr)
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
            // target�� �ִ��� �˻� (�ٸ� �߻�ü�� ���� ����, Goal �������� �̵��� ���� ��)
            if (attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // target�� ���� ���� �ȿ� �ִ��� �˻� (���� ������ ����� ���ο� �� Ž��)
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if(distance > Attack_Range)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // Attack_Rate �ð���ŭ ���
            yield return new WaitForSeconds(Attack_Rate);

            StartFire();
        }
    }

    public void TryAttack()
    {
        Instantiate(Player_bullet, transform.position, Quaternion.identity);
    }
    private IEnumerator TryAttack_co()
    {
        while (true)
        {
            Instantiate(Player_bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Attack_Rate);
        }
    }

    public void StartFire()
    {
        StartCoroutine("TryAttack_co");
    }

    public void StopFire()
    {
        StopCoroutine("TryAttack_co");
    }
}
