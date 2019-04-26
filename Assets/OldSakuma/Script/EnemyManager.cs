using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private bool inRange;
    public int HP;
    public int MAXHP;
    [SerializeField]
    private GameObject playerobj;
    private PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        playerManager = playerobj.GetComponent<PlayerManager>(); 
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (playerManager.Atksignal)
        {
            Damege(playerManager.Atk);
        }
    }

    public void Damege(int atk)
    {
        if (inRange)
        {
            HP -= atk;
            //Debug.Log(transform.name+"が"+atk+"のダメージを受けました。");
            //Debug.Log(HP);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Atk")
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Atk")
        {
            inRange = false;
        }
    }
}
