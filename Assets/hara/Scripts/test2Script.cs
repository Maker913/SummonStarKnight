using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2Script : MonoBehaviour
{
    public void ButtonAction()
    {
        SceneControl.Instance.LoadScene("test1", 1.0f);
    }
}
