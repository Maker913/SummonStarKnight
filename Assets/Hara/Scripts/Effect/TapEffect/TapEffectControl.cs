using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffectControl : MonoBehaviour
{
    public static TapEffectControl Instance;

    [SerializeField, Tooltip("タップした時のエフェクト")]
    private GameObject tapEffectObject;
    [SerializeField, Tooltip("なぞった時のエフェクト")]
    private GameObject swipeEffectObject;
    [SerializeField, Tooltip("エフェクトを使用したくない時はfalse")]
    private bool isEffect = true;
    public bool IsEffect { set { isEffect = value; } }

    // エフェクトを生成するための変数
    private ParticleSystem tapParticle;
    private ParticleSystem swipeParticle;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if(tapParticle == null)
            {
                // タップした時のエフェクトが生成されていないなら生成する
                tapParticle = Instantiate(tapEffectObject).GetComponent<ParticleSystem>();
                DontDestroyOnLoad(tapParticle.gameObject);
                tapParticle.Stop();
            }
            if(swipeParticle == null)
            {
                // スワイプした時のエフェクトが生成されていないなら生成する
                swipeParticle = Instantiate(swipeEffectObject).GetComponent<ParticleSystem>();
                DontDestroyOnLoad(swipeParticle.gameObject);
                swipeParticle.Stop();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isEffect) TapEffect();
    }

    /// <summary>
    /// タッチした位置を検知して、エフェクトをその位置で再生する
    /// </summary>
    private void TapEffect()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Camera.main.transform.forward * 10);
        swipeParticle.transform.position = pos;

        // タッチの検知
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                tapParticle.Stop();
                tapParticle.transform.position = pos;
                tapParticle.Play();
                swipeParticle.Play();
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                tapParticle.Stop();
                swipeParticle.Stop();
            }
        }
    }
}
