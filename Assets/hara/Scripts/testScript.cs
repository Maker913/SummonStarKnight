using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public void ButtonAction()
    {
        SceneControl.Instance.LoadScene("test2",1.0f);
    }
}
