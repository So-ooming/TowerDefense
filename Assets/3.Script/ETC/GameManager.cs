using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    AudioSource audio;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Debug.Log("�̹� ���� �Ŵ����� �����մϴ�.");
            Destroy(gameObject);
        }
        audio = transform.GetComponent<AudioSource>();
    }

    private void Start()
    {
        audio.Play();
    }
}
