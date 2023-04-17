using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private bool isAlt; // AltŰ �������� Ȯ���ϱ� ���� ����
    Vector2 clickPoint; // ���콺�� Ŭ���Ǿ��� �� ���콺 ������ ���� �޾ƿ��� ���� ����
    float dragSpeed = 30.0f; // �巡�� ���ǵ�
    [SerializeField] private Stage_Data stagedata;
    float width, height;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            isAlt = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
        {
            isAlt = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            clickPoint = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            if (isAlt)
            {
                Vector2 position = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);


                Vector2 move = position * (Time.deltaTime * dragSpeed);

                transform.Translate(move);
                transform.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, stagedata.LimitMin.x, stagedata.LimitMax.x),
            Mathf.Clamp(transform.position.y, stagedata.LimitMin.y, stagedata.LimitMax.y),
            transform.position.z
            );
    }
}
