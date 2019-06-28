using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    //シナリオの元データ
    [SerializeField]
    private GameObject tData;
    [SerializeField]
    private Text uiText;
    [SerializeField]
    private Text autoText;
    
    //時間
    [SerializeField]
    private float time;
    //保存時間
    [SerializeField]
    private float tmpTime;

    [SerializeField]
    private float autoOnlyTime;
    //文字の表示される速度
    [SerializeField][Range(0.001f, 0.3f)]
    private float DisplayTextIntarval = 0.05f;
    
    //テキストの格納場所
    [SerializeField]
    private List<string> oringtext = new List<string>();

    [SerializeField]
    private string[] texts;
    //現在の行数
    private int tIndex = 0;
    //最後の行数
    private int tDataIndex = 0;
    //現在のページ
    [SerializeField]
    private int page = 0;
    private List<int> lineCount = new List<int>();
    private int charCount = 0;
    private bool auto = false;

    private enum NextText
    {
        next,
        end,
        standby,
        print
    }

    [SerializeField]
    private NextText nextText = 0;


    void Start()
    {
        
    }
    
    void Update()
    {
        ScenarioStore();
        time += Time.deltaTime;

        if(nextText == NextText.end) { return; }
        PrintText();
        //Debug.Log(nextText);
    }
    
    private void PrintText()
    {
        AutoTextPrint();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (auto == false)
            {
                TextSkip();
            }
        }

        if (tIndex < tDataIndex)
        {
            StoreText();

            DisplayText();
        }
        Debug.Log("page:" + page);
    }

    //テキストデータのロードが終わり次第テキストデータの格納
    private void ScenarioStore()
    {
        if (tData.GetComponent<TextData>().loadFinish)
        {
            tDataIndex = tData.GetComponent<TextData>().textData.Count;

            for (int i = 0; i < tDataIndex; i++)
            {
                oringtext.Add(tData.GetComponent<TextData>().textData[i]);
            }

            for (int i = 0; i < tDataIndex; i++)
            {
                if (oringtext[i].Substring(0, 1) == "{")
                {
                    if (oringtext[i].Substring(oringtext[i].IndexOf('{') + 1, oringtext[i].IndexOf('}') - 1) == "next")
                    {
                        lineCount.Add(i);
                    }

                }
            }
            tData.GetComponent<TextData>().loadFinish = false;
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
                        if (auto) { autoOnlyTime = time; }
                        break;
                    case "end":
                        nextText = NextText.end;
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
            if (charCount == 0)
            {
                //表示スペースの調整
                uiText.text += " ";
            }

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

    private void TextSkip()
    {
        if (nextText == NextText.end) { return; }
        if (nextText == NextText.standby)
        {
            uiText.text = "";
            tIndex++;
            nextText = NextText.next;
            page++;
        }
        else
        {
            PageSet();
        }
    }

    private void PageSet()
    {
        charCount = 0;
        nextText = NextText.standby;

        //最後のページかどうか
        if (page > lineCount.Count - 1)
        {
            tIndex = tDataIndex - 1;
            nextText = NextText.end;
        }
        else
        {
            tIndex = lineCount[page];
        }

        if (oringtext.Count - 1 >= 0)
        {
            //テキストの削除
            uiText.text = "";

            if (page != 0)
            {
                for (int i = lineCount[page - 1] + 1; i < tIndex; i++)
                {
                    uiText.text += " " + oringtext[i] + "\n";
                }
            }
            else
            {
                for (int i = 0; i < tIndex; i++)
                {
                    uiText.text += " " + oringtext[i] + "\n";
                }
            }
        }
    }

    private void AutoTextPrint()
    {
        if (auto)
        {
            if (nextText == NextText.standby && time >= autoOnlyTime + 2)
            {
                uiText.text = "";
                tIndex++;
                nextText = NextText.next;
                page++;
            }
        }
    }

    public void AutoSwitch()
    {
        if (auto == false)
        {
            auto = true;
            Animator animator = autoText.GetComponent<Animator>();
            animator.SetBool("On", true);

        }
        else if (auto == true)
        {
            auto = false;
            Animator animator = autoText.GetComponent<Animator>();
            animator.SetBool("On", false);
        }
    }
}
