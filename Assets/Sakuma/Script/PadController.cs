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
    private int touchController = 0;

    public float ControllerMoveX;
    public float ControllerMoveY;
    public bool Atktouch;
    public bool Summontouch;
    public bool Menutouch;


    void Start()
    {
        //Debug.Log(controller.transform.position);
        fastControllerPosition = controller.transform.position;
        ControllerMoveX=0;
        ControllerMoveY=0;
        Atktouch=false ;
        Summontouch=false ;
        Menutouch=false ;
    }

    void Update()
    {
        //画面に複数以上タッチされているときの処理
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            

            if (touch.phase == TouchPhase.Began)
            {
                // タッチ開始

                float dis = Vector2.Distance(new Vector2(touch.position.x, touch.position.y), controller.transform.position);
                if (dis < controllerRadius&&moveFlg ==false &&touchController==0)
                {
                    Debug.Log("controllerタップしたで");
                    moveStartPosition = new Vector2(touch.position.x, touch.position.y);
                    moveFlg = true;
                    touchController = 1;
                }
                else
                {
                }
                
            }
            else if (touch.phase == TouchPhase.Moved&&moveFlg&&touchController==1)
            {
                // タッチ移動
                float dis = Vector2.Distance(new Vector2(touch.position.x, touch.position.y) - moveStartPosition + fastControllerPosition, fastControllerPosition);
                if (dis < limit)
                {
                    controller.transform.position = new Vector2(touch.position.x, touch.position.y)- moveStartPosition+fastControllerPosition;
                }
                else
                {
                    float angle = Mathf.Atan2((touch.position.y-moveStartPosition.y+fastControllerPosition.y) - fastControllerPosition.y , (touch.position.x - moveStartPosition.x + fastControllerPosition.x) - fastControllerPosition.x);
                    if (angle < 0)
                    {
                        angle = angle + 2 * Mathf.PI;
                    }
                    controller.transform.position = new Vector2(fastControllerPosition.x+(Mathf.Cos(angle)*limit), fastControllerPosition.y+ (Mathf.Sin(angle) * limit));

                    
                }

            }
            else if(touch.phase == TouchPhase.Moved &&touchController != 1)
            {
                Debug.Log("チャージ攻撃");



            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // タッチ終了
                
                if (touchController == 1)
                {
                    controller.transform.position = fastControllerPosition;
                    moveFlg = false;
                    touchController = 0;
                }
                if(touchController == 2)
                {
                    touchController = 1;
                }
            }


            ////////////////////////////////////////////
            if (Input.touchCount > 1)
            {
                touch = Input.GetTouch(1);


                if (touch.phase == TouchPhase.Began)
                {
                    // タッチ開始

                    float dis = Vector2.Distance(new Vector2(touch.position.x, touch.position.y), controller.transform.position);
                    if (dis < controllerRadius && moveFlg == false && touchController == 0)
                    {
                        Debug.Log("controllerタップしたで");
                        moveStartPosition = new Vector2(touch.position.x, touch.position.y);
                        moveFlg = true;
                        touchController = 2;
                    }
                    else
                    {

                    }

                }
                else if (touch.phase == TouchPhase.Moved && moveFlg && touchController == 2)
                {
                    // タッチ移動
                    float dis = Vector2.Distance(new Vector2(touch.position.x, touch.position.y) - moveStartPosition + fastControllerPosition, fastControllerPosition);
                    if (dis < limit)
                    {
                        controller.transform.position = new Vector2(touch.position.x, touch.position.y) - moveStartPosition + fastControllerPosition;
                    }
                    else
                    {
                        float angle = Mathf.Atan2((touch.position.y - moveStartPosition.y + fastControllerPosition.y) - fastControllerPosition.y, (touch.position.x - moveStartPosition.x + fastControllerPosition.x) - fastControllerPosition.x);
                        if (angle < 0)
                        {
                            angle = angle + 2 * Mathf.PI;
                        }
                        controller.transform.position = new Vector2(fastControllerPosition.x + (Mathf.Cos(angle) * limit), fastControllerPosition.y + (Mathf.Sin(angle) * limit));


                    }

                }
                else if (touch.phase == TouchPhase.Moved && touchController != 2)
                {




                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    // タッチ終了

                    if (touchController == 2)
                    {
                        controller.transform.position = fastControllerPosition;
                        moveFlg = false;
                        touchController = 0;
                    }
                }
            }
        }


        ControllerMoveX= (fastControllerPosition.x- controller.transform.position.x)/limit;
        ControllerMoveY= (fastControllerPosition.y - controller.transform.position.y) / limit;

        Debug.Log("Atktouch = "+ Atktouch +",X = "+ ControllerMoveX + ",Y = "+ ControllerMoveY);

    }


    public void PushDown()
    {
        Atktouch = true;
    }

    public void PushUp()
    {
        Atktouch = false;
    }
}
