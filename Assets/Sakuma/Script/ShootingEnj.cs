using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShootingEnj : MonoBehaviour
{

    [SerializeField]
    private GameObject[] sterPos;
    [SerializeField]
    private GameObject lineObj;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject lineParent;


    [SerializeField]
    private GameObject padControllerObj;
    private PadController2 padController;

    public int lineNum = 0;
    public int[] lineCode = new int[2];
    [SerializeField]
    private int[] ster = new int[3];

    private int[,] LincSter = {
        { 2,8,9,0,0},
        {1,10,3 ,0,0},
        { 2,10,4,0,0},
        {3,10,11,0,0 },
        { 6,4,12,11,0},
        { 7,12,5,0,0},
        { 8,13,6,0,0},
        { 1,13,7,0,0},
        { 1,2,8,14,0},
        { 1,2,3,9,11},
        { 10,12,14,4,0},
        { 13,14,11,6,0},
        { 9,14,12,8,7},
        { 9,10,11,12,13}
    };




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void RandSelect()
    {
        ster[0] = Random.Range(1,15);

        sterPos[ster[0]-1].GetComponent<Image>().enabled = true;

        int[] badNum = { ster[0], 0,0 };
        for (int j = 0; j < lineCode.Length; j++)
        {
            int num = 0;
            for (int i = 0; i < 5 ; i++)
            {
                if (LincSter[ster[j]-1, i] != 0)
                {
                    num++;
                }
            }
            do {
                ster[j + 1] = LincSter[ster[j]-1, Random.Range(1, num)];
            } while (badNum[0]== ster[j + 1]|| badNum[1] == ster[j + 1]|| badNum[2] == ster[j + 1]);

            badNum[j + 1] = ster[j + 1];
            sterPos[ster[j + 1] - 1].GetComponent<Image>().enabled = true;
        }













        for (int i = 0; i < lineCode.Length; i++)
        {
            int num = 0;
            for (int a = 1; a <= 14; a++)
            {
                for (int b = a + 1; b <= 14; b++)
                {
                    num++;
                    if ((ster[i] == a&&ster[i+1]==b)|| (ster[i] == b && ster[i + 1] == a))
                    {


                        lineCode[i] = num;

                    }

                }
            }

            GameObject obj = (GameObject)Instantiate(lineObj, transform.position, Quaternion.identity, lineParent.transform);
            obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
            UILineRenderer data2 = obj.GetComponent<UILineRenderer>();
            data2.points[0] = sterPos[ster[i]-1].GetComponent<RectTransform>().anchoredPosition;
            data2.points[1] = sterPos[ster[i + 1]-1].GetComponent<RectTransform>().anchoredPosition;
        }













    }



        public void BoardReset()
    {

        for (int i = 0; i < sterPos.Length; i++)
        {

            sterPos[i].GetComponent<Image>().enabled = false;

        }
        foreach (Transform n in lineParent.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
    }


}
