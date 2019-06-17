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
    public int EnemyActionrange=1;
    //ここから先久野変数
    private int reoSlip;



    private void Start()
    {
        gageSpeed = gageSpeedDef; 
    }

    public void SummonCheck(int num)
    {
        switch (num)
        {
            case 0:
                //獅子座を召還した時の処理
                reoSlip = 3;
                break;
            default:
                break;
        }
    }

    public void TurnCheck()
    {
        if(reoSlip > 0)
        {
            reoSlip--;
            enemyHP -= 5;
        }
    }


    public void BarrierCheck()
    {

    }









    //ここまでプレイヤー処理




    private int data=0;

    public void EnemyAction(int num)
    {
        switch (num)
        {
            case 1:
                //操作時間減少
                data = 2;
                break;
            default:
                break;

        }
        


    }


    public void EnemyTurnCheck()
    {
        gageSpeed = gageSpeedDef;
        if (data > 0)
        {
            gageSpeed -=2;
            data--;
        }


    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
