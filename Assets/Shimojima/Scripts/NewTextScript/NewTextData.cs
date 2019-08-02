using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTextData : MonoBehaviour
{
    //シナリオファイルの名前
    public static string scenarioName = "";

    //デバッグ用です
    [SerializeField, Tooltip("デバッグ用 シナリオファイルの名前")]
    public string sName = "";
    [SerializeField, Tooltip("デバッグ用 ゲームモード名")]
    public string gName = "";


    //ゲームモード名と" / "を入れてください
    // 例) MainStage/    Shooting/
    //ゲームモード名がない場合は空のままにしてください
    public static string gameModeName = "";

    public List<string> textData;
    public bool loadFinish = false;
    private string scenario;


    /// <summary>
    /// テキストデータの読込
    /// </summary>
    public void TextDataRead()
    {
        gameObject.GetComponent<NewTextController>().ResetText();
        textData = new List<string>();
        TextAsset ta = new TextAsset();

        //↓本番時または実際の挙動を確認するときにコメントアウトを消してください
        //ta = Resources.Load<TextAsset>("Scenario/" + gameModeName + scenarioName);

        //デバッグ用 ↑の確認をするときはコメントアウトしてください
        ta = Resources.Load<TextAsset>("Scenario/" + gName + sName);


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
