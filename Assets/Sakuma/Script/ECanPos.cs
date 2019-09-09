using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECanPos : MonoBehaviour
{

    [SerializeField]
    private Vector3[] Pos;
    [SerializeField]
    private float[] Sc;
    [SerializeField]
    private float sclt = 0;

    void Start()
    {
        transform.localPosition = Pos[StageCobtroller.stageNum - 1];
        transform.localScale = new Vector3(Sc[StageCobtroller.stageNum - 1] * 1.1f, Sc[StageCobtroller.stageNum - 1], Sc[StageCobtroller.stageNum - 1]);
    }

    //private void Update()
    //{
    //    transform.localScale = new Vector3(Sc[StageCobtroller.stageNum - 1]*sclt, Sc[StageCobtroller.stageNum - 1], Sc[StageCobtroller.stageNum - 1]);

    //}

}
