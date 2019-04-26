using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject playerManagerobj;
    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject stage = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);
        playerManager = playerManagerobj.GetComponent<PlayerManager>();
        //playerManager.EnemyAcquisition(stage);
    }

}
