using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// SE再生。
    ///AudioManager.Instance.PlaySE("番号");
    ///BGM再生。
    ///AudioManager.Instance.PlayBGM("番号");
    /// </summary>
    public static AudioManager Instance;
    private AudioSource audioSource;
    //SEとBGMの区別
    [SerializeField]
    private AudioClip[] BGM;
    private AudioSource BGMsource; 
    [SerializeField]
    private AudioClip[] SE;
    private AudioSource SEsource;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = gameObject.GetComponent<AudioSource>();
        BGMsource.loop = true;
    }
    //SE
    public void PlaySE(int number)
    {
        if(0 > number || SE.Length <= number)
        {
            return;
        }
        foreach(AudioClip audio in SE)
        {
            if(audioSource.clip == SE[number])
            {
                audioSource.clip = audio;
                audioSource.Play();
                break;
            }
        }
    }
    //BGM
    public void PlayBGM(int number)
    {
        if(0 > number || BGM.Length <= number)
        {
            return;
        }
        if (BGMsource.clip == BGM[number])
        {
            return;
        }
        BGMsource.Stop();
        BGMsource.clip = BGM[number];
        BGMsource.Play();
    }
    public void StopBGM()
    {
        BGMsource.Stop();
        BGMsource.clip = null;
    }
}
