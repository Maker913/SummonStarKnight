using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    //現在のカメラモード
    public int cameraMode=0;


    //座標取得のアレ
    [SerializeField]
    private GameObject pleyerObj;
    [SerializeField]
    private GameObject enemyObj;
    private Vector3 midpoint;


    private int time = 0;

    public float angle = 0;
    private float distance;
    [SerializeField]
    private float distanceLete;
    [SerializeField]
    private float height;
    [SerializeField]
    private float PadDown;

    void Start()
    {
        
    }

    void Update()
    {
        switch (cameraMode)
        {
            case 0:
                Rot();
                break;
            default:
                break;
        }






    }




    private void Rot()
    {
        if (time == 0)
        {
            midpoint = (pleyerObj.transform.position +enemyObj.transform.position)/2;
            time++;
        }

        distance = Vector3.Distance(pleyerObj.transform.position, enemyObj.transform.position)*distanceLete;
        angle += 0.1f;

        transform.position = new Vector3(
        Mathf.Cos(angle / 180 * Mathf.PI) * distance + midpoint.x,
        midpoint.y + height + PadDown,
        Mathf.Sin(angle / 180 * Mathf.PI) * distance + midpoint.z);

        this.transform.rotation = Quaternion.Euler(Mathf.Atan(height / distance) * 180 / Mathf.PI, -1 * (angle + 90), transform.rotation.z);


    }
}
