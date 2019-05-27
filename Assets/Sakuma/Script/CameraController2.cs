using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    //現在のカメラモード
    public int cameraMode=0;

    private int time = 0;

    private Vector3 nowPos;


    // FirstCamera用
    [Space(10)]
    [SerializeField]
    private GameObject firstPos;
    [SerializeField]
    private float rate;
    private float progressTime;
    private float progressTime2;

    private Vector3 firstAngle;
    private Vector3 nowAngle;



    void Start()
    {
        cameraMode = 0;
    }

    void Update()
    {
        switch (cameraMode)
        {
            case 0:
                FirstCamera();
                break;
            case 1:

                break;
            default:
                break;
        }






    }



    private void FirstCamera()
    {
        if (time == 0)
        {
            nowPos = transform.position;
            time = 1;
            progressTime = rate;
            progressTime2 = 0;
            nowAngle = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) ;
            firstAngle = new Vector3(firstPos.transform.localRotation.x, firstPos.transform.localRotation.y, firstPos.transform.localRotation.z);
            Debug.Log(firstAngle);
        }




        float anglex = Mathf.LerpAngle(nowAngle.x, firstPos.transform.eulerAngles.x, 1 - ((progressTime) * (progressTime)) / (rate * rate));
        float angley = Mathf.LerpAngle(nowAngle.y, firstPos.transform.eulerAngles.y, 1 - ((progressTime) * (progressTime)) / (rate * rate));
        float anglez = Mathf.LerpAngle(nowAngle.z, firstPos.transform.eulerAngles.z, 1 - ((progressTime) * (progressTime)) / (rate * rate));
        





        progressTime -= Time.deltaTime;
        progressTime2 += Time.deltaTime;
        if (progressTime2 < rate)
        {
            transform.position = Vector3.Lerp(nowPos, firstPos.transform.position, 1 - ((progressTime) * (progressTime)) / (rate * rate));
            transform.eulerAngles = new Vector3(anglex, angley, anglez);
        }








    }



    private void Change(int data)
    {
        time = 0;
        cameraMode = data;
    }








}
