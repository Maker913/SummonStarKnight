using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kariB : MonoBehaviour
{
    public void gamestart()
    {
        SceneChanger.instance.LoadScene("Newmain", 1);
    }

    public void TitleBack()
    {
        SceneChanger.instance.LoadScene("Titlekari", 1);
    }

}
