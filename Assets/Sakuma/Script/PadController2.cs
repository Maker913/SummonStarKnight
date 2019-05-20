using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PadController2 : MonoBehaviour
{
    //タップの入力受付の有無
    public bool Pad;

    //召喚buttonで主に使用する奴
    public bool summon;
    [SerializeField]
    private GameObject summonButton;
    [SerializeField]
    private int summonButtonRadius;

    //召喚板奴
    [SerializeField]
    private GameObject boardObj;
    [SerializeField]
    private int boardRadius;

    [SerializeField]
    private GameObject[] SterPos;
    [SerializeField]
    private int SterRadius;
    [SerializeField]
    private int chainRadius;

    private int SterController = 0;
    private int catchster;
    private int catchster2=0;
    private bool angleController = false;
    private float angle;

    public int[] SterLine = new int[91];
    private int sterLineamount = 0;
    private bool moveFlg=false;

    private bool[] glowStar = new bool[14];
    private Image[]glowSterImage=new Image[14];

    [SerializeField]
    private GameObject[] SterEf;
    private Animator[] SterEfAnime = new Animator[14];


    [SerializeField]
    private GameObject sterLineObj;
    private UILineRenderer sterUILine;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject linePr;
    [SerializeField]
    private GameObject lineParent;

    //
    [SerializeField]
    private GameObject textPadObj;

    //GameControllerアクセス用
    [SerializeField]
    private GameObject gameControllerObj;
    private GameController gameController;

    [SerializeField]
    private GameObject enjObj;
    private Enj enj;


    private bool move=false ;

    // Start is called before the first frame update
    void Start()
    {
        Pad = false;
        gameController = gameControllerObj.GetComponent<GameController>();
        sterUILine = sterLineObj.GetComponent<UILineRenderer>();
        for (int i = 0; i < glowSterImage.Length; i++)
        {
            glowSterImage[i] = SterPos[i].GetComponent<Image>();
            SterEfAnime[i] = SterEf[i].GetComponent<Animator>();
        }
        enj = enjObj.GetComponent<Enj>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(SterLine[0] + " " + SterLine[1] + " " + SterLine[2] + " " + SterLine[3] + " " + SterLine[4] + " " + SterLine[5] + " " + SterLine[6]);


        if (Input.touchCount > 0 && Pad)
        {


            //一つ目のタップ処理
            Touch touch = Input.GetTouch(0);


            //開始時
            if (touch.phase == TouchPhase.Began)
            {
                //召喚buttonの処理
                if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), summonButton.transform.position) < summonButtonRadius)
                {
                    summon = true;
                    Summon();
                }

                int radius = boardRadius;

                if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), new Vector2(boardObj.transform.position.x, boardObj.transform.position.y)) < radius+50)
                {
                    for (int i = 0; i < SterPos.Length; i++)
                    {
                        if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), SterPos[i].transform.position) < SterRadius)
                        {
                            SterController = 1;
                            catchster = i + 1;
                            if(glowStar[catchster - 1] == true) {
                                move = true;
                            } else {
                                glowStar[catchster - 1] = true;
                                move = false;
                            }
                            SterEfAnime[catchster - 1].SetBool("Change", true);
                            radius = (int)Vector2.Distance(new Vector2(touch.position.x, touch.position.y), SterPos[i].transform.position);
                        }
                    }
                }
                else
                {
                    angle = Mathf.Atan2(touch.position.y - boardObj.transform.position.y, touch.position.x - boardObj.transform.position.x);
                        if (angle < 0) {
                            angle = angle + 2 * Mathf.PI;
                        }
                    angle = angle * 180 / Mathf.PI;
                    angleController = true;
                }




            }
            //移動時
            else if (touch.phase == TouchPhase.Moved)
            {

                if (SterController == 1)
                {
                    moveFlg = false;
                    int radius = chainRadius;
                    for (int i = 0; i < SterPos.Length; i++)
                    {
                        if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), SterPos[i].transform.position) < radius)
                        {
                            if (catchster == i + 1)
                            {
                                break;
                            }

                            catchster2 = i + 1;
                            radius = (int)Vector2.Distance(new Vector2(touch.position.x, touch.position.y), SterPos[i].transform.position);
                            moveFlg = true;
                        }
                    }
                    if (moveFlg)
                    {
                        move = true;
                        glowStar[catchster2 - 1] = true;
                        glowStar[catchster - 1] = true;
                        SterEfAnime[catchster2 - 1].SetBool("Change", true);
                        SterEfAnime[catchster - 1].SetBool("Change", false);
                        int num = 0;
                        for (int a = 1; a <= SterPos.Length ; a++)
                        {
                            for (int b = a + 1; b <= SterPos.Length; b++)
                            {
                                num++;
                                if ((a == catchster && b == catchster2) || (b == catchster && a == catchster2))
                                {
                                    if (sterLineamount == 0)
                                    {
                                        SterLine[sterLineamount] = num;
                                        sterLineamount++;
                                        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
                                        GameObject obj = (GameObject)Instantiate(linePr, transform.position, Quaternion.identity, lineParent.transform);
                                        UILineRenderer data2 = obj.GetComponent<UILineRenderer>();
                                        data2.points[0] = new Vector2((SterPos[catchster - 1].transform.position.x - Screen.width / 2) / Screen.width * CanvasRect.sizeDelta.x, (SterPos[catchster - 1].transform.position.y - Screen.height / 2) / Screen.height * CanvasRect.sizeDelta.y);
                                        data2.points[1] = new Vector2((SterPos[catchster2 - 1].transform.position.x - Screen.width / 2) / Screen.width * CanvasRect.sizeDelta.x, (SterPos[catchster2 - 1].transform.position.y - Screen.height / 2) / Screen.height * CanvasRect.sizeDelta.y);
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

                                            RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
                                            GameObject obj = (GameObject)Instantiate(linePr, transform.position, Quaternion.identity, lineParent.transform);
                                            UILineRenderer data2 = obj.GetComponent<UILineRenderer>();
                                            data2.points[0] = new Vector2((SterPos[catchster - 1].transform.position.x - Screen.width / 2) / Screen.width * CanvasRect.sizeDelta.x, (SterPos[catchster - 1].transform.position.y - Screen.height / 2) / Screen.height * CanvasRect.sizeDelta.y);
                                            data2.points[1] = new Vector2((SterPos[catchster2 - 1].transform.position.x - Screen.width / 2) / Screen.width * CanvasRect.sizeDelta.x, (SterPos[catchster2 - 1].transform.position.y - Screen.height / 2) / Screen.height * CanvasRect.sizeDelta.y);
                                            //obj.transform.parent = lineParent.transform;

                                        }
                                    }
                                }
                            }
                        }






                        catchster = catchster2;
                    }
                    if (SterController != 0)
                    {
                        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
                        sterUILine.points[1] = new Vector2((touch.position.x - Screen.width / 2) / Screen.width * CanvasRect.sizeDelta.x, (touch.position.y - Screen.height / 2) / Screen.height * CanvasRect.sizeDelta.y);
                        sterUILine.points[0] = new Vector2((SterPos[catchster - 1].transform.position.x - Screen.width / 2) / Screen.width * CanvasRect.sizeDelta.x, (SterPos[catchster - 1].transform.position.y - Screen.height / 2) / Screen.height * CanvasRect.sizeDelta.y);


                    }

                }
                if (angleController)
                {
                    float angle2 = Mathf.Atan2(touch.position.y - boardObj.transform.position.y, touch.position.x - boardObj.transform.position.x);
                    if (angle2 < 0)
                    {
                        angle2 = angle2 + 2 * Mathf.PI;
                    }
                    angle2 = angle2 * 180 / Mathf.PI;


                    boardObj.transform.eulerAngles +=new Vector3(0,0,((angle)-(angle2))*-1);
                    angle = angle2;
                }




            }
            //終了時
            else if (touch.phase == TouchPhase.Ended)
            {
                if (summon)
                {
                    summon = false;
                }
                if (SterController == 1)
                {
                    SterController = 0;
                    sterUILine.points[1] = new Vector2(0, 0);
                    sterUILine.points[0] = new Vector2(0, 0);
                    if (move == false)
                    {
                        glowStar[catchster - 1] = false;
                    }
                }
                if (angleController)
                {
                    angleController = false;
                }

                for (int i = 0; i < glowSterImage.Length; i++)
                {
                    SterEfAnime[i].SetBool("Change", false);
                }
            }


            for (int i = 0; i < glowSterImage.Length; i++)
            {
                if (glowStar[i])
                {
                    glowSterImage[i].enabled = true;
                }
                else
                {
                    glowSterImage[i].enabled = false;
                }
               
            }




#if false
            //デュアルタップのタップ処理
            if (Input.touchCount > 1)
            {
                touch = Input.GetTouch(1);


                //開始時
                if (touch.phase == TouchPhase.Began)
                {
                    //召喚buttonの処理
                    if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), summonButton.transform.position) < summonButtonRadius && summonButtonPas == 0)
                    {
                        summon = true;
                        summonButtonPas = 2;
                    }



                }
                //移動時
                else if (touch.phase == TouchPhase.Moved)
                {
                }
                //終了時
                else if (touch.phase == TouchPhase.Ended)
                {

                    if (summonButtonPas == 2)
                    {
                        summonButtonPas = 0;
                        summon = false; ;
                    }

                }
            }
#endif


        }
    }



    private void Summon()
    {
#if false
        int weapon = -1;

        Array.Sort(SterLine);
        Array.Reverse(SterLine);
        for (int i=0; i< gameController.technique.Length; i++)
        {
            int[] bfList = gameController.technique[i].Code;
            Array.Resize(ref bfList, bfList.Length+1);

            int check = 0;
            for(int j=0;j<bfList.Length;j++)
            {
                if(bfList[j] == SterLine[j])
                {
                    check++;
                }
            }
            if (check == bfList.Length)
            {
                weapon = i;
                break;
            }

        }






        if (weapon != -1)
        {
            textPadObj.SetActive(true);
            gameController.weapon = weapon;
            Pad = false;
            gameController.ModeChange(3, 0f);
            Debug.Log(gameController.technique[weapon].Name);
        }
        else
        {
            Debug.Log("形まちがっとるで");
        }
#endif
        
        Array.Sort(SterLine);
        Array.Reverse(SterLine);

        int[] bfList = gameController.nomalAttack[enj.summonNum].Code;
        Array.Resize(ref bfList, bfList.Length + 1);

        int check = 0;
        for (int j = 0; j < bfList.Length ; j++)
        {
            if (bfList[j] == SterLine[j])
            {
                check++;
            }
        }
        if (check == bfList.Length)
        {

            enj.BoardReset();
            enj.RandSelect();
        }

        BoardReset();
    }



    private void BoardReset()
    {

        sterLineamount = 0;
        SterLine = new int[91];
        for (int i = 0; i < glowSterImage.Length; i++)
        {

            glowStar[i] = false;

        }
        foreach (Transform n in lineParent.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
    }







}
