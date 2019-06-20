using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultControl : MonoBehaviour
{
    [SerializeField]
    private Button titleButton = null;
    [SerializeField]
    private GameObject characterModel = null;
    [SerializeField, Tooltip("シーン遷移先")]
    private string sceneName = null;

    // Start is called before the first frame update
    void Start()
    {
        if (titleButton != null)
        {
            titleButton.onClick.AddListener(() => ButtonAction());
        }
        else
        {
            Debug.LogError("ボタンオブジェクトを割り当ててください");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 仮の処理
        characterModel.transform.Rotate(0, 1, 0);
    }

    /// <summary>
    /// ボタンで呼び出される処理
    /// </summary>
    private void ButtonAction()
    {
        // シーン遷移
        SceneControl.Instance.LoadScene(sceneName, true, 0.75f, () => SoundStop());
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
