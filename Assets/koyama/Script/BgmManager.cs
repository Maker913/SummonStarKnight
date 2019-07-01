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
            return;
        }
        audioSource.Stop();
        audioSource.clip = audioClips[number];
        audioSource.Play();
    }
    //BGM停止
    public void StopBGM()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }
}
