using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public static SceneControl Instance;

    // 画面遷移時のフェードテクスチャ
    private Texture2D blackTexture;
    private float fadeAlpha = 0;
    private bool isFading = false;

    // 遷移先のシーン情報
    private string sceneNameData;
    private int sceneNumberData;

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

        // 暗くする
        float time = 0;
        while(time <= interval)
        {
            fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        // シーン切り替え
        LoadScene(sceneChangeMode);

        // 明るくする
        time = 0;
        while(time <= interval)
        {
            fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        isFading = false;
    }

    /// <summary>
    /// シーン切り替え
    /// </summary>
    /// <param name="sceneChangeMode">true=シーン名参照、false=シーン番号参照</param>
    private void LoadScene(bool sceneChangeMode)
    {
        if (sceneChangeMode)
        {
            SceneManager.LoadScene(sceneNameData);
        }
        else
        {
            SceneManager.LoadScene(sceneNumberData);
        }
    }

    /// <summary>
    /// シーン遷移（シーン名参照）
    /// </summary>
    /// <param name="scene">シーン名</param>
    /// <param name="interval">暗転にかかる時間(秒)</param>
    public void LoadScene(string sceneName, float interval)
    {
        if (isFading) return;
        sceneNameData = sceneName;
        StartCoroutine(FadeScene(true, interval));
    }

    /// <summary>
    /// シーン遷移（シーン番号参照）
    /// </summary>
    /// <param name="sceneNum">シーン番号</param>
    /// <param name="interval">暗転にかかる時間(秒)</param>
    public void LoadScene(int sceneNum, float interval)
    {
        if (isFading) return;
        sceneNumberData = sceneNum;
        StartCoroutine(FadeScene(false, interval));
    }

    /// <summary>
    /// シーン遷移（シーン名参照、フェードなし）
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    public void LoadScene(string sceneName)
    {
        sceneNameData = sceneName;
        LoadScene(true);
    }

    /// <summary>
    /// シーン遷移（シーン番号参照、フェードなし）
    /// </summary>
    /// <param name="sceneName">シーン番号</param>
    public void LoadScene(int sceneNum)
    {
        sceneNumberData = sceneNum;
        LoadScene(false);
    }
}
