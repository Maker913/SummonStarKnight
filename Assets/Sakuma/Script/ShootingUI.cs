using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingUI : MonoBehaviour
{
    [SerializeField]
    GameObject[] ShootingObj;
    [SerializeField]
    GameObject[] NonShootingObj;

    // Start is called before the first frame update
    void Start()
    {
        if(StageCobtroller.Shooting)
        {
            for(int i=0;i<ShootingObj.Length; i++)
            {
                ShootingObj[i].SetActive(true);
            }
            for (int i = 0; i < NonShootingObj.Length; i++)
            {
                NonShootingObj[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < ShootingObj.Length; i++)
            {
                ShootingObj[i].SetActive(false);
            }
            for (int i = 0; i < NonShootingObj.Length; i++)
            {
                NonShootingObj[i].SetActive(true);
            }
        }
    }


}
