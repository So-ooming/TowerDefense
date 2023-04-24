using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip[] bgList;
    public static SoundManager Instance = null;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BgmPlay(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.loop = true;
        bgm.volume = 0.5f;
        bgm.Play();
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i = 0; i < bgList.Length; i++)
        {
            if(arg0.name == bgList[i].name)
            {
                BgmPlay(bgList[i]);
            }
        }
    }
}
