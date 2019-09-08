using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator model;
    public Animator player;
    public GameObject[] takitoka=new GameObject [4]; 

    void Update()
    {
    }

    public void Stop()
    {

        if (!StageCobtroller.Shooting)
        {
            player.SetFloat("Spead", 0);
                model.SetFloat("Spead", 0);
                player.SetFloat("Spead", 0);

            if(StageCobtroller .stageNum == 1)
            {
                for(int i=0;i<takitoka.Length;i++)
                {
                    takitoka[i].transform.GetChild(0).GetComponent<ParticleSystem>().Pause();
                }
            }


        }
    }
    public void ReState()
    {
        if (!StageCobtroller.Shooting)
        {
            player.SetFloat("Spead", 1);

                model.SetFloat("Spead", 1);
            if (StageCobtroller.stageNum == 1)
            {
                for (int i = 0; i < takitoka.Length; i++)
                {
                    takitoka[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                }
            }

        }
    }

    public void ModelSet(GameObject gameObject2,GameObject gameObject )
    {
        if (!StageCobtroller.Shooting)
        {
            player = gameObject.GetComponent<Animator>();

                model = gameObject2.GetComponent<Animator>();


        }
    }

    public void AnimationStart(float rimitTime, int num,string aTrigger)
    {
        if (!StageCobtroller.Shooting)
        {
            StartCoroutine(Animation(rimitTime, num, aTrigger));
        }
    }

    IEnumerator Animation(float rimit, int num, string aTrigger)
    {
        yield return new WaitForSeconds(rimit);
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
