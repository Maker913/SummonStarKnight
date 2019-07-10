using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kariB : MonoBehaviour
{
    public void gamestart()
    {
        SceneControl.Instance.LoadScene(SceneControl.SceneName.Stage1, true);
    }

    public void TitleBack()
    {
        SceneControl.Instance.LoadScene(SceneControl.SceneName.Title
            , true);
    }

}
