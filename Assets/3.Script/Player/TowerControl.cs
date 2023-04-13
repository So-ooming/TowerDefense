using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    // 자동 공격 간격 조절을 위한 timer 및 waitingTime
    private float timer = 0.0f;
    [SerializeField] private float waitingTime;

    private Weapon weapon;

    private void Awake()
    {
        weapon = transform.GetComponent<Weapon>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            weapon.StartFire();
            weapon.StopFire();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        timer += Time.deltaTime;
        if (col.CompareTag("Enemy"))
        {
            if (timer > waitingTime)
            {
                weapon.StartFire();
                weapon.StopFire();
                timer = 0.0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        weapon.StopFire();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            weapon.StartFire();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            weapon.StopFire();
        }
    }
}
