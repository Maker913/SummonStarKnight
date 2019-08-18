using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyName : MonoBehaviour
{
    public void EnemyNameStart()
    {
        GetComponent<Animator>().SetBool("EnemyName", true);
    }
}
