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
            if(blackTexture == null)
            {
                // フェード用の黒いテクスチャを作成
                blackTexture = new Texture2D(32, 32, TextureFormat.RGB24, false);
                blackTexture.ReadPixels(new Rect(0, 0, 32, 32), 0, 0, false);
                blackTexture.SetPixel(0, 0, Color.white);
                blackTexture.Apply();
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnGUI()
    {
        if (!isFading || blackTexture == null) return;

        // フェード用の黒いテクスチャを描画する
        GUI.color = new Color(0, 0, 0, fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture);
    }

   /// <summary>
   /// シーン遷移用のコルーチン処理
   /// </summary>
    private IEnumerator SceneChange(SceneName scene, bool isFade, float interval, UnityEngine.Events.UnityAction unityAction)
    {
        #region フェードしないを宣言、又はフェード時間が0秒以下の場合
        if (!isFade || interval <= 0)
        {
            SceneManager.LoadScene((int)scene);
            yield break;
        }
        #endregion

        #region フェードするを宣言、及びフェード時間が0秒より多い場合
        isFading = true;

        float halfInterval = interval / 2;

        // 画面を暗くする
        float time = 0;
        while (time <= halfInterval)
        {
            fadeAlpha = Mathf.Lerp(0f, 1f, time / halfInterval);
            time += Time.deltaTime;
            yield return 0;
        }

        // 画面暗転中に実行したい処理があれば実行する
        unityAction?.Invoke();

        // シーンを遷移させる
        SceneManager.LoadScene((int)scene);

        // 画面を明るくする
        time = 0;
        while (time <= halfInterval)
        {
            fadeAlpha = Mathf.Lerp(1f, 0f, time / halfInterval);
            time += Time.deltaTime;
            yield return 0;
        }

        isFading = false;
        #endregion
    }

    /// <summary>
    /// <para>シーン遷移</para>
    /// <para>フェードなし: LoadScene(SceneControl.SceneName.シーン名); </para>
    /// <para>1秒間のフェードあり: LoadScene(SceneControl.SceneName.シーン名, true); </para>
    /// <para>2秒間のフェードあり: LoadScene(SceneControl.SceneName.シーン名, true, 2.0f); </para>
    /// </summary>
    /// <param name="scene">遷移先のシーン</param>
    /// <param name="isFade">フェードを実行する場合はtrue</param>
    /// <param name="interval">フェード開始から終了までの時間(秒)<para>0秒以下ならフェードなしとして実行</para></param>
    /// <param name="unityAction">画面が完全に暗くなったタイミングで実行したい処理<para>フェードを実行した場合のみ有効</para></param>

    public void LoadScene(SceneName scene, bool isFade = false, float interval = 1.0f, UnityEngine.Events.UnityAction unityAction = null)
    {
        if (isFading) return;
        StartCoroutine(SceneChange(scene, isFade, interval, unityAction));
    }

    public enum SceneName
    {
        // Titleシーン
        Title = 0,

        // Stageシーン
        Stage1,

        // Rsultシーン
        Result

    }
}
