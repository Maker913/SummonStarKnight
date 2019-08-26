using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMg : MonoBehaviour
{
    [SerializeField]
    GameObject VSObj;
    [SerializeField]
    GameObject EnemyNameObj;

    public void StartVSAnime()
    {
        VSObj.GetComponent<VS>().VSstart();
    }

    public void StartEnemyNameAnime()
    {
        EnemyNameObj.GetComponent<EnemyName>().EnemyNameStart();
    }
}
