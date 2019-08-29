using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReverseEf : MonoBehaviour
{
    float time;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        material = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime*3;
        material.SetFloat("_Rate",time);
        if (time >= 1)
        {
            Destroy(gameObject);
        }
    }
}
