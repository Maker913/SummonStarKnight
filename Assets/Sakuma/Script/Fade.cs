using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    Image image;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;


        if (time > 1.75f)
        {
            image.color = new Color(1, 1, 1, Mathf.Abs ( time-2f)*2);
        }

    }
}
