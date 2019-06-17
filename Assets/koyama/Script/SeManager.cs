using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeManager : MonoBehaviour
{
    public static SeManager Instance;
    private AudioManager audioManager;
    private AudioSource[] seSource;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        audioManager = AudioManager.Instance;
        seSource = new AudioSource[audioManager.SeList.Length];
        seSource = GetComponents<AudioSource>();
        for(int i = 0; i < audioManager.SeList.Length; i++)
        {
            seSource[i].clip = audioManager.SeList[i];
        }
    }
    //SE再生
    public void PlaySE(int number)
    {
        if (0 > number || audioManager.SeList.Length <= number)
        {
            return;
        }
        seSource[number].PlayOneShot(seSource[number].clip);
        Debug.Log("aaaaa");
    }
}
