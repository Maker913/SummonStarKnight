using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour
{

    [SerializeField]
    private GameObject controller;
    [SerializeField]
    private int controllerRadius;

    private Vector2 fastControllerPosition;
    private bool moveFlg=false;
    private Vector2 moveStartPosition;
    [SerializeField]
    private int limit;





    void Start()
    {
        Debug.Log(controller.transform.position);
        fastControllerPosition = controller.transform.position;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // タッチ開始

                float dis = Vector2.Distance(new Vector2(touch.position.x, touch.position.y), controller.transform.position);
                if (dis < controllerRadius)
                {
                    Debug.Log("controllerタップしたで");
                    moveStartPosition = controller.transform.position;
                    moveFlg = true;
                }
                else
                {
                    Debug.Log(touch.position);
                }
                
            }
            else if (touch.phase == TouchPhase.Moved&&moveFlg)
            {
                // タッチ移動
                float dis = Vector2.Distance(new Vector2(touch.position.x, touch.position.y),moveStartPosition);
                if (dis < limit)
                {
                    controller.transform.position = new Vector2(touch.position.x, touch.position.y);
                }
                else
                {
                    float angle = Mathf.Atan2(touch.position.y - fastControllerPosition.y , touch.position.x - fastControllerPosition.x);
                    if (angle < 0)
                    {
                        angle = angle + 2 * Mathf.PI;
                    }
                    controller.transform.position = new Vector2(fastControllerPosition.x+(Mathf.Cos(angle)*limit), fastControllerPosition.y+ (Mathf.Sin(angle) * limit));

                    
                }

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // タッチ終了
                controller.transform.position = fastControllerPosition;
                moveFlg = false;
            }
        }
    }
}
