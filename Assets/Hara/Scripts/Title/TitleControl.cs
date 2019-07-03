using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleControl : MonoBehaviour
{
    [SerializeField,Tooltip("遷移先のシーン")]
    private SceneControl.SceneName scene;
    
    [SerializeField, Tooltip("ステージモデルの座標")]
    private List<GameObject> stageModels;
    [SerializeField, Tooltip("タイトルのMainCamera")]
    private TitleCamera mainCamera;

    private bool cameraStartFlag = false;
    private int stageModelNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        cameraStartFlag = true;
        foreach(GameObject obj in stageModels)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        if (cameraStartFlag)
        {
            // カメラを回転させる
            cameraStartFlag = false;
            stageModels[stageModelNumber].SetActive(true);
            mainCamera.transform.position = new Vector3(stageModels[stageModelNumber].transform.position.x, mainCamera.transform.position.y, stageModels[stageModelNumber].transform.position.z);
            mainCamera.CameraRotateStop = false;
        }

        if (mainCamera.CameraRotateStop)
        {
            // カメラが一回転したら次のカメラを回転させる
            cameraStartFlag = true;
            stageModels[stageModelNumber].SetActive(false);
            stageModelNumber++;
            if(stageModelNumber >= stageModels.Count)
            {
                stageModelNumber = 0;
            }
        }
    }

    /// <summary>
    /// スタートボタンで呼び出される処理
    /// </summary>
    public void ButtonAction()
    {
        AudioManager.Instance.PlaySE(AudioManager.SeName.button);
        // シーン遷移
        SceneControl.Instance.LoadScene(scene, true);
    }
}
