using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraController : MonoBehaviour
{
    public GameObject[] auraObj;

    public void AuraOn(int num)
    {
        for(int i = 0; i < auraObj.Length; i++)
        {
            auraObj[i].SetActive(i == num);

        }
    }

}
