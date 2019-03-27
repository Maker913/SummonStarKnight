using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour
{

    [SerializeField]
    private GameObject controller;
    [SerializeField]
    private int controllerRadius;


    [SerializeField]
    private GameObject atkObj;
    [SerializeField]
    private int atkRadius;



    private Vector2 fastControllerPosition;
    public bool moveFlg=false;
    private Vector2 moveStartPosition;
    [SerializeField]
    private int limit;
    private int touchController = 0;

    private Vector2 angleFastPos;
    private int angleController = 0;


    public float ControllerMoveX;
    public float ControllerMoveY;
    public bool Atktouch;
    public bool Summontouch;
    public bool Menutouch;
    public float angle;
    public float cameraangle;





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

                //float dis = Vector2.Distance(new Vector2(touch.position.x, touch.position.y), controller.transform.position);
                if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), controller.transform.position) < controllerRadius&&moveFlg ==false &&touchController==0)
                {
                    moveStartPosition = new Vector2(touch.position.x, touch.position.y);
                    moveFlg = true;
                    touchController = 1;
                }
                else if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), atkObj.transform.position) < controllerRadius && Atktouch == false)
                {
                    Atktouch = true;
                }
                else
                {
                    angleFastPos = new Vector2(touch.position.x, touch.position.y);
                    angleController = 1;
                }
                
            }
            else if (touch.phase == TouchPhase.Moved&&moveFlg&&touchController==1)
            {
                // タッチ移動
                float dis = Vector2.Distance(new Vector2(touch.position.x, touch.position.y) - moveStartPosition + fastControllerPosition, fastControllerPosition);
                angle = Mathf.Atan2((touch.position.y - moveStartPosition.y + fastControllerPosition.y) - fastControllerPosition.y, (touch.position.x - moveStartPosition.x + fastControllerPosition.x) - fastControllerPosition.x);
                if (angle < 0)
                {
                    angle = angle + 2 * Mathf.PI;
                }


                if (dis < limit)
                {
                    controller.transform.position = new Vector2(touch.position.x, touch.position.y)- moveStartPosition+fastControllerPosition;
                }
                else
                {
                    controller.transform.position = new Vector2(fastControllerPosition.x+(Mathf.Cos(angle)*limit), fastControllerPosition.y+ (Mathf.Sin(angle) * limit));

                    
                }

            }
            else if(touch.phase == TouchPhase.Moved &&angleController == 1)
            {
                cameraangle = angleFastPos.x - touch.position.x;
                angleFastPos.x = touch.position.x;


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


                if (angleController == 1)
                {
                    angleController = 0;
                }
                if (angleController == 2)
                {
                    angleController = 1;
                }

                if (Atktouch)
                {
                    Atktouch = false;
                }


            }


            ////////////////////////////////////////////
            if (Input.touchCount > 1)
            {
                touch = Input.GetTouch(1);


                if (touch.phase == TouchPhase.Began)
                {
                    // タッチ開始
                    if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), controller.transform.position) < controllerRadius && moveFlg == false && touchController == 0)
                    {
                        moveStartPosition = new Vector2(touch.position.x, touch.position.y);
                        moveFlg = true;
                        touchController = 2;
                    }
                    else if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), atkObj.transform.position) < controllerRadius && Atktouch == false)
                    {
                        Atktouch = true;
                    }
                    else
                    {
                        angleFastPos = new Vector2(touch.position.x, touch.position.y);
                        angleController = 2;
                    }

                }
                else if (touch.phase == TouchPhase.Moved && moveFlg && touchController == 2)
                {
                    // タッチ移動
                    float dis = Vector2.Distance(new Vector2(touch.position.x, touch.position.y) - moveStartPosition + fastControllerPosition, fastControllerPosition);
                    angle = Mathf.Atan2((touch.position.y - moveStartPosition.y + fastControllerPosition.y) - fastControllerPosition.y, (touch.position.x - moveStartPosition.x + fastControllerPosition.x) - fastControllerPosition.x);
                    if (angle < 0)
                    {
                        angle = angle + 2 * Mathf.PI;
                    }
                    if (dis < limit)
                    {
                        controller.transform.position = new Vector2(touch.position.x, touch.position.y) - moveStartPosition + fastControllerPosition;
                    }
                    else
                    {

                        controller.transform.position = new Vector2(fastControllerPosition.x + (Mathf.Cos(angle) * limit), fastControllerPosition.y + (Mathf.Sin(angle) * limit));


                    }

                }
                else if (touch.phase == TouchPhase.Moved && angleController == 2)
                {

                    cameraangle = angleFastPos.x - touch.position.x;
                    angleFastPos.x = touch.position.x;


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



                    if (angleController == 2)
                    {
                        angleController = 0;
                    }

                    if (Atktouch)
                    {
                        Atktouch = false;
                    }

                }
            }
        }

        if (angleController == 0)
        {
            cameraangle /= 2;
        }
        if(Mathf.Abs(cameraangle) <= 1)
        {
            cameraangle = 0;
        } 

        ControllerMoveY= -1 * (fastControllerPosition.x- controller.transform.position.x)/limit;
        ControllerMoveX= (fastControllerPosition.y - controller.transform.position.y) / limit;

        //Debug.Log("Atktouch = "+ Atktouch +",X = "+ ControllerMoveX + ",Y = "+ ControllerMoveY + ",angle = " +angle*180/Mathf.PI );
        Debug.Log("Atk=" + Atktouch);
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
