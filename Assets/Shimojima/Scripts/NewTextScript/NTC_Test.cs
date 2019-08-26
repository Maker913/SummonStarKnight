using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NTC_Test : MonoBehaviour
{
    [SerializeField]
    private NewTextData ntd;

    [SerializeField]
    private Text text;

    [SerializeField]
    private string a;

    [SerializeField]
    private string b;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            text.text = "";
            ntd.TextDataRead(a + b);
        }
    }
}
