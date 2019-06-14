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
            HP = gameController.GetComponent<GameController>().myHP;
            bar.maxValue = HP;
        }else if(Flag == 1){
            HP = gameController.GetComponent<GameController>().tekiHP;
            bar.maxValue = HP;
        }else if(Flag == 3){
            HP = gameController.GetComponent<GameController>().myHP;
            bar.maxValue = HP;
            bar.value = HP;
            
        }else if (Flag == 4){
            HP = gameController.GetComponent<GameController>().tekiHP;
            bar.maxValue = HP;
            bar.value = HP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Flag == 0)
        {
            HP = gameController.GetComponent<GameController>().myHP;
            bar.value = HP;
        }
        else if (Flag == 1)
        {
            HP = gameController.GetComponent<GameController>().tekiHP;
            bar.value = HP;
        }
        else if (Flag == 3)
        {
            HP = gameController.GetComponent<GameController>().myHP;
            if (bar.value > HP)
            {
                bar.value -= 10f * Time.deltaTime;
            }
        }
        else if (Flag == 4)
        {
            HP = gameController.GetComponent<GameController>().tekiHP;
            if (bar.value > HP)
            {
                bar.value -= 10f * Time.deltaTime;
            }
        }
    }
}
