using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    Movement2D movement2D;
    public Transform target;
    private float damage;

    public void Setup(Transform target, float damage)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;
        if (col.transform != target) return;
        col.GetComponent<EnemyInfo>().TakeDamage(damage);
        Destroy(gameObject);
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
}
