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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ButtonAction()
    {
        SceneChanger.instance.LoadScene(sceneName, 1.0f);
    }
}
