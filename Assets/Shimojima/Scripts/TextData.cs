using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextData : MonoBehaviour
{
    public List<string> textData;
    public bool loadFinish = false;
    private string scenario;

    void Start()
    {

        TextAsset ta=new TextAsset() ;
        if(StageCobtroller.Shooting ==false )
        {
            ta = Resources.Load<TextAsset>("Scenario/MainStage/Scenario" + StageCobtroller.stageNum.ToString());
        }
        else
        {
            ta = Resources.Load<TextAsset>("Scenario/Shooting/Shooting" + StageCobtroller.stageNum.ToString());
        }
        
        
        

        string s = ta.text;
        string[] ss = s.Split('\n');
        int i = 0;
        while (i < ss.Length)
        {
            textData.Add(ss[i]);
            i++;
        }
        i = 0;
        loadFinish = true;
    }
}
