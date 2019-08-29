using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nazore : MonoBehaviour
{
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialFlg.SummonBefore)
        {
            time += Time.deltaTime;
        }
        if (time > 1f)
        {
            Destroy(gameObject);
        }
    }
}
