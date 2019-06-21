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

    [SerializeField]
    private float gageSpeedDef;
    [SerializeField]
    private int enemyAtkDef;
    [SerializeField]
    private int playerAtkDef;
    public int EnemyActionrange=1;
    //ここから先久野変数
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
        gageSpeed = gageSpeedDef;
        enemyAtk = enemyAtkDef;
        playerAtk = playerAtkDef;

    }

    public void SummonCheck(int num)
    {
        switch (num)
        {
            case 0:
                //獅子座を召還した時の処理
                reoSlip = 3;
                break;
            case 1:
                //うお座
                pisBarrier = 1;
                break;
            case 2:
                //蟹
                playerAtk = (int)(playerAtk * 1.5f);
                break;
            case 3:
                //サソリ
                scorSlip = 3;
                break;
            case 4:
                //双子　gemini 攻撃回数が2回に増える しょうま
                break;
            case 5:
                //山羊　Capri　制限ターン2のびる
                break;
            case 6:
                //乙女
                virgoBarrier = 3;
                break;
            case 7:
                //天秤
                LibraTurn = 1;
                break;
            case 8:
                //射手
                playerAtk *= 3;
                break;
            case 9:
                //水瓶
                aquariTurn = 1;
                break;
            case 10:
                //牡羊 Aries 制限ターン5のびる
                break;
            case 11:
                //牡牛
                playerAtk *= 2;
                break;
            default:
                break;
        }
    }

    public void TurnCheck()
    {
        enemyAtk = enemyAtkDef;
        gageSpeed = gageSpeedDef;
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

        //敵
        if (data > 0)
        {
            gageSpeed -= 2;
            data--;
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




    private int data=0;

    public void EnemyAction(int num)
    {
        switch (num)
        {
            case 1:
                //操作時間減少　魚
                data = 2;
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
