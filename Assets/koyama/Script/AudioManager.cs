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
    private AudioClip[] bgmList;
    public AudioClip[] BgmList { set { bgmList = value; } get { return bgmList; } }
    
    [SerializeField]
    private AudioClip[] seList;
    public AudioClip[] SeList { set { seList = value; } get { return seList; } }
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
    }

    //BGM再生
    public void PlayBGM(int number)
    {
        BgmManager.Instance.PlayBGM(number);
    }
    //BGM停止
    public void StopBGM()
    {
        BgmManager.Instance.StopBGM();
    }
    //SE再生
    public void PlaySE(int number)
    {
        SeManager.Instance.PlaySE(number);
    }
}
