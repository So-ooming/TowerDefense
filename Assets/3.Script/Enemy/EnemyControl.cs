using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDestroyType { Kill = 0, Arrive}

public class EnemyControl : MonoBehaviour
{
    private int wayPointCount;          // 이동 경로 개수
    private Transform[] wayPoints;      // 이동 경로 정보
    private int currentIndex = 0;       // 현재 지점 목표 인덱스
    private Movement2D movement2D;      // 오브젝트 이동 제어
    private EnemySpawner enemySpawner;
    [SerializeField] private int gold = 10;

    [SerializeField] Animator animator;

    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;

        // 적 이동 경로 WayPoints 정보 설정
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
                // 다음 이동 방향 설정
                NextMoveTo();
            }

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if(currentIndex < wayPointCount - 1)
        {
            
            // 적의 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoints[currentIndex].position;
            // 이동 방향 설정 -> 다음 목표 지점(wayPoints)
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
            // 현재 위치가 마지막 wayPoints라면
            gold = 0;
            OnDie(EnemyDestroyType.Arrive);
        }
    }
    public void OnDie(EnemyDestroyType type)
    {
        enemySpawner.DestroyEnemy(type, this, gold);
    }
}
