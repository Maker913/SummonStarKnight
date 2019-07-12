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
        model.SetFloat("Spead", 0);
    }
    public void ReState()
    {
        model.SetFloat("Spead", 1);
    }

    public void ModelSet(GameObject gameObject2)
    {
        model = gameObject2.GetComponent<Animator>();
        
    }

    public void AnimationStart(string aTrigger)
    {
        model.SetTrigger(aTrigger);
    }
}
