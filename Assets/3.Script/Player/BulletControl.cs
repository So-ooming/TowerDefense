using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] private Stage_Data stage_Data;
    private float destoryWeight = 2.0f;
    public GameObject target;
    private Rigidbody2D rb;
    public float speed = 5f;
    public float rotateSpeed = 200f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.transform.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = target.transform.up * speed;
        Debug.Log(target.transform.up);
        Debug.Log(rb.velocity);
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
