using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    public static SceneControl Instance;

    // 画面遷移時のフェードテクスチャ
    private Texture2D blackTexture;
    private float fadeAlpha = 0;
    private bool isFading = false;

    // 遷移先のシーン情報
    private string sceneName;
    private int sceneNumber;

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

        // 黒いテクスチャの作成
        StartCoroutine(MakeTexture());
    }

    /// <summary>
    /// フェード用の黒いテクスチャを作成
    /// </summary>
    /// <returns></returns>
    private IEnumerator MakeTexture()
    {
        blackTexture = new Texture2D(32, 32, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();
        blackTexture.ReadPixels(new Rect(0, 0, 32, 32), 0, 0, false);
        blackTexture.SetPixel(0, 0, Color.white);
        blackTexture.Apply();
    }

    private void OnGUI()
    {
        if (!isFading) return;

        // 黒いテクスチャの描画
        GUI.color = new Color(0, 0, 0, fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture);
    }

    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name="scene">シーン名</param>
    /// <param name="interval">暗転にかかる時間(秒)</param>
    /// <returns></returns>
    private IEnumerator FadeScene(bool sceneChangeMode, float interval)
    {
        isFading = true;
        Debug.Log("画面遷移を開始しました");

        // 暗くする
        float time = 0;
        while(time <= interval)
        {
            fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        // シーン切り替え
        if (sceneChangeMode)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNumber);
        }

        // 明るくする
        time = 0;
        while(time <= interval)
        {
            fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        isFading = false;
        Debug.Log("画面遷移を終了しました");
    }

    /// <summary>
    /// 画面遷移（シーン名参照）
    /// </summary>
    /// <param name="scene">シーン名</param>
    /// <param name="interval">暗転にかかる時間(秒)</param>
    public void LoadScene(string scene, float interval)
    {
        if (isFading) return;
        sceneName = scene;
        StartCoroutine(FadeScene(true, interval));
    }

    /// <summary>
    /// 画面遷移（シーン番号参照）
    /// </summary>
    /// <param name="sceneNum">シーン番号</param>
    /// <param name="interval">暗転にかかる時間(秒)</param>
    public void LoadScene(int sceneNum, float interval)
    {
        if (isFading) return;
        sceneNumber = sceneNum;
        StartCoroutine(FadeScene(false, interval));
    }
}
