using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    [SerializeField]
    GameObject gameController;

    [SerializeField]
    Slider bar;

    [SerializeField]
    int Flag;

    int HP;





    // Start is called before the first frame update
    void Start()
    {
        if (Flag == 0)
        {
            HP = gameController.GetComponent<StatusManager>().playerHP;
            bar.maxValue = HP;
        }else if(Flag == 1){
            HP = gameController.GetComponent<StatusManager>().abilities[StageCobtroller.stageNum - 1].enemyHPDef;
            bar.maxValue = HP;
        }else if(Flag == 3){
            HP = gameController.GetComponent<StatusManager>().playerHP;
            bar.maxValue = HP;
            bar.value = HP;
            
        }else if (Flag == 4){
            HP = gameController.GetComponent<StatusManager>().abilities[StageCobtroller.stageNum - 1].enemyHPDef;
            bar.maxValue = HP;
            bar.value = HP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Flag == 0)
        {
            HP = gameController.GetComponent<StatusManager>().playerHP;
            bar.value = HP;
        }
        else if (Flag == 1)
        {
            HP = gameController.GetComponent<StatusManager>().enemyHP;
            bar.value = HP;
        }
        else if (Flag == 3)
        {
            HP = gameController.GetComponent<StatusManager>().playerHP;
            if (bar.value > HP)
            {
                bar.value -= 10f * Time.deltaTime;
            }
        }
        else if (Flag == 4)
        {
            HP = gameController.GetComponent<StatusManager>().enemyHP;
            if (bar.value > HP)
            {
                bar.value -= 10f * Time.deltaTime;
            }
        }
    }
}
