using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCobtroller : MonoBehaviour
{


    static public int stageNum = 1;

    static public bool Shooting = true;

    [SerializeField ]
    private GameObject [] stageObj;
    [SerializeField]
    private GameObject stageParent;

    void Start()
    {
        Instantiate(stageObj[stageNum-1], stageParent.transform.position, Quaternion.identity, stageParent.transform);
    }

}
