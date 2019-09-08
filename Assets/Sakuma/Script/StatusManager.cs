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
        public int turn;
    }
    [SerializeField]
    public  Ability[] abilities = new Ability[3];


    public int EnemyActionrange=4;







    //ここから先久野変数


    private int reoSlip;
    private int scorSlip;
    public int pisBarrier;
    private int virgoBarrier;
    private int LibraTurn;
    private int aquariTurn;
    public bool yagichan = false;
    //private int cancerAtk;
    //private int taurusAtk;
    //private int sagiAtk;
    




    private void Start()
    {
        gageSpeed = abilities[StageCobtroller .stageNum -1].gageSpeedDef ;
        enemyAtk = abilities[StageCobtroller.stageNum - 1].enemyAtkDef;
        enemyHP = abilities[StageCobtroller.stageNum - 1].enemyHPDef;
        playerAtk = playerAtkDef;

    }

    public string SummonCheck(int num)
    {
        string text="";
        switch (num)
        {
            case 0:
                //獅子座を召還した時の処理
                text = "獅子座の力を借りました\n少しの間敵にダメージを持続的に与えます";
                reoSlip = 3;
                break;
            case 1:
                //うお座
                text = "魚座の力を借りました\n一度だけ敵の攻撃を受けません";
                pisBarrier = 1;
                break;
            case 2:
                //蟹
                text = "蟹座の力を借りました\n攻撃力が上昇しました";
                playerAtk = (int)(playerAtk * 1.5f);
                break;
            case 3:
                //サソリ
                text = "蠍座の力を借りました\n毒により、持続的にダメージを与えます";
                scorSlip = 3;
                break;
            case 4:
                //双子　gemini 攻撃回数が2回に増える しょうま
                text = "双子座の力を借りました\n攻撃力が2倍になりました";
                playerAtk = (int)(playerAtk * 2f);
                break;
            case 5:
                //山羊　Capri　制限ターン2のびる
                text = "山羊座の力を借りました\n召喚ゲージを毎ターン少しづつ取得できます";
                yagichan = true;
                break;
            case 6:
                //乙女
                text = "乙女座の力を借りました\n三回の間敵の攻撃を受けません";
                virgoBarrier = 3;
                break;
            case 7:
                //天秤
                text = "天秤座の力を借りました\n敵のゲージがたまる速度が遅くなりました";
                LibraTurn = 1;
                break;
            case 8:
                //射手
                text = "射手座の力を借りました\n攻撃力が大幅に上昇しました";
                playerAtk *= 3;
                break;
            case 9:
                //水瓶
                text = "水瓶座の力を借りました\n敵の攻撃力を減少させました";
                aquariTurn = 1;
                break;
            case 10:
                //牡羊 Aries 制限ターン5のびる
                text = "牡羊座の力を借りました\n体力を回復しました";
                playerHP += 30;
                if (playerHP > 100)
                {
                    playerHP = 100;
                }
                break;
            case 11:
                //牡牛
                text = "牡牛座の力を借りました\n攻撃力が上昇しました";
                playerAtk *= 2;
                break;
            default:
                break;
        }
        return text;
    }

    public void TurnCheck()
    {
        enemyAtk = abilities[StageCobtroller.stageNum - 1].enemyAtkDef;
        gageSpeed = abilities[StageCobtroller.stageNum - 1].gageSpeedDef;
        //獅子
        if (reoSlip > 0)
        {
            reoSlip--;
            if (enemyHP > 10)
            {
                enemyHP -= 10;
            }

        }
        //サソリ
        if (scorSlip > 0)
        {
            scorSlip--;
            if (enemyHP > 10)
            {
                enemyHP -= 10;
            }
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
        if (data[0] > 0)
        {
            gageSpeed -= 2;
            data[0]--;
        }
        if (data[1] > 0)
        {
            enemyAtk += 10;
            data[1]--;
        }
        if (data[2] > 0)
        {
            gageSpeed -= 3;
            data[2]--;
        }
        if (data[4] > 0)
        {
            gageSpeed -= 1;
            data[4]--;
        }
        if (data[5] > 0)
        {
            enemyAtk += 5;
            data[5]--;
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




    private int[] data=new int[10];
    public string EnemyAction(int num)
    {
        string text = "";
        switch (num)
        {
            case 1:
                //操作時間減少　魚
                data[0] = 2;
                text = " 2ターンの間制限時間減少";
                break;
            case 2:
                data[1] = 1;
                text = " 1ターンの間攻撃力上昇";
                break;
            case 3:
                data[2] = 1;
                text = " 1ターンの間制限時間大幅減少";
                break;
            case 4:
                playerAtk -= 2;
                text = " 受けるダメージが少し減少";
                break;
            case 5:
                data[4] = 5;
                text = " 5ターンの間制限時間が少し減少";
                break;
            case 6:
                data[5] = 3;
                text = " 3ターンの間攻撃力が少し上昇";
                break;
            default:
                break;

        }

        return text;

    }


    public void EnemyTurnCheck()
    {
        
        


    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
