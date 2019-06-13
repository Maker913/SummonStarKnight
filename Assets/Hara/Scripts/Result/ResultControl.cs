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

    private void Awake()
    {
        titleButton.onClick.AddListener(() => ButtonAction());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 仮の処理
        characterModel.transform.Rotate(0, 1, 0);
    }

    private void ButtonAction()
    {
        SceneControl.Instance.LoadScene(sceneName, 1.0f);
    }
}
