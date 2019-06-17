﻿using System.Collections;
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
    //public int myHP;
    //public int tekiHP;
    //public int gage;


    //2のみ開始時処理使用
   public  int startPas;


    //敵星座板の奴
    [SerializeField]
    private GameObject EnjObj;
    private Enj enj;

    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private GameObject gageobj;
    private Image gaged;

    private int dcont = 0;
    private float cameradTime = 0;


    [SerializeField]
    private GameObject StatusManagerObj;
    private StatusManager statusManager;

    private int enemyTurn;






    static public string result = "NULL";
    [Space(10)]
    [Header("ここからエフェクト")]

    //ここからエフェクト用
    [SerializeField]
    private GameObject teki;


    void Start()
    {
        enemyTurn = Random.Range(2, 5);
        gaged = gageobj.GetComponent<Image>();
        gameMode = 1;
        text = textObj.GetComponent<Text>();
        padController2 = padControllerObj.GetComponent<PadController2>();
        enj = EnjObj.GetComponent<Enj>();
        statusManager = StatusManagerObj.GetComponent<StatusManager>();
    }



    void Update()
    {
        //Debug.Log(gameMode);
        gaged.fillAmount = (float)statusManager .summonGage  / 100f;
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
            case 6:
                Win();
                break;
            case 7:
                Lose();
                break;
            case 8:
                Sumon();
                break;
            case 9:
                Stop();
                break;
            case 10:
                ChaneSceen();
                break;
            case 11:
                EnemyMove();
                break;
            case 12:
                Battlesoon();
                break;
            default:
                break;
        }
    }



    private void EnemyMove()
    {
        
        if (enemyTurn <=0)
        {
            text.text = "2ターンの間制限時間減少";
            statusManager.EnemyAction(Random.Range(1, statusManager .EnemyActionrange +1));
            textPadObj.SetActive(true);
            ModeChange(12, 2);
            enemyTurn = Random.Range(2, 5);
        }
        else
        {
            ModeChange(12, 0);
            enemyTurn--;
        }
        
    }


    private void Battlesoon()
    {
        statusManager.EnemyTurnCheck();
        statusManager.TurnCheck();
        ModeChange(2, 1f);
        camera.GetComponent<CameraController2>().SetCamera(2, 1);
    }

    private void ChaneSceen()
    {
        SceneChanger.instance.LoadScene("resultkari", 1);
    }

    private void Sumon()
    {
        enj.GetComponent<Animator>().SetBool("Open", true);
        statusManager.SummonCheck(weapon);
        text.text = technique[weapon ].Name +"を召喚しました";
        padController2.Pad = false;
        textPadObj.SetActive(true);


        ModeChange(9, 2);
    }


    private void Stop()
    {
        ModeChange(12, 0);
        textPadObj.SetActive(false );
        //camera.GetComponent<CameraController2>().SetCamera(0, 1);
    }

    private void Lose()
    {
        result = "敗北";
        text.text = "負け";
        padController2.Pad = false;
        textPadObj.SetActive(true);

        ModeChange(10, 1);
    }

    private void Win()
    {
        result = "勝利";
        text.text = "勝ち";
        padController2.Pad = false;
        textPadObj.SetActive(true);

        ModeChange(10, 1);
    }

    private void EnemyAtk()
    {
        if(cameradTime == 0 || cameradTime > 1)
        {
            if (dcont == 0)
            {
                camera.GetComponent<CameraController2>().SetCamera(0, 1);
                dcont++;
                cameradTime += Time.deltaTime;
                enj.GetComponent<Animator>().SetBool("Open", true);
            }
            else
            {
                dcont = 0;
                cameradTime = 0;


                teki.GetComponent<Animator>().SetTrigger("Attack");
                padController2.Pad = false;
                textPadObj.SetActive(true);
                text.text = "攻撃されました";
                statusManager.playerHP  -= statusManager.enemyAtk ;
                statusManager.BarrierCheck();
                if (statusManager.playerHP <= 0)
                {
                    ModeChange(7, 2f);
                }
                else
                {
                    ModeChange(11, 2f);
                }
            }

        }
        else
        {
            cameradTime += Time.deltaTime;
        }


    }

    private void MyAtk()
    {
        if (cameradTime == 0 || cameradTime > 1)
        {
            if (dcont == 0)
            {
                camera.GetComponent<CameraController2>().SetCamera(3, 1);
                dcont++;
                cameradTime += Time.deltaTime;
                enj.GetComponent<Animator>().SetBool("Open", true);
            }
            else
            {
                dcont = 0;
                cameradTime = 0;

                statusManager.summonGage += 40;
                if (statusManager.summonGage > 100)
                {
                    statusManager.summonGage = 100;
                }

                
                padController2.Pad = false;
                textPadObj.SetActive(true);
                text.text = "攻撃しました";
                statusManager.enemyHP  -= statusManager.playerAtk;
                if (statusManager.enemyHP <= 0)
                {
                    ModeChange(6, 2f);
                }
                else
                {
                    ModeChange(11, 2f);
                }
            }

        }
        else
        {
            cameradTime += Time.deltaTime;
        }

    }

    private void Battle()
    {
        if(startPas == 0)
        {
            enj.GetComponent<Animator>().SetBool("Start", true);
            enj.GetComponent<Animator>().SetBool("Open", false);
            enj.NextGame();
            startPas = 1;
            
        }
        text.text = "";
        padController2.Pad = true;
        textPadObj.SetActive(false);
        
    }

    private void Stert()
    {
        textPadObj.SetActive(true);
        text.text = "ゲーム開始";
        ModeChange(12, 3f);
    }

    private void Menu()
    {
        textPadObj.SetActive(false);
    }





    public void ModeChange(int nextGameMode2,float delayTime2)
    {
        startPas = 0;
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

