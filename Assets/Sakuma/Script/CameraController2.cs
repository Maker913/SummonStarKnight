using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    //現在のカメラモード
    public int cameraMode=0;

    private int time = 0;

    private Vector3 nowPos;




    [System.Serializable]
    public struct CameraPos
    {
        public GameObject[] Pos;
        public GameObject stratPos;
    }

    public CameraPos[] posData;





    private GameObject firstPos;
    [SerializeField]
    private float rate;
    private float progressTime;
    private float progressTime2;

    private Vector3 firstAngle;
    private Vector3 nowAngle;

    [SerializeField]
    private Vector3[] shootingPos;
    [SerializeField ]
    private Vector3[] shootingRot;

    void Start()
    {
        if (StageCobtroller.Shooting == false)
        {
            transform.position = posData[StageCobtroller .stageNum -1].stratPos.transform.position;
            transform.localEulerAngles = posData[StageCobtroller.stageNum - 1].stratPos.transform.localEulerAngles;
            SetCamera(-1, 0);
            cameraMode = 0;
        }
        else
        {
            transform.position = shootingPos [StageCobtroller .stageNum -1];
            transform.localEulerAngles = shootingRot[StageCobtroller.stageNum - 1];
            cameraMode = 1;
        }
        
    }

    private void FixedUpdate()
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

        float anglex = Mathf.LerpAngle(nowAngle.x, firstPos.transform.eulerAngles.x, 1f - ((progressTime) * (progressTime)) / (rate * rate));
        float angley = Mathf.LerpAngle(nowAngle.y, firstPos.transform.eulerAngles.y, 1f - ((progressTime) * (progressTime)) / (rate * rate));
        float anglez = Mathf.LerpAngle(nowAngle.z, firstPos.transform.eulerAngles.z, 1f - ((progressTime) * (progressTime)) / (rate * rate));
        





        progressTime -= Time.deltaTime;
        progressTime2 += Time.deltaTime;
        if (progressTime2 < rate)
        {
            transform.position = Vector3.Lerp(nowPos, firstPos.transform.position, 1f - ((progressTime) * (progressTime)) / (rate * rate));
            transform.eulerAngles = new Vector3(anglex, angley, anglez);
        }








    }

    public void SetCamera(int num, float data)
    {
        if (num == -1)
        {
            firstPos = posData[StageCobtroller.stageNum - 1].stratPos;
        }
        else
        {
            firstPos = posData[StageCobtroller.stageNum - 1].Pos[num];
        }
        rate = data;
        nowPos = transform.position;
        time = 1;
        progressTime = rate;
        progressTime2 = 0;
        nowAngle = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        firstAngle = new Vector3(firstPos.transform.localRotation.x, firstPos.transform.localRotation.y, firstPos.transform.localRotation.z);
        //Debug.Log(firstAngle);
    }

    private void Change(int data)
    {
        time = 0;
        cameraMode = data;
    }








}
