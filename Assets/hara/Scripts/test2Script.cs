using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2Script : MonoBehaviour
{
    public void ButtonAction()
    {
        SceneChanger.instance.LoadScene("test1");
    }
}
