using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleControl : MonoBehaviour
{
    [SerializeField,Tooltip("遷移先のシーン名")]
    private SceneControl.SceneName scene;
    [SerializeField]
    private Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        if(startButton != null)
        {
            startButton.onClick.AddListener(() => ButtonAction());
        }
        else
        {
            Debug.LogError("ボタンオブジェクトを割り当ててください");
        }
        //AudioManager.Instance.PlayBGM(0);
    }

    /// <summary>
    /// スタートボタンで呼び出される処理
    /// </summary>
    private void ButtonAction()
    {
        // シーン遷移
        SceneControl.Instance.LoadScene(scene, true, unityAction: () => SoundStop());
    }

    /// <summary>
    /// シーン内で鳴っているBGMとSEを停止
    /// </summary>
    private void SoundStop()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.StopSE();
    }
}
