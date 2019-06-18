using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : SoundModel
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    //BGM再生
    public void PlayBGM(int number)
    {
        if (0 > number || audioClips.Count <= number)
        {
            return;
        }
        //同じBGMの時は何もしない
        if (audioSource.clip == audioClips[number])
        {
            Debug.Log("何もしない");
            return;
        }
        audioSource.Stop();
        audioSource.clip = audioClips[number];
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
