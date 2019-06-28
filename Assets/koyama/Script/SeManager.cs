//using System;
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
    public static string Name(SEName seName)
    {
        string[] names = { };
        return names[(int)seName];
    }
    //SE再生
    /*
    public void PlaySE(int number)
    {
        if (0 > number || audioClips.Count <= number)
        {
            return;
        }
        audioSource.PlayOneShot(audioClips[number]);
        Debug.Log("aaaaa");
    }
    /
    public void PlaySE(SEName name)
    {
        var se = (SEName)Enum.Parse(typeof(SEName), " ");
        audioSource.PlayOneShot(audioClips[(int)se]);
        Debug.Log("seなった");
    }*/
    public void PlaySE(string name)
    {
        //audioSource.PlayOneShot(audioClips[names]);
    }
    public void StopSE()
    {
        audioSource.Stop();
        Debug.Log("se止めた");
    }
}
