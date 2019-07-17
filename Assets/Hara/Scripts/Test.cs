using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Vector3 pos;
    [SerializeField]
    private EffectControl.Effect effect;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EffectControl.Instance.PlayEffect(effect, pos);
        }

        if (Input.GetMouseButtonDown(1))
        {
            EffectControl.Instance.StopEffect(effect);
        }
    }
}
