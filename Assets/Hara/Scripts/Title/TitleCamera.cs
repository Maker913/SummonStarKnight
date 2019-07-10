using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{
    [SerializeField]
    private bool cameraRotateStop = true;    // カメラの回転を止めるフラグ
    public bool CameraRotateStop { set { cameraRotateStop = value; } get { return cameraRotateStop; } }

    private float time;
    private float angle = 36;    // 1秒間に回転する角度

    // Update is called once per frame
    void Update()
    {
        /*
        transform.Rotate(0, 0.25f, 0, Space.World);
        */

        // World空間のY軸を中心に回転
        if (!cameraRotateStop)
        {
            time += Time.deltaTime;
            transform.RotateAround(transform.position, Vector3.up, angle * Time.deltaTime);
        }
        
        // 1回転したら回転を止める
        if(time >= (360 / angle))
        {
            cameraRotateStop = true;
            time = 0;
            // カメラの向きを初期値に戻す
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
    }
}
