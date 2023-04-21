using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackToTarget}

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;       // �Ҹ� ������
    [SerializeField] private TowerTemplate towerTemplate;       // �Ҹ� ������
    /*[SerializeField] private float attackRate;              // ���� �ֱ�
    [SerializeField] private float attackRange;             // ���� ����
    [SerializeField] private float attackDamage = 1;*/        // ���ݷ�
    private int level = 0;                                  // Ÿ�� ����
    [SerializeField] private Transform spawnPoint;          // ���� ����Ʈ
    private WeaponState weaponState = WeaponState.SearchTarget;
    private Transform attackTarget = null;
    private EnemySpawner enemySpawner;
    private Tile ownerTile;
    [SerializeField] private GameObject head;

    private float improvedDamage = 0f;
    private float improvedRate = 0f;
    private float improvedRange = 0f;

    public float ImprovedDamage => improvedDamage;          // ���� ������
    public float ImprovedRate => improvedRate;              // ���� ���� �ֱ�
    public float ImprovedRange => improvedRange;            // ���� ���� ����

    /*public float AttackDamage => attackDamage;
    public float AttackRate => attackRate;
    public float AttackRange => attackRange;*/
    public Sprite TowerSprite => towerTemplate.weapon[0].sprite;
    public float AttackRate => towerTemplate.weapon[0].rate;
    public float AttackRange => towerTemplate.weapon[0].range;
    public float AttackDamage => towerTemplate.weapon[0].damage;
    public int Level => level + 1;

    public void Setup(EnemySpawner enemySpawner, Tile ownerTile)
    {
        this.enemySpawner = enemySpawner;
        this.ownerTile = ownerTile;
        SetupTower();
        ChangeState(WeaponState.SearchTarget);
    }

    public void SetupTower()
    {
        towerTemplate.weapon[0].damage = 1f;
        towerTemplate.weapon[0].rate = 0.5f;
        towerTemplate.weapon[0].range = 4f;
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
            // ���� ������ �ִ� ���� ã�� ���� ���� �Ÿ��� �ִ��� ũ�� ����
            float closestDistSqr = Mathf.Infinity; 

            // EnemySpawner�� EnemyList�� �ִ� ���� �ʿ� �����ϴ� ��� �� �˻�
            for(int i = 0; i < enemySpawner.EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                // ���� �˻� ���� ������ �Ÿ��� ���� ���� ���� �ְ�, ������� �˻��� ������ �Ÿ��� ������
                if (distance <= AttackRange && distance <= closestDistSqr)
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
            if(distance > AttackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // Attack_Rate �ð���ŭ ���
            yield return new WaitForSeconds(AttackRate);

            Fire();
        }
    }

    private void Fire()
    {
        GameObject clone = Instantiate(bulletPrefab, spawnPoint.position, head.transform.rotation);
        clone.GetComponent<BulletControl>().Setup(attackTarget, AttackDamage);
    }
    
    public void UpgradeTower(Weapon currentTower)
    {
        currentTower.level += 1;
        currentTower.improvedDamage += 2;
        currentTower.improvedRange += 0.02f;

        currentTower.towerTemplate.weapon[0].damage += 2;
        if (!(currentTower.AttackRate - 0.005f <= 0.3f))
        {
            currentTower.improvedRate -= 0.005f;
            currentTower.towerTemplate.weapon[0].rate -= 0.005f;
        }
        currentTower.towerTemplate.weapon[0].range += 0.02f;
    }

    public void IsBuildSetFalse()
    {
        ownerTile.isBuildTower = false;
    }
}
