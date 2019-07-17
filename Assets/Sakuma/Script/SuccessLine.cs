using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessLine : MonoBehaviour
{
    private float time;
    public bool check = false;
    public bool check2 = false;

    private bool check3 = false;

    private Material[] anime;
    private int cont = 0;
    // Update is called once per frame
    void Update()
    {

        if (check==true&&check3 ==false )
        {
            check2 = true;
            time = 0;
            cont = transform.childCount;
            anime = new Material[cont];


            for (int i=0;i< cont; i++)
            {
                anime[i] = transform.GetChild(i).GetComponent<UILineRenderer>().material;
            }
        }

        if(check2)
        {
            time += Time.deltaTime;


            for(int i = 0; i < cont; i++)
            {
                //anime[i].color.a = (byte)((0.5f - time) / 0.5f);
                anime[i].SetFloat("_Fade", (0.5f - time) / 0.5f);
            }

            if (time > 0.5f)
            {
                check2 = false;
                Destroy(transform.parent.gameObject);
            }
        }


        check3 = check; 

    }


    public void Success(byte data)
    {
        foreach (Transform n in transform)
        {
            n.gameObject.GetComponent<UILineRenderer>().color.a = data;
        }
    }

}
