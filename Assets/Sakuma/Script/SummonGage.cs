using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SummonGage : MonoBehaviour
{
    [SerializeField]
    private GameObject statusObj;
    private StatusManager statusManager;

    private Material material;

    float gage = 0;
    float nowGage = 0;
    // Start is called before the first frame update
    void Start()
    {
        statusManager = statusObj.GetComponent<StatusManager>();
        material = GetComponent<Image>().material;
        gage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gage = statusManager.summonGage ;

        if(nowGage < gage-1)
        {
            nowGage+=2;
        }

        if (nowGage > gage+1)
        {
            nowGage-=2;
        }

        material.SetFloat("_Gage", nowGage  /100);

    }
}
