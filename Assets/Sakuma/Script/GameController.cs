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
    public Technique[] nomalAttack;
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

    //HP
    public int myHP;
    public int tekiHP;

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
                Battle();
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
        padController2.Pad = false;
        textPadObj.SetActive(true);
        text.text = "攻撃されました";
        myHP -= 10;
        ModeChange(2, 1f);

    }

    private void MyAtk()
    {
        padController2.Pad = false;
        textPadObj.SetActive(true);
        text.text = "攻撃しました";
        tekiHP -= 10;
        ModeChange(2, 1f);

    }

    private void Battle()
    {
        if (myHP > 0 && tekiHP > 0)
        {
            text.text = "";
            padController2.Pad = true;
            textPadObj.SetActive(false);
        }else if(myHP <= 0)
        {
            text.text = "負け";
            padController2.Pad =false ;
            textPadObj.SetActive(true);
        }
        else
        {
            text.text = "勝ち";
            padController2.Pad = false;
            textPadObj.SetActive(true);
        }
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

