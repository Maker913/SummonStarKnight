﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{

    [SerializeField]
    GameObject[] AttackObj;
    [SerializeField]
    GameObject[] ReverseObj;
    [SerializeField]
    GameObject[] EndPosObj;
    [SerializeField]
    GameObject[] EffectPr;
    [SerializeField]
    GameObject ReversePr;



    [SerializeField]
    float Spead;

    [SerializeField ]
    GameObject[] bomPr;
    [SerializeField]
    GameObject statusManagerObj;
    StatusManager statusManager;
    [SerializeField]
    GameObject padController2Obj;
    PadController2 padController2;
    [SerializeField]
    GameObject gamecontrollerObj;
    GameController gamecontroller;








    bool effectOn = false;
    bool AttackTrue = false;

    GameObject EffectObj;

    public float time;

    float dis = 0;

    bool reverse;

    float reverseSpeed;

    GameObject game;
    private void Start()
    {
        padController2 = padController2Obj.GetComponent<PadController2>();
        statusManager = statusManagerObj.GetComponent<StatusManager>();
        gamecontroller = gamecontrollerObj.GetComponent<GameController>();
    }


    public void EffectSettrue()
    {
        effectOn = true;
        AttackTrue = true;
        EffectObj = Instantiate(EffectPr[StageCobtroller .stageNum -1] ,AttackObj[StageCobtroller .stageNum-1 ].transform.position ,Quaternion.identity );
        time = 0;
        dis = Vector3.Distance(EffectObj.transform.position, ReverseObj[StageCobtroller.stageNum - 1].transform.position);
        reverse = false;
    }

    public void EffectSetfalse()
    {
        effectOn = true;
        AttackTrue = false ;
        EffectObj = Instantiate(EffectPr[StageCobtroller.stageNum - 1], AttackObj[StageCobtroller.stageNum - 1].transform.position, Quaternion.identity);
        time = 0;
        dis = Vector3.Distance(EffectObj.transform.position, ReverseObj[StageCobtroller.stageNum - 1].transform.position);
        reverse = false;
    }

    private void Update()
    {
        
        if(effectOn)
        {
            if(AttackTrue)
            {
                if (!reverse)
                {


                    EffectObj.transform.position = Vector3.MoveTowards(EffectObj.transform.position, ReverseObj[StageCobtroller.stageNum - 1].transform.position, dis * Time.deltaTime * Spead);


                    if (time >= 1 / Spead)
                    {
                        AudioManager.Instance.PlaySE(AudioManager.SeName.Counterattack);

                        reverse = true;
                        time = 0;
                        dis= Vector3.Distance(EffectObj.transform.position, EndPosObj[StageCobtroller.stageNum - 1].transform.position);

                        reverseSpeed = Spead*1.5f;
                        Instantiate(ReversePr , EffectObj.transform.position+new Vector3 (0,0,0.1f), Quaternion.identity);
                    }
                }
                else
                {
                    EffectObj.transform.position = Vector3.MoveTowards( EffectObj.transform.position, EndPosObj[StageCobtroller.stageNum - 1].transform.position, dis * Time.deltaTime * reverseSpeed);

                    if (time >= 1 / reverseSpeed)
                    {

                        AudioManager.Instance.PlaySE(AudioManager.SeName.Explosion);

                        game= Instantiate(bomPr[StageCobtroller.stageNum - 1], EffectObj.transform.position, Quaternion.identity);
                        Invoke("EfDl", 1);
                        EffectObj.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                        EffectObj.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                        Invoke("EfDl2", 1);
                        effectOn = false;



                        statusManager.summonGage += Random.Range(30, 40);
                        if (statusManager.summonGage > 100) {
                            statusManager.summonGage = 100;
                        }


                        
                        

                        if (padController2.oneLine)
                        {
                            statusManager.enemyHP -= (int)((statusManager.playerAtk + gamecontroller.combo - 1) * 1.5f);
                        }
                        else
                        {
                            statusManager.enemyHP -= statusManager.playerAtk;
                        }


                        if (statusManager.enemyHP <= 0)
                        {
                            gamecontroller.ModeChange(6, 0.5f);
                        }
                        else
                        {
                            if (!TutorialFlg.FastAtk)
                            {
                                gamecontroller.ModeChange(27, 0.5f);
                            }
                            else
                            {
                                gamecontroller.ModeChange(11, 0.5f);
                            }
                        }


                    }


                }



            }
            else
            {

                EffectObj.transform.position = Vector3.MoveTowards(EffectObj.transform.position, ReverseObj[StageCobtroller.stageNum - 1].transform.position, dis *Time.deltaTime*Spead  );





                if (time >= 1/Spead)
                {
                    AudioManager.Instance.PlaySE(AudioManager.SeName.Explosion);

                    game =Instantiate(bomPr[StageCobtroller.stageNum - 1], EffectObj.transform.position, Quaternion.identity);
                    Invoke("EfDl", 1);
                    Invoke("EfDl2", 1);
                    EffectObj.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                    EffectObj.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                    effectOn = false;



                    statusManager.summonGage += Random.Range(10, 20);
                    if (statusManager.summonGage > 100) {
                        statusManager.summonGage = 100;
                    }

                    statusManager.playerHP -= statusManager.enemyAtk;
                    statusManager.BarrierCheck();

                    if (statusManager.playerHP <= 0)
                    {
                        gamecontroller . ModeChange(7, 0.5f);
                    }
                    else
                    {
                        gamecontroller . ModeChange(11, 0.5f);
                    }

                }






            }




            time += Time.deltaTime;




        }







    }



    public void EfDl2()
    {
        Destroy(EffectObj.gameObject);
    }

    public  void EfDl() {
        Destroy(game.gameObject);
    }


    public void bttrue(float time)
    {
        Invoke("EffectSettrue",time);
    }

    public void btfalse(float time)
    {
        Invoke("EffectSetfalse", time);
    }


}
