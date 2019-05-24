using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleControl : MonoBehaviour
{
    [SerializeField]
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
        SceneControl.Instance.LoadScene(1, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ButtonAction()
    {
        SceneControl.Instance.LoadScene(sceneName, 0.5f);
    }
}
