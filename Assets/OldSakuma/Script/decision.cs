using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decision : MonoBehaviour
{


    private bool test;


    // Start is called before the first frame update
    void Start()
    {
        test = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(test);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Atk")
        {
            test = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Atk")
        {
            test = false;
        }
    }
}
