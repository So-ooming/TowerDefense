using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private bool isAlt; // Alt키 눌렀는지 확인하기 위한 변수
    Vector2 clickPoint; // 마우스가 클릭되었을 때 마우스 포지션 값을 받아오기 위한 변수
    float dragSpeed = 30.0f; // 드래그 스피드
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
