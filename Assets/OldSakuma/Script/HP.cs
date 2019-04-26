using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyobj;
    private EnemyManager enemyManager;

    [SerializeField]
    private GameObject sliderobj;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        enemyManager = enemyobj.GetComponent<EnemyManager>();
        slider = sliderobj.GetComponent<Slider>();
    }




    // Update is called once per frame
    void Update()
    {
        Vector3 p = Camera.main.transform.position;
        p.y = transform.position.y;
        transform.LookAt(p);
        slider.value = (float)enemyManager.HP/(float)enemyManager.MAXHP;
    }
}
