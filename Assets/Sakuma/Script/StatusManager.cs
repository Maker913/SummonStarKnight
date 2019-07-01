using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public int playerHP;
    public int enemyHP;

    public int playerAtk;
    public int enemyAtk;

    public float gageSpeed;
    public float summonGage;

    public int playerAtkDef;

    [System.Serializable]
    public struct Ability
    {
        public float gageSpeedDef;
        public int enemyAtkDef;
        public int enemyHPDef;
    }
    [SerializeField]
    public  Ability[] abilities = new Ability[3];


    public int EnemyActionrange=2;







    //ここから先久野変数
    private int playerAtkBuf;

    private int reoSlip;
    private int scorSlip;
    public int pisBarrier;
    private int virgoBarrier;
    private int LibraTurn;
    private int aquariTurn;
    //private int cancerAtk;
    //private int taurusAtk;
    //private int sagiAtk;
    




    private void Start()
    {
        gageSpeed = abilities[StageCobtroller .stageNum -1].gageSpeedDef ;
        enemyAtk = abilities[StageCobtroller.stageNum - 1].enemyAtkDef;
        enemyHP = abilities[StageCobtroller.stageNum - 1].enemyHPDef;
        playerAtk = playerAtkDef;
        playerAtkBuf = playerAtk;
    }

    public void SummonCheck(int num)
    {
        switch (num)
        {
            case 0:
                //獅子座を召還した時の処理 スリップ3ターン
                reoSlip = 3;
                break;
            case 1:
                //うお座　バリア1
                pisBarrier = 1;
                break;
            case 2:
                //蟹　攻撃1.5
                playerAtk = (int)(playerAtk * 1.5f);
                playerAtkBuf = playerAtk;
                break;
            case 3:
                //サソリ　毒3ターン
                scorSlip = 3;
                break;
            case 4:
                //双子　gemini 攻撃回数が2回に増える しょうま
                break;
            case 5:
                //山羊　Capri　制限ターン2のびる
                break;
            case 6:
                //乙女　バリア3
                virgoBarrier = 3;
                break;
            case 7:
                //天秤　指3秒+
                LibraTurn = 1;
                break;
            case 8:
                //射手　攻撃3倍
                playerAtk *= 3;
                playerAtkBuf = playerAtk;
                break;
            case 9:
                //水瓶　敵の攻撃半減
                aquariTurn = 1;
                break;
            case 10:
                //牡羊 Aries 制限ターン5のびる
                break;
            case 11:
                //牡牛　攻撃2倍
                playerAtk *= 2;
                playerAtkBuf = playerAtk;
                break;
            default:
                break;
        }
    }

    public void TurnCheck()
    {
        enemyAtk = abilities[StageCobtroller.stageNum - 1].enemyAtkDef;
        gageSpeed = abilities[StageCobtroller.stageNum - 1].gageSpeedDef;
        playerAtk = playerAtkBuf;
        //獅子
        if (reoSlip > 0)
        {
            reoSlip--;
            enemyHP -= 5;
        }
        //サソリ
        if (scorSlip > 0)
        {
            scorSlip--;
            enemyHP -= 5;
        }
        //魚
        if (pisBarrier > 0)
        {
            enemyAtk = 0;
        }
        //乙女
        if (virgoBarrier > 0)
        {
            enemyAtk = 0;
        }
        //天秤
        if(LibraTurn > 0)
        {
            gageSpeed += 3;
        }
        //水瓶
        if (aquariTurn > 0)
        {
            enemyAtk /= 2;
            aquariTurn--;
        }

        //敵　魚
        if (enemyPis > 0)
        {
            gageSpeed -= 2;
            enemyPis--;
        }
        //敵　蟹
        if(enemyCancer > 0)
        {
            playerAtk /= 2;
            enemyCancer--;
        }

    }


    public void BarrierCheck()
    {
        //魚
        if(pisBarrier > 0) {
            pisBarrier--;
        }
        //乙女
        if (virgoBarrier > 0)
        {
            virgoBarrier--;
        }

    }









    //ここまでプレイヤー処理




    private int enemyPis=0;
    private int enemyCancer = 0;

    public void EnemyAction(int num)
    {
        switch (num)
        {
            case 1:
                //操作時間減少　魚
                enemyPis = 2;
                break;
            case 2:
                //プレイヤーの攻撃半減　1ターン？
                enemyCancer = 1;
                break;
            case 3:
                //制限ターン2減らす 蛇 Ophiuchus
                break;
            default:
                break;

        }
        


    }


    public void EnemyTurnCheck()
    {
        
        


    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
