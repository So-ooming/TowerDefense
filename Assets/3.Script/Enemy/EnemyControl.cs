using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDestroyType { Kill = 0, Arrive}

public class EnemyControl : MonoBehaviour
{
    private int wayPointCount;          // �̵� ��� ����
    private Transform[] wayPoints;      // �̵� ��� ����
    private int currentIndex = 0;       // ���� ���� ��ǥ �ε���
    private Movement2D movement2D;      // ������Ʈ �̵� ����
    private EnemySpawner enemySpawner;
    [SerializeField] private int gold = 10;

    [SerializeField] Animator animator;

    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;

        // �� �̵� ��� WayPoints ���� ����
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine("OnMove");
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.moveSpeed)
            {
                // ���� �̵� ���� ����
                NextMoveTo();
            }

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if(currentIndex < wayPointCount - 1)
        {
            
            // ���� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
            transform.position = wayPoints[currentIndex].position;
            // �̵� ���� ���� -> ���� ��ǥ ����(wayPoints)
            currentIndex++;
            if (currentIndex >= 6 && currentIndex < 8)
            {
                animator.SetBool("isRight", true);
            }
            else
            {
                animator.SetBool("isRight", false);
            }
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }

        else
        {
            // ���� ��ġ�� ������ wayPoints���
            gold = 0;
            OnDie(EnemyDestroyType.Arrive);
        }
    }
    public void OnDie(EnemyDestroyType type)
    {
        enemySpawner.DestroyEnemy(type, this, gold);
    }
}
