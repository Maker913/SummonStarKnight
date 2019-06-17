using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    AudioManager audioManager;
    public static BgmManager Instance;
    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        audioManager = AudioManager.Instance;
    }

    private void Start()
    {
        
    }
    //BGM再生
    public void PlayBGM(int number)
    {
        if (0 > number || audioManager.BgmList.Length <= number)
        {
            return;
        }
        //同じBGMの時は何もしない
        if (audioSource.clip == audioManager.BgmList[number])
        {
            Debug.Log("何もしない");
            return;
        }
        audioSource.Stop();
        audioSource.clip = audioManager.BgmList[number];
        audioSource.Play();
        Debug.Log("鳴った");
    }
    //BGM停止
    public void StopBGM()
    {
        audioSource.Stop();
        audioSource.clip = null;
        Debug.Log("止めた");
    }
}
