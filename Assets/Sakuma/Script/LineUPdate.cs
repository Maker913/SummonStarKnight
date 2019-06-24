using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUPdate : MonoBehaviour
{
    private Material material;
    private UILineRenderer UIline;
    // Start is called before the first frame update
    void Start()
    {
        
        UIline = GetComponent<UILineRenderer>();
        material = UIline.material;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("_Height", Vector2.Distance (UIline.points[0],UIline .points [1]  ));
        material.SetFloat("_Width", UIline.width *2);
    }
}
