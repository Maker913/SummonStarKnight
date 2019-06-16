using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// SE再生。
    ///AudioManager.Instance.PlaySE("番号");
    ///BGM再生。
    ///AudioManager.Instance.PlayBGM("番号");
    /// </summary>
    public static AudioManager Instance;
    //SEとBGMの区別
    [SerializeField]
    private AudioClip[] BGM;
    private AudioSource BGMsource; 
    [SerializeField]
    private AudioClip[] SE;
    private AudioSource SEsources;
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
        SEsources = gameObject.GetComponent<AudioSource>();
        BGMsource = gameObject.AddComponent<AudioSource>();
        //BGMループ
        BGMsource.loop = true;
    }
    //BGM再生
    public void PlayBGM(int number)
    {
        if(0 > number || BGM.Length <= number)
        {
            return;
        }
        //同じBGMの時は何もしない
        if (BGMsource.clip == BGM[number])
        {
            Debug.Log("何もしない");
            return;
        }
        BGMsource.Stop();
        BGMsource.clip = BGM[number];
        BGMsource.Play();
        Debug.Log("鳴った");
    }
    //BGM停止
    public void StopBGM()
    {
        BGMsource.Stop();
        BGMsource.clip = null;
        Debug.Log("止めた");
    }
    //SE再生
    public void PlaySE(int number)
    {
        if (0 > number || SE.Length <= number)
        {
            return;
        }
        foreach (AudioClip source in SE)
        {
            SEsources.clip = SE[number];
            SEsources.Play();
            Debug.Log("鳴った");
            break;
        }
    }
    //SE停止
    public void StopSE()
    {
        //すべてのSEの停止
        foreach (AudioClip source in SE)
        {
            SEsources.Stop();
            SEsources.clip = null;
            Debug.Log("止めた");
        }
    }
}
