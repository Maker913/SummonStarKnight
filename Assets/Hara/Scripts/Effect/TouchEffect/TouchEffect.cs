using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchEffect : MonoBehaviour
{
    public static TouchEffect Instance;

    [SerializeField, Tooltip("タッチしたときのParticle")]
    private ParticleSystem touchPartticleSystem;
    [SerializeField, Tooltip("なぞるときのParticle")]
    private ParticleSystem swipeParticleSystem;
    [SerializeField, Tooltip("パーティクルを描画するCanvas")]
    private Canvas particleCanvasPrefab;
    [SerializeField, Tooltip("パーティクル用のカメラ")]
    private Camera particleCameraPrefab;
    [SerializeField, Tooltip("パーティクル描画用のRawImage")]
    private RawImage rawImagePrefab;
    [SerializeField, Tooltip("タッチしたときのParticleを表示しないならfalse")]
    private bool isTouchEffect = true;
    public bool IsTouchEffect { set { isTouchEffect = value; } }
    [SerializeField, Tooltip("なぞったときのParticleを表示しないならfalse")]
    private bool isSwipeEffect = true;
    public bool IsSwipeEffect { set { isSwipeEffect = value; } }


    // エフェクトを生成するための変数
    private ParticleSystem touchParticle;
    private ParticleSystem swipeParticle;
    private Canvas particleCanvas;
    private RenderTexture renderTexture;
    private RawImage rawImage;

    // パーティクル用のカメラ
    private Camera particleCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if(renderTexture == null)
            {
                // Particle描画用のRenderTextureを画面サイズに合わせて作成
                renderTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
            }

            if(particleCanvas == null)
            {
                // Particleを描画するRawImageとRawImage用のCanvasの生成
                particleCanvas = Instantiate(particleCanvasPrefab);

                if(rawImage == null)
                {
                    // RawImageを画面サイズと同じサイズにして生成
                    rawImagePrefab.texture = renderTexture;
                    rawImagePrefab.SetNativeSize();
                    var rect = rawImagePrefab.GetComponent<RectTransform>();
                    rect.anchorMin = new Vector2(0, 0);
                    rect.anchorMax = new Vector2(1, 1);

                    rawImage = Instantiate(rawImagePrefab);
                    rawImage.transform.SetParent(particleCanvas.transform);
                    rawImage.transform.localPosition = new Vector3(0, 0, 0);
                }
                
                DontDestroyOnLoad(particleCanvas);
            }

            if(particleCamera == null)
            {
                // Particle用のカメラと描画するParticleの生成
                particleCamera = Instantiate(particleCameraPrefab);
                particleCamera.targetTexture = renderTexture;

                if (touchParticle == null)
                {
                    // タッチした時のParticleの生成
                    touchParticle = Instantiate(touchPartticleSystem);
                    touchParticle.transform.SetParent(particleCamera.transform, false);
                    touchParticle.Stop();
                }

                if (swipeParticle == null)
                {
                    // なぞった時のParticleの生成
                    swipeParticle = Instantiate(swipeParticleSystem);
                    swipeParticle.transform.SetParent(particleCamera.transform, false);
                    touchParticle.Stop();
                }

                DontDestroyOnLoad(particleCamera);
            }
        }
        else
        {
            Destroy(gameObject);
        }
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
        if(!isTouchEffect && !isSwipeEffect)
        {
            return;
        }

        if (!isTouchEffect)
        {
            if (touchParticle.isPlaying)
            {
                touchParticle.Stop();
            }
        }

        if (!isSwipeEffect)
        {
            if (swipeParticle.isPlaying)
            {
                swipeParticle.Stop();
            }
        }

        // なぞっている位置を検知してParticleオブジェクトを移動
        var pos = particleCamera.ScreenToWorldPoint(Input.mousePosition + particleCamera.transform.forward * 10);
        swipeParticle.transform.position = pos;

        // タッチの検知
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                // タッチした位置を検知してParticleオブジェクトを移動してParticleを再生する
                if (touchParticle.isPlaying)
                {
                    touchParticle.Stop();
                }

                touchParticle.transform.position = pos;

                if (isTouchEffect)
                {
                    touchParticle.Play();
                }

                if (isSwipeEffect)
                {
                    swipeParticle.Play();
                }
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                // タッチの終了を検知してParticleの再生を止める
                if (touchParticle.isPlaying)
                {
                    touchParticle.Stop();
                }

                if (swipeParticle.isPlaying)
                {
                    swipeParticle.Stop();
                }
            }
        }
    }
}
