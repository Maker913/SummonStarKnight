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

    [SerializeField]
    private float time;
    [SerializeField]
    private float tmpTime;
    [SerializeField][Range(0.001f, 0.3f)]
    private float DisplayTextIntarval = 0.05f;
    
    private List<string> oringtext = new List<string>();
    [SerializeField]
    private string[] texts;
    private int tIndex = 0;
    private int tDataIndex = 0;
    private int charCount = 0;

    private enum NextText
    {
        next,
        end,
        standby,
        print
    }

    private NextText nextText = 0;


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
        Debug.Log(nextText);
    }
    
    private void PrintText()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AllSetText();
        }

        if (tIndex < tDataIndex)
        {
            StoreText();

            DisplayText();
        }
    }

    //textsに文章を一文字ずつに分けて格納
    private void StoreText()
    {
        if (nextText == NextText.next)
        {
            texts = new string[oringtext[tIndex].Length];
            if (oringtext[tIndex].Substring(0, 1) != "{")
            {
                for (int i = 0; i < texts.Length; i++)
                {
                    texts[i] = oringtext[tIndex].Substring(i, 1);
                }
                nextText = NextText.print;
                tmpTime = time + DisplayTextIntarval;
            }
            else
            {
                string command = oringtext[tIndex].Substring(oringtext[tIndex].IndexOf('{') + 1, oringtext[tIndex].IndexOf('}') - 1);
                switch (command)
                {
                    case "next":
                        nextText = NextText.standby;
                        break;
                    case "end":
                        break;
                }

            }
        }
    }

    //textを表示する処理
    private void DisplayText()
    {
        if (nextText == NextText.print && time >= tmpTime && charCount != oringtext[tIndex].Length)
        {
            uiText.text += texts[charCount];
            charCount++;
            tmpTime = time + DisplayTextIntarval;
        }
        else if (charCount == oringtext[tIndex].Length)
        {
            uiText.text += "\n";
            tIndex++;
            charCount = 0;
            nextText = NextText.next;
        }
    }

    private void AllSetText()
    {
        if (nextText == NextText.standby)
        {
            uiText.text = "";
            tIndex++;
            nextText = NextText.next;
        }
        else
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



}
