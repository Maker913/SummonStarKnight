using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    static public AnimationManager animationManager;
    public Animator model;

    void Update()
    {
        
    }

    public void ModelSet(GameObject gameObject)
    {
        model = gameObject.GetComponent<Animator>();
    }

    public void AnimationStart(string aTrigger)
    {
        model.SetTrigger(aTrigger);
    }
}
