using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] private Stage_Data stage_Data;
    Movement2D movement2D;
    private float destoryWeight = 1.0f;
    public Transform target;
    Vector2 speed = Vector2.zero;

    public void Setup(Transform target)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;
        if (col.transform != target) return;
        col.GetComponent<EnemyControl>().OnDie();
        Destroy(gameObject);
    }

    private void Awake()
    {
        //target = GameObject.FindGameObjectWithTag("Enemy");
        movement2D = GetComponent<Movement2D>();
    }

    private void FixedUpdate()
    {
        /*Vector2 direction = (Vector2)target.transform.position;
        transform.position = Vector2.SmoothDamp(transform.position, direction, ref speed, 0.5f);*/
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        //              
        else
        {
            Destroy(gameObject);
        }

    }

    private void LateUpdate()
    {
        if (transform.position.y < stage_Data.LimitMin.y - destoryWeight ||
            transform.position.y > stage_Data.LimitMax.y + destoryWeight ||
            transform.position.x < stage_Data.LimitMin.x - destoryWeight ||
            transform.position.x > stage_Data.LimitMax.x + destoryWeight)
        {
            Destroy(gameObject);
        }
    }
}
