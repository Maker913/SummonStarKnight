using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    // 画面遷移時のフェードテクスチャ
    private Texture2D blackTexture;
    private float fadeAlpha = 0;
    private bool isFading = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // 黒いテクスチャの作成
        StartCoroutine(MakeTexture());
    }

    /// <summary>
    /// フェード用の黒いテクスチャを作成
    /// </summary>
    /// <returns></returns>
    private IEnumerator MakeTexture()
    {
        this.blackTexture = new Texture2D(32, 32, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();
        this.blackTexture.ReadPixels(new Rect(0, 0, 32, 32), 0, 0, false);
        this.blackTexture.SetPixel(0, 0, Color.white);
        this.blackTexture.Apply();
    }

    private void OnGUI()
    {
        if (!this.isFading) return;

        // 黒いテクスチャの描画
        GUI.color = new Color(0, 0, 0, this.fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.blackTexture);
    }

    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name="scene">シーン名</param>
    /// <param name="interval">暗転にかかる時間(秒)</param>
    /// <returns></returns>
    private IEnumerator FadeScene(string scene, float interval)
    {
        this.isFading = true;

        // 暗くする
        float time = 0;
        while(time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        // シーン切り替え
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);

        // 明るくする
        time = 0;
        while(time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }

    /// <summary>
    /// 画面遷移
    /// </summary>
    /// <param name="scene">シーン名</param>
    /// <param name="interval">暗転にかかる時間(秒)</param>
    public void LoadScene(string scene, float interval)
    {
        if(!isFading) StartCoroutine(FadeScene(scene, interval));
    }
}
