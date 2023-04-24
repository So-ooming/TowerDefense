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
            Debug.Log("이미 게임 매니저가 존재합니다.");
            Destroy(gameObject);
        }
        audio = transform.GetComponent<AudioSource>();
    }

    private void Start()
    {
        audio.Play();
    }
}
