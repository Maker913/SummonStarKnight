using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private BgmManager bgmManager;
    private SeManager seManager;

    public enum BgmName
    {
        TitleBGM = 0,       //タイトル
        ThemeBGM = 1,       //メインテーマ
    }
    public enum SeName
    {
        button = 0,         //ボタン
        enemy_Deathblow,    //敵の必殺技
        failed,             //失敗
        player_attack,      //プレイヤー攻撃
        result,             //リザルト
        Follow,             //なぞる
        success,            //成功
        gauge,              //ゲージ
        //敵攻撃音
        Reflection,         //跳ね返す時
        //反撃ヒット音
    }

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
    /// <summary>
    /// SE再生
    /// <para>AudioManager.Instance.PlayBGM("AudioManager.BgmName.名前");</para>
    /// </summary>
    /// <param name="name"></param>
    //BGM再生
    public void PlayBGM(BgmName name)
    {
        bgmManager.PlayBGM((int)name);
    }
    //BGM停止
    public void StopBGM()
    {
        bgmManager.StopBGM();
    }
    /// <summary>
    /// SE再生
    /// <para>AudioManager.Instance.PlaySE("AudioManager.SeName.名前");</para>
    /// </summary>
    /// <param name="name"></param>
    public void PlaySE(SeName name)
    {
        seManager.PlaySE((int)name);
    }

    //SE停止
    public void StopSE()
    {
        seManager.StopSE();
    }
}
