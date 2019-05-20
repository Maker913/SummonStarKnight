using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField]
    private GameObject tData;
    [SerializeField]
    private Text uiText;

    private float time;
    private float tmpTime;
    [SerializeField][Range(0.001f, 0.3f)]
    private float DisplayTextIntarval = 0.05f;
    
    private List<string> oringtext = new List<string>();
    [SerializeField]
    private string[] texts;
    private int tIndex = 0;
    private int tDataIndex = 0;
    private int charCount = 0;
    private bool nextText = true;


    void Start()
    {
        tDataIndex = tData.GetComponent<TextData>().textData.Length;
        for (int i = 0; i < tDataIndex; i++)
        {
            oringtext.Add(tData.GetComponent<TextData>().textData[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        PrintText();
    }
    
    private void PrintText()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AllSetText();
        }

        if (tIndex != tDataIndex)
        {
            StoreText();

            DisplayText();
        }
    }

    //textsに文章を一文字ずつに分けて格納
    private void StoreText()
    {
        if (nextText)
        {
            texts = new string[oringtext[tIndex].Length];
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = oringtext[tIndex].Substring(i, 1);
            }
            nextText = false;
            tmpTime = time + DisplayTextIntarval;
        }
    }

    //textを表示する処理
    private void DisplayText()
    {
        if (nextText == false && time >= tmpTime && charCount != oringtext[tIndex].Length)
        {
            uiText.text += texts[charCount];
            charCount++;
            tmpTime = time + DisplayTextIntarval;
        }
        else if (uiText.text.Length == oringtext[tIndex].Length)
        {
            uiText.text += "\n";
            tIndex++;
            charCount = 0;
            nextText = true;
        }
    }

    private void AllSetText()
    {
        tIndex = tDataIndex;
        uiText.text = oringtext[0] + "\n";
        if (oringtext.Count - 1 >= 0)
        {
            for (int i = 1; i < oringtext.Count; i++)
            {
                uiText.text += oringtext[i] + "\n";
            }
        }
    }



}
