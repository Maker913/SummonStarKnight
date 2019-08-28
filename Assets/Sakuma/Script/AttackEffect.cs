using System.Collections;
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
    GameObject EffectPr;

    [SerializeField]
    float Spead;




    bool effectOn = false;
    bool AttackTrue = false;

    GameObject EffectObj;

    public float time;

    float dis = 0;

    bool reverse;

    float reverseSpeed;

    public void EffectSettrue()
    {
        effectOn = true;
        AttackTrue = true;
        EffectObj = Instantiate(EffectPr ,AttackObj[StageCobtroller .stageNum-1 ].transform.position ,Quaternion.identity );
        time = 0;
        dis = Vector3.Distance(EffectObj.transform.position, ReverseObj[StageCobtroller.stageNum - 1].transform.position);
        reverse = false;
    }

    public void EffectSetfalse()
    {
        effectOn = true;
        AttackTrue = false ;
        EffectObj = Instantiate(EffectPr, AttackObj[StageCobtroller.stageNum - 1].transform.position, Quaternion.identity);
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
                        reverse = true;
                        time = 0;
                        dis= Vector3.Distance(EffectObj.transform.position, EndPosObj[StageCobtroller.stageNum - 1].transform.position);

                        reverseSpeed = Spead*1.5f;
                    }
                }
                else
                {
                    EffectObj.transform.position = Vector3.MoveTowards( EffectObj.transform.position, EndPosObj[StageCobtroller.stageNum - 1].transform.position, dis * Time.deltaTime * reverseSpeed);

                    if (time >= 1 / reverseSpeed)
                    {
                        Destroy(EffectObj);
                        effectOn = false;




                    }


                }



            }
            else
            {

                EffectObj.transform.position = Vector3.MoveTowards(EffectObj.transform.position, ReverseObj[StageCobtroller.stageNum - 1].transform.position, dis *Time.deltaTime*Spead  );





                if (time >= 1/Spead)
                {
                    Destroy(EffectObj);
                    effectOn = false;



                }






            }




            time += Time.deltaTime;




        }







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
