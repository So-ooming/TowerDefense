using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPPositionSetter : MonoBehaviour
{
    [SerializeField] private Vector3 distance = Vector3.up * 35f;

    private Transform targetTransform;
    private RectTransform UItransform;

    public void Setup(Transform target)
    {
        targetTransform = target;
        UItransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 ScreenPostion = Camera.main.WorldToScreenPoint(targetTransform.position);

        UItransform.position = ScreenPostion + distance;
    }
}
