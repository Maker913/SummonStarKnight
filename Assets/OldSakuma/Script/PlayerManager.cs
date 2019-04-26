using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public int HP;
    public int Atk;
    public GameObject[] summonModeCollider;
    public int summonMode;

    [SerializeField]
    private int startMode;
    [SerializeField]
    private GameObject padControllerObj;
    [SerializeField]
    private GameObject stageObj;
    private PadController padController;
    //private EnemyManager[] enemyComponent=new  EnemyManager[100];
    private int enemycont;

    private bool Atkflg;
    public bool Atksignal;
    // Start is called before the first frame update
    void Start()
    {
        ChangeSummonMode(startMode);
        padController = padControllerObj.GetComponent<PadController>();
        Atksignal = false;
    }

    private void FixedUpdate()
    {

        if (padController.Atktouch == false && Atkflg)
        {
            Atksignal = true;
        }
        else
        {
            Atksignal = false;
        }
        Atkflg = padController.Atktouch;

        if (summonMode == 0)
        {
            Debug.Log("通常");
        }
        else if (summonMode == 1)
        {
            Debug.Log("牡羊座");
        }
        else if (summonMode == 2)
        {
            Debug.Log("獅子座");
        }
        else if (summonMode == 3)
        {
            Debug.Log("魚座");
        }
        else if (summonMode == 4)
        {
            Debug.Log("蟹座");
        }
    }

    public void ChangeSummonMode(int changeNum)
    {
        for (int i = 0; i < summonModeCollider.Length; i++)
        {
            if (i == changeNum)
            {
                summonModeCollider[i].SetActive(true);
            }
            else
            {
                summonModeCollider[i].SetActive(false);
            }
        }
        summonMode = changeNum;

    }

}
