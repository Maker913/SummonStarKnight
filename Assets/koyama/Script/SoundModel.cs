using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SEName
{
    button,         //ボタン
    enemy_Deathblow,//敵の必殺技
    failed,         //失敗
    player_attack,  //プレイヤー攻撃
    result,         //リザルト
    Follow,         //なぞる
    success,        //成功
    gauge,          //ゲージ
}
public class SoundModel : MonoBehaviour
{
    [SerializeField,Tooltip("音源のリスト")]
    protected List<AudioClip> audioClips;
}
