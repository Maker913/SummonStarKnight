using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemy;
    private int eLength;

    void Start()
    {
        eLength = enemy.Length;
        GameObject enemyPre = Instantiate(enemy[0], new Vector3(0,0,0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
