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
   /// シーン遷移用のコルーチン処理
   /// </summary>
   /// <param name="sceneName">遷移先のシーン名</param>
   /// <param name="isFade">true=フェードを実行</param>
   /// <param name="interval">フェードにかかる時間</param>
   /// <param name="unityAction">フェード中に実行したい処理</param>
   /// <returns></returns>
    private IEnumerator SceneChange(string sceneName, bool isFade, float interval, UnityEngine.Events.UnityAction unityAction)
    {
        if(!isFade)
        {
            SceneManager.LoadScene(sceneName);
            yield break;
        }

        isFading = true;

        // 画面を暗くする
        float time = 0;
        while (time <= interval)
        {
            fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        // 画面暗転中に実行したい処理を実行する
        unityAction?.Invoke();
        // シーンを遷移させる
        SceneManager.LoadScene(sceneName);

        // 画面を明るくする
        time = 0;
        while (time <= interval)
        {
            fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        isFading = false;
    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="sceneName">遷移先のシーン名</param>
    /// <param name="isFade">フェードを実行する場合はtrue</param>
    /// <param name="interval">フェードにかかる時間(秒)</param>
    /// <param name="unityAction">フェード中に実行したい処理のメソッド名</param>
    public void LoadScene(string sceneName, bool isFade = false, float interval = 1.0f, UnityEngine.Events.UnityAction unityAction = null)
    {
        StartCoroutine(SceneChange(sceneName, isFade, interval, unityAction));
    }
}
