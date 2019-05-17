using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PadController : MonoBehaviour
{

    [SerializeField]
    private GameObject controller;
    [SerializeField]
    private int controllerRadius;
    [SerializeField]
    private int limit;

    [SerializeField]
    private GameObject atkObj;
    [SerializeField]
    private int atkRadius;

    [SerializeField]
    private GameObject menuObj;
    [SerializeField]
    private int menuRadius;

    [SerializeField]
    private GameObject boardObj;
    [SerializeField]
    private int boardRadius;
    [SerializeField]
    private int OnboardRadius;



    private Vector2 fastControllerPosition;
    public bool moveFlg=false;
    private Vector2 moveStartPosition;
    
    private int touchController = 0;

    private Vector2 angleFastPos;
    private int angleController = 0;
    private int atkController = 0;

    public float ControllerMoveX;
    public float ControllerMoveY;
    public bool Atktouch;
    public bool Summontouch;
    public float angle;
    public float cameraangle;
    public bool anPad;

    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private GameObject MenuUI;

    [SerializeField]
    private GameObject restate;
    private Animator restateanime;

    public bool gameStop;

    public bool boardcast;


    [SerializeField]
    private GameObject[] SterPos;
    [SerializeField]
    private int SterRadius;
    private int SterController=0;
    private int catchster;
    private int catchster2;

    public int[] SterLine = new int[36];
    private int sterLineamount=0;


    [SerializeField]
    private GameObject playerManagerObj;
    private PlayerManager playerManager;

    [SerializeField]
    private GameObject sterLineObj;
    private UILineRenderer sterUILine;

    private bool[] glowStar = new bool[14];

    void Start()
    {
        //Debug.Log(controller.transform.position);
        fastControllerPosition = controller.transform.position;
        ControllerMoveX=0;
        ControllerMoveY=0;
        Atktouch=false ;
        Summontouch=false ;
        gameStop = false ;
        anPad = true;
        boardcast = false;
        restateanime = restate.GetComponent<Animator>();
        playerManager = playerManagerObj.GetComponent<PlayerManager>();
        sterUILine = sterLineObj.GetComponent<UILineRenderer>();
    }

    void Update()
    {
        //画面に複数以上タッチされているときの処理
        if (Input.touchCount > 0&&anPad)
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
                else if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), atkObj.transform.position) <atkRadius && Atktouch == false&&atkController ==0)
                {
                    Atktouch = true;
                    atkController = 1;
                }
                else if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), boardObj.transform.position) < boardRadius&&boardcast ==false )
                {
                        boardcast = true;
                        
                }
                else if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), menuObj.transform.position) < menuRadius)
                {

                    PadSwitch();

                    gameObject.SetActive(false);
                    UI.gameObject.SetActive(false);
                    MenuUI.gameObject.SetActive(true);
                    restateanime.SetBool("test", true);
                }
                else if (boardcast && Vector2.Distance(new Vector2(touch.position.x, touch.position.y), new Vector2(boardObj.transform.position.x, boardObj.transform.position.y)) < OnboardRadius)
                {
                    for (int i = 0; i < SterPos.Length; i++)
                    {
                        if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), SterPos[i].transform.position) < SterRadius)
                        {
                            SterController = 1;
                            catchster = i + 1;
                            break;
                        }
                    }
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
            else if (touch.phase == TouchPhase.Moved && SterController == 1)
            {
                for (int i = 0; i < SterPos.Length; i++)
                {
                    if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), SterPos[i].transform.position) < SterRadius)
                    {
                        if (catchster == i + 1)
                        {
                            break;
                        }

                        catchster2= i + 1;

                        int num = 0;
                        for(int a = 1; a <= 9; a++)
                        {
                            for(int b = a+1; b <= 9; b++)
                            {
                                num++;
                                if((a==catchster &&b==catchster2 )||(b == catchster && a == catchster2))
                                {
                                    if (sterLineamount == 0)
                                    {
                                        SterLine[sterLineamount] = num;
                                        sterLineamount++;
                                    }
                                    else
                                    {
                                        int data = 0;
                                        for (int c = 0; c < sterLineamount; c++)
                                        {
                                            if (SterLine[c] != num)
                                            {
                                                data++;


                                            }
                                        }
                                        if (data == sterLineamount)
                                        {
                                            SterLine[sterLineamount] = num;
                                            sterLineamount++;

                                        }
                                    }
                                }
                            }
                        }
                        catchster = catchster2;
                        break;
                    }
                }


                if (SterController != 0)
                {
                    sterUILine.points[1] = new Vector2(touch.position.x,touch.position.y);
                    sterUILine.points[0] = new Vector2(0,0);
                }




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

                if (Atktouch&&atkController==1)
                {
                    atkController = 0;
                    Atktouch = false;
                }
                if (Atktouch && atkController == 2)
                {
                    atkController = 1;
                }

                if (SterController == 1)
                {
                    SterController = 0;
                    catchster = 0;
                }
                if (SterController == 2)
                {
                    SterController = 1;
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
                    else if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), atkObj.transform.position) < atkRadius && Atktouch == false&&atkController==0)
                    {
                        Atktouch = true;
                        atkController = 2;
                    }
                    else if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), boardObj.transform.position) < boardRadius && boardcast == false)
                    {
                            boardcast = true;

                    }
                    else if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), menuObj.transform.position) < menuRadius)
                    {
                        PadSwitch();

                        gameObject.SetActive(false);
                        UI.gameObject.SetActive(false);
                        MenuUI.gameObject.SetActive(true);
                        restateanime.SetBool("test", true);
                    }
                    else if (boardcast&& Vector2.Distance(new Vector2(touch.position.x, touch.position.y), new Vector2(boardObj.transform.position.x, boardObj.transform.position.y)) < OnboardRadius)
                    {
                        for (int i = 0; i < SterPos.Length; i++)
                        {
                            if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), SterPos[i].transform.position) < SterRadius)
                            {
                                SterController = 2;
                                catchster = i + 1;
                                break;
                            }
                        }
                    }else                    {
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
                else if (touch.phase == TouchPhase.Moved && SterController == 2)
                {
                    for (int i = 0; i < SterPos.Length; i++)
                    {
                        if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), SterPos[i].transform.position) < SterRadius)
                        {
                            if (catchster == i + 1)
                            {
                                break;
                            }

                            catchster2 = i + 1;

                            int num = 0;
                            for (int a = 1; a <= 9; a++)
                            {
                                for (int b = a + 1; b <= 9; b++)
                                {
                                    num++;
                                    if ((a == catchster && b == catchster2) || (b == catchster && a == catchster2))
                                    {
                                        if (sterLineamount == 0)
                                        {
                                            SterLine[sterLineamount] = num;
                                            sterLineamount++;
                                        }
                                        else
                                        {
                                            int data = 0;
                                            for (int c = 0; c < sterLineamount; c++)
                                            {
                                                if (SterLine[c] != num)
                                                {
                                                    data++;


                                                }
                                            }
                                            if (data == sterLineamount)
                                            {
                                                SterLine[sterLineamount] = num;
                                                sterLineamount++;
                                            }
                                        }
                                    }
                                }
                            }
                            catchster = catchster2;
                            break;
                        }


                        if (SterController != 0)
                        {
                            sterUILine.points[0] = new Vector2(touch.position.x, touch.position.y);
                            sterUILine.points[1] = new Vector2(SterPos[catchster].transform.position.x, SterPos[catchster].transform.position.y);
                        }


                    }


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

                    if (SterController == 2)
                    {
                        SterController = 0;
                        catchster = 0;
                    }

                    if (Atktouch&&atkController ==2)
                    {
                        atkController = 0;
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
        //Debug.Log("Atk=" + Atktouch);
        //Debug.Log(SterLine[0]+" "+ SterLine[1] + " " + SterLine[2] + " " + SterLine[3] + " " + SterLine[4] + " " + SterLine[5] + " " + SterLine[6]);
    }


    public void UIChange()
    {
        Time.timeScale = 1;
        anPad = true;
        gameStop = false;
        restateanime.SetBool("test", false);
        gameObject.SetActive(true);
        UI.gameObject.SetActive(true);
        MenuUI.gameObject.SetActive(false);
    }




    public void PadSwitch()
    {
            anPad = false;

            gameStop = true;

        controller.transform.position = fastControllerPosition;

        moveFlg = false;
        Atktouch = false;

        SterController = 0;
        catchster = 0;
        sterLineamount = 0;
        for (int i = 0; i < SterLine.Length; i++)
        {
            SterLine[i] = 0;
        }

        touchController = 0;
        atkController = 0;
        angleController = 0;
    }


    public void Sumon()
    {
        Array.Sort(SterLine);
        Array.Reverse(SterLine);

        if (SterLine[0] == 34 && SterLine[1] == 31 && SterLine[2] == 28 && SterLine[3] == 27 && SterLine[4] == 20)
        {
            playerManager.ChangeSummonMode(2);
        }

        if (SterLine[0] == 34 && SterLine[1] == 31 && SterLine[2] == 28 && SterLine[3] == 20 && SterLine[4] == 14)
        {
            playerManager.ChangeSummonMode(3);
        }

        if (SterLine[0] == 34 && SterLine[1] == 31 && SterLine[2] == 30 && SterLine[3] == 21 && SterLine[4] == 14)
        {
            playerManager.ChangeSummonMode(4);
        }



        sterLineamount = 0;
        for(int i = 0; i < SterLine.Length; i++)
        {
            SterLine[i] = 0;
        }
    }

}
