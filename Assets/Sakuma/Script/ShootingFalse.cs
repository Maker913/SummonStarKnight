using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingFalse : MonoBehaviour
{

    public GameObject[] falseData;

    void Start()
    {
        if (StageCobtroller.Shooting == false)
        {
            for (int i = 0; i < falseData.Length; i++)
            {
                falseData[i].SetActive(false);
            }



            this.gameObject.SetActive(false);
        }
    }

}
