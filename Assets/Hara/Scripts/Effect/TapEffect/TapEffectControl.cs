using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffectControl : MonoBehaviour
{
    public static TapEffectControl Instance;

    [SerializeField, Tooltip("インスタンス元のオブジェクト")]
    private GameObject touchEffectPrefab;
    private RectTransform touchEffect;
    [SerializeField, Tooltip("タッチしたときのParticle")]
    private ParticleSystem touchPartticleSystem;
    [SerializeField, Tooltip("なぞるときのParticle")]
    private ParticleSystem swipeParticleSystem;
    [SerializeField, Tooltip("エフェクトを使用したくない時はfalse")]
    private bool isEffect = true;
    public bool IsEffect { set { isEffect = value; } }

    // エフェクトを生成するための変数
    
    private ParticleSystem touchParticle;
    private ParticleSystem swipeParticle;
    [SerializeField]
    private RectTransform canvasRct;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if(touchParticle == null)
            {
                touchParticle = Instantiate(touchPartticleSystem);
                touchParticle.transform.SetParent(gameObject.transform);
                touchParticle.Stop();
            }
            if (swipeParticle == null)
            {
                swipeParticle = Instantiate(swipeParticleSystem);
                swipeParticle.transform.SetParent(gameObject.transform);
                touchParticle.Stop();
            }
        }
        else
        {
            Destroy(gameObject);
        }
        //UnityEngine.SceneManagement.SceneManager.sceneLoaded += FindCanvas;
    }

    private void Update()
    {
        TapEffect();
    }

    /// <summary>
    /// タッチした位置を検知して、エフェクトをその位置で再生する
    /// </summary>
    private void TapEffect()
    {
        if (!isEffect)
        {
            if (touchParticle.isPlaying)
            {
                touchParticle.Stop();
            }
            if (swipeParticle.isPlaying)
            {
                swipeParticle.Stop();
            }
            return;
        }

        // なぞっている位置を検知してエフェクトオブジェクトを移動
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Camera.main.transform.forward * 10);
        swipeParticle.transform.position = pos;
        /*
        var mousepos = Input.mousePosition;
        mousepos.z = 0f;
        var scrPos = Camera.main.ScreenToViewportPoint(mousepos);
        var objPos = new Vector2((scrPos.x * canvasRct.sizeDelta.x) - (canvasRct.sizeDelta.x * 0.5f), (scrPos.y * canvasRct.sizeDelta.y) - (canvasRct.sizeDelta.y * 0.5f));
        swipeParticle.transform.localPosition = objPos;
        */

        if (Input.GetMouseButtonDown(0))
        {
            if (touchParticle.isPlaying)
            {
                touchParticle.Stop();
            }
            //tapParticle.transform.localPosition = objPos;
            touchParticle.transform.position = pos;
            touchParticle.Play();
            swipeParticle.Play();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touchParticle.Stop();
            swipeParticle.Stop();
        }
        /*
        // タッチの検知
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                // タッチした位置を検知してエフェクトオブジェクトを移動し、エフェクトを再生
                if (tapParticle.isPlaying)
                {
                    tapParticle.Stop();
                }
                tapParticle.transform.localPosition = objPos;
                tapParticle.Play();
                swipeParticle.Play();
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                tapParticle.Stop();
                swipeParticle.Stop();
            }
        }
        */
    }

    /// <summary>
    /// シーンが切り替わったらCanvasがあるかを確認する
    /// </summary>
    /// <param name="nextScene"></param>
    /// <param name="mode"></param>
    private void FindCanvas(UnityEngine.SceneManagement.Scene nextScene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        /*
        if(FindObjectOfType<Canvas>() != null)
        {
            canvasRct = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
            if(touchEffect == null)
            {
                touchEffect = Instantiate(touchEffectPrefab).GetComponent<RectTransform>();
                if (touchEffect == null)
                {
                    // タップした時のエフェクトが生成されていないなら生成する
                    touchEffect = touchEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
                    touchEffect.Stop();
                }
                if (swipeParticle == null)
                {
                    // スワイプした時のエフェクトが生成されていないなら生成する
                    swipeParticle = touchEffect.transform.GetChild(1).GetComponent<ParticleSystem>();
                    swipeParticle.Stop();
                }
            }
            touchEffect.transform.SetParent(canvasRct.transform);
            touchEffect.transform.localPosition = new Vector3(0, 0, 0);
            touchEffect.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            touchEffect.sizeDelta = new Vector2(Screen.width, Screen.height);
        }
        else
        {
            Debug.LogError("タッチエフェクトを表示する為に、Canvasを生成してください!!!");
        }
        */
    }
}
