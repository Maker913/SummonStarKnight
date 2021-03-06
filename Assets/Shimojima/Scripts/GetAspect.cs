﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAspect : MonoBehaviour
{
    [SerializeField]
    private Text[] texts;
    private float w,h = 0;

    void Start()
    {
        FontSizeAdjust();
    }
    
    /// <summary>
    /// フォントサイズの調整
    /// </summary>
    public void FontSizeAdjust()
    {
        w = Screen.width;
        h = Screen.height;
        float gcd = Euclidean(w,h);
        string aspect = (w / gcd + ":" + h / gcd).ToString();

        //Debug.Log(aspect);
        switch (aspect)
        {
            case "5:8":
                texts[0].fontSize = 30;
                texts[1].fontSize = 48;
                texts[2].fontSize = 30;
                break;
            case "9:16":
                texts[0].fontSize = 30;
                texts[1].fontSize = 48;
                texts[2].fontSize = 30;
                break;
        }

    }

    /// <summary>
    /// 最大公約数を返す
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private float Euclidean(float a, float b)
    {
        if (a < b)
            return Euclidean(b,a);
        while (b != 0)
        {
            float c= a % b;
            a = b;
            b = c;
        }

        return a;
    }
}
