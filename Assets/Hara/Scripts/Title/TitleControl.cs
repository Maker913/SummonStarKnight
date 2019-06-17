using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleControl : MonoBehaviour
{
    [SerializeField,Tooltip("遷移先のシーン名")]
    private string sceneName;
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
            Debug.LogError("スタートボタンオブジェクトを割り当ててください");
        }
        //AudioManager.Instance.PlayBGM(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ButtonAction()
    {
        //AudioManager.Instance.StopBGM();
        SceneControl.Instance.LoadScene(sceneName, true);
    }
}
