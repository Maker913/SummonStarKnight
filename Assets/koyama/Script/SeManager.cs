using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeManager : SoundModel
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    //SE再生
    public void PlaySE(int number)
    {
        if (0 > number || audioClips.Count <= number)
        {
            return;
        }
        audioSource.PlayOneShot(audioClips[number]);
        Debug.Log("aaaaa");
    }
    public void StopSE()
    {
        audioSource.Stop();
        Debug.Log("se止めた");
    }
}
