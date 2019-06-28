//using System;
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
    private BgmManager bgmManager;
    private SeManager seManager;

    readonly List<string> seNameList = new List<string>
    {
        "button",
        "enemy_Deathblow",
        "failed",
        "player_attack",
        "result",
        "Follow",
        "success",
        "gauge",
    };
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
        bgmManager = GetComponentInChildren<BgmManager>();
        seManager = GetComponentInChildren<SeManager>();
    }
    //BGM再生
    public void PlayBGM(int number)
    {
        bgmManager.PlayBGM(number);
    }
    //BGM停止
    public void StopBGM()
    {
        bgmManager.StopBGM();
    }
    //SE再生
    /*
    public void PlaySE(int number)
    {
        seManager.PlaySE(number);
    }
    */
    public void PlaySE(SEName name)
    {
        //var se = (SEName)Enum.Parse(typeof(SEName), " ");
        seManager.PlaySE(seNameList[(int)name]);
    }

    //SE停止
    public void StopSE()
    {
        seManager.StopSE();
    }
}
