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
    bool Flag;

    int HP;

    // Start is called before the first frame update
    void Start()
    {
        if (Flag)
        {
            HP = gameController.GetComponent<GameController>().myHP;
            bar.maxValue = HP;
        }else{
            HP = gameController.GetComponent<GameController>().tekiHP;
            bar.maxValue = HP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Flag)
        {
            HP = gameController.GetComponent<GameController>().myHP;
            bar.value = HP;
        }else{
            HP = gameController.GetComponent<GameController>().tekiHP;
            bar.value = HP;
        }
    }
}
