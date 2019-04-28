using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct Technique
{
    public string Name;
    public float Power;
    public float Time;
    public int[] Code;
}




public class GameController : MonoBehaviour
{

    //現在のゲームモード
    public int gameMode;
    //技
    public Technique[] technique;
    //選択された技
    public int weapon;

    ////Delay関数内で主に使うもの

    //Delay中に停止しておく時間
    private float delayTime;
    //経過時間
    private float elapsedTime;
    //次のゲームモードを保存しておくバッファ
    private int nextGameMode = 0;


    //PadControllerアクセス用
    [SerializeField]
    private GameObject padControllerObj;
    private PadController2 padController2;

    //テキストパットとか奴
    [SerializeField]
    private GameObject textPadObj;
    [SerializeField]
    private GameObject textObj;
    private Text text;

    void Start()
    {
        gameMode = 1;
        text = textObj.GetComponent<Text>();
        padController2 = padControllerObj.GetComponent<PadController2>();
    }



    void Update()
    {
        Debug.Log(gameMode);
        switch (gameMode)
        {
            case 0:
                Delay();
                break;
            case 1:
                Stert();
                break;
            case 2:
                WaitInput();
                break;
            case 3:
                MyAtk();
                break;
            case 4:
                EnemyAtk();
                break;
            case 5:
                Menu();
                break;
            default:
                break;
        }
    }






    private void EnemyAtk()
    {
        text.text = "攻撃されますた\n痛いンゴwwwww";
        ModeChange(2, 3f);
    }

    private void MyAtk()
    {
        text.text = technique[weapon].Name+"を召喚し、攻撃しますた";
        ModeChange(4, 3f);
    }

    private void WaitInput()
    {
        text.text = "";
        padController2.Pad = true;
        textPadObj.SetActive(false);
    }

    private void Stert()
    {
        text.text = "ゲーム開始";
        ModeChange(2, 3f);
    }

    private void Menu()
    {
        textPadObj.SetActive(false);
    }





    public void ModeChange(int nextGameMode2,float delayTime2)
    {
        elapsedTime = 0;
        nextGameMode = nextGameMode2;
        delayTime = delayTime2;
        gameMode = 0;
    }

    private void Delay()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >delayTime)
        {
            elapsedTime = 0;
            gameMode = nextGameMode;
        }
    }



}

