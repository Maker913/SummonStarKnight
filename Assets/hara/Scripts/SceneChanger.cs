using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;
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
