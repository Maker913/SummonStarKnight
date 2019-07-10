using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECanPos : MonoBehaviour
{

    [SerializeField]
    private Vector3[] Pos;
    [SerializeField]
    private float[] Sc;


    void Start()
    {
        transform.localPosition = Pos[StageCobtroller.stageNum - 1];
        transform.localScale = new Vector3(Sc[StageCobtroller.stageNum - 1], Sc[StageCobtroller.stageNum - 1], Sc[StageCobtroller.stageNum - 1]);
    }

}
