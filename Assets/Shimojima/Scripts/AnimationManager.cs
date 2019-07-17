using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator model;

    void Update()
    {
        
    }

    public void Stop()
    {
        if (StageCobtroller.stageNum != 3&&!StageCobtroller .Shooting )
        {
            model.SetFloat("Spead", 0);
        }
    }
    public void ReState()
    {
        if (StageCobtroller.stageNum != 3 && !StageCobtroller.Shooting)
        {
            model.SetFloat("Spead", 1);
        }
    }

    public void ModelSet(GameObject gameObject2)
    {
        if (StageCobtroller.stageNum != 3 && !StageCobtroller.Shooting)
        {
            model = gameObject2.GetComponent<Animator>();
        }
    }

    public void AnimationStart(string aTrigger)
    {
        if (StageCobtroller.stageNum != 3 && !StageCobtroller.Shooting)
        {
            model.SetTrigger(aTrigger);
        }
    }
}
