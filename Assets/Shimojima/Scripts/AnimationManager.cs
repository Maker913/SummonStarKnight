using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator model;
    public Animator player;
    void Update()
    {
        
    }

    public void Stop()
    {
        if (StageCobtroller.stageNum != 3&&!StageCobtroller .Shooting )
        {
            model.SetFloat("Spead", 0);
            player.SetFloat("Spead", 0);
        }
    }
    public void ReState()
    {
        if (StageCobtroller.stageNum != 3 && !StageCobtroller.Shooting)
        {
            model.SetFloat("Spead", 1);
            player.SetFloat("Spead", 1);
        }
    }

    public void ModelSet(GameObject gameObject2,GameObject gameObject )
    {
        if (StageCobtroller.stageNum != 3 && !StageCobtroller.Shooting)
        {
            model = gameObject2.GetComponent<Animator>();
            player = gameObject.GetComponent<Animator>();
        }
    }

    public void AnimationStart(int num,string aTrigger)
    {
        if (StageCobtroller.stageNum != 3 && !StageCobtroller.Shooting)
        {
            if (num == 1)
            {
                player.SetTrigger(aTrigger);
            }
            else
            {
                model.SetTrigger(aTrigger);
            }
        }
    }
}
