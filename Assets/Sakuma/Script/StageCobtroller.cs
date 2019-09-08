using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCobtroller : MonoBehaviour
{


    static public int stageNum = 1;

    static public bool Shooting =false;//*/true ;

    static public int[] Technique= { 0,-1,-1};

    static public int Score=0;

    static public bool Win = false;

    [SerializeField ]
    private GameObject [] stageObj;
    [SerializeField]
    private GameObject stageParent;


    [SerializeField]
    private GameObject gamecontroller;


    [SerializeField]
    private GameObject playerObj;
    [SerializeField]
    private GameObject[] enemyObj=new GameObject [3];

    [SerializeField]
    private Vector3[] playerPos = new Vector3[3];
    [SerializeField]
    private Vector3[] enemyPos = new Vector3[3];

    [SerializeField]
    private Quaternion[]  playerRot = new Quaternion[3];
    [SerializeField]
    private Quaternion[] enemyRot = new Quaternion[3];

    [SerializeField]
    private Vector3[] attackEfPos = new Vector3[3];

    [SerializeField]
    GameObject mag;

    void Start()
    {

        GameObject data3 = Instantiate(stageObj[stageNum - 1], stageParent.transform.position, Quaternion.identity, stageParent.transform);


        GameObject data1 = (GameObject)Instantiate(playerObj , playerPos[stageNum -1]*0.33f + stageParent.transform.position, playerRot[stageNum - 1], stageParent.transform);
        if (StageCobtroller.Shooting == false)
        {
            GameObject data = (GameObject)Instantiate(enemyObj[stageNum - 1], enemyPos[stageNum - 1] * 0.33f + stageParent.transform.position, enemyRot[stageNum - 1], stageParent.transform);



            gamecontroller.GetComponent<GameController>().auraController = data1.transform.GetChild(3).gameObject.GetComponent<AuraController>();
            mag.GetComponent<AnimationManager>().ModelSet(data,data1);

        }

        if (stageNum == 1)
        {
            mag.GetComponent<AnimationManager>().takitoka[0] = data3.transform.GetChild(0).gameObject .transform.GetChild(4).gameObject;
            mag.GetComponent<AnimationManager>().takitoka[1] = data3.transform.GetChild(0).gameObject .transform.GetChild(5).gameObject;
            mag.GetComponent<AnimationManager>().takitoka[2] = data3.transform.GetChild(0).gameObject .transform.GetChild(6).gameObject;
            mag.GetComponent<AnimationManager>().takitoka[3] = data3.transform.GetChild(0).gameObject .transform.GetChild(7).gameObject;





        }

        gamecontroller.GetComponent<GameController>().ObjPosSet(attackEfPos [stageNum-1]);
        
    }

}
