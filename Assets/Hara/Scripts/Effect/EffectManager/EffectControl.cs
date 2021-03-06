﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    public static EffectControl Instance;

    [SerializeField, Tooltip("ゲーム内で使用するエフェクト")]
    private List<ParticleSystem> effectObjects;
    private ParticleSystem[] particles;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            particles = new ParticleSystem[effectObjects.Count];
            for(int i = 0; i < particles.Length; i++)
            {
                particles[i] = Instantiate(effectObjects[i], gameObject.transform);
                particles[i].Stop();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum Effect
    {
        Attack = 0,              // 攻撃エフェクト
        Aura_Blue,               // オーラ（ブルー）
        Aura_DarkBlue,           // オーラ（ダークブルー）
        Aura_EmeraldGreen,       // オーラ（エメラルドグリーン）
        Aura_Green,              // オーラ（グリーン）
        Aura_LightBlue,          // オーラ（ライトブルー）
        Aura_Orange,             // オーラ（オレンジ）
        Aura_Pink,               // オーラ（ピンク）
        Aura_Purple,             // オーラ（パープル）
        Aura_Red,                // オーラ（レッド）
        Aura_White,              // オーラ（ホワイト）
        Aura_Yellow,             // オーラ（イエロー)
        Aura_YellowishGreen,     // オーラ（イエローグリーン）
        Fire,                    // 炎のエフェクト
        Poison,                  // 毒のエフェクト
        SwordSlash,              // 斬撃エフェクト
        Water,                   // 水のエフェクト
        WaterBall,               // 水の球のエフェクト
    }

    /// <summary>
    /// エフェクトを再生する
    /// <para>PlayEffect(EffectControl.Effect.再生するエフェクト, エフェクトを表示する座標, エフェクトの向き, 再生時間)</para>
    /// </summary>
    /// <param name="effectName">再生するエフェクト名</param>
    /// <param name="effectPos">エフェクトを再生する座標</param>
    /// <param name="effectRot">エフェクトを再生する向き</param>
    /// <param name="duration">エフェクトの再生時間</param>
    public void PlayEffect(Effect effectName, Vector3 effectPos, Vector3 effectRot, float duration = 0)
    {
        int effectNumber = (int)effectName;
        StartCoroutine(PlayEffectCotoutine(effectNumber, effectPos, effectRot, duration));
    }

    private IEnumerator PlayEffectCotoutine(int effectNumber, Vector3 effectPos, Vector3 effectRot, float duration)
    {
        if (particles[effectNumber].isPlaying)
        {
            particles[effectNumber].Stop();
        }
        particles[effectNumber].transform.position = effectPos;
        particles[effectNumber].transform.rotation = Quaternion.Euler(effectRot);
        particles[effectNumber].Play();

        // durationに0より多い値が宣言された場合、duration秒後にエフェクトの再生を止める
        if(duration > 0)
        {
            yield return new WaitForSeconds(duration);
            particles[effectNumber].Stop();
        }
    }

    /// <summary>
    /// エフェクトを停止する
    /// <para>StopEffect(EffectControl.Effect.停止するエフェクト)</para>
    /// </summary>
    /// <param name="effectName">停止するエフェクト名</param>
    public void StopEffect(Effect effectName)
    {
        if (particles[(int)effectName].isPlaying)
        {
            particles[(int)effectName].Stop();
        }
    }
}
