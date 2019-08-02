using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Vector3 pos;
    [SerializeField]
    private Vector3 rot;
    [SerializeField]
    private EffectControl.Effect effect;
    [SerializeField]
    private bool updateOn;

    // Update is called once per frame
    void Update()
    {
        if (updateOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                EffectControl.Instance.PlayEffect(effect, pos, rot);
            }

            if (Input.GetMouseButtonDown(1))
            {
                EffectControl.Instance.StopEffect(effect);
            }
        } 
    }
}
