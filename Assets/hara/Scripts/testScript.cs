using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public void ButtonAction()
    {
        SceneChanger.instance.LoadScene("test2");
    }
}
