using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

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

        /*
        blackTexture = new Texture2D(32, 32, TextureFormat.RGB24, false);
        blackTexture.ReadPixels(new Rect(0, 0, 32, 32), 0, 0, false);
        blackTexture.SetPixel(0, 0, Color.white);
        blackTexture.Apply();
        */
    }

    public void OnGUI()
    {
        if (!isFading)
            return;

        GUI.color = new Color(0, 0, 0, fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture);
    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="str">遷移先のシーン名</param>
    public void LoadScene(string str)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(str);
    }
}
