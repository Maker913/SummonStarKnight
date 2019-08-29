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
    private int catchster2 = 0;
    private bool angleController = false;
    private float angle;

    public int[] SterLine = new int[91];
    [SerializeField]
    private int sterLineamount = 0;
    private bool moveFlg = false;

    private bool[] glowStar = new bool[11];
    private Image[] glowSterImage = new Image[11];

    [SerializeField]
    private GameObject[] SterEf;
    private Animator[] SterEfAnime = new Animator[11];


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

    [SerializeField]
    private GameObject underPos;

    private bool move = false;


    public bool sumonMode = false;
    public bool sumonbd = false;
    public int sumonNum;

    [SerializeField]
    private GameObject sumonbdobj;

    [SerializeField]
    private GameObject lineParent2;
    [SerializeField]
    private GameObject sumontext;
    private Text text;

    [SerializeField]
    private GameObject BLine;

    [SerializeField]
    private GameObject StatusManagerObj;
    private StatusManager statusManager;


    [SerializeField]
    private GameObject ShootingObj;
    private ShootingEnj shooting;


    [SerializeField]
    private GameObject newsummon;
    private Animator newsummonanime;

    [SerializeField]
    private GameObject nameobj;

    [SerializeField]
    private GameObject ster;

    public bool summonRem = false;
    public float summonDelay = 0;

    private float summonRemTime=0;

    [SerializeField]
    private float BLineTime;

    private int deleteSterNum=-1;
    [SerializeField]
    private GameObject falseLineP;

    public bool oneLine = true;
    public bool oneLineCheck = true;

    [SerializeField]
    private GameObject oneLineText;
    [SerializeField]
    private GameObject textPos;

    [SerializeField]
    private GameObject oboeroPr;
    [SerializeField]
    private GameObject nazorePr;
    [SerializeField]
    private GameObject nazoP;

    // Start is called before the first frame update
    void Start()
    {
        newsummonanime = newsummon.GetComponent<Animator>();
        statusManager = StatusManagerObj.GetComponent<StatusManager>();
        text = sumontext.GetComponent<Text>();
        transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Pad = false;
        gameController = gameControllerObj.GetComponent<GameController>();
        sterUILine = sterLineObj.GetComponent<UILineRenderer>();
        for (int i = 0; i < glowSterImage.Length; i++)
        {
            glowSterImage[i] = SterPos[i].GetComponent<Image>();
            SterEfAnime[i] = SterEf[i].GetComponent<Animator>();
        }
        enj = enjObj.GetComponent<Enj>();
        shooting = ShootingObj.GetComponent<ShootingEnj>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(SterLine[0] + " " + SterLine[1] + " " + SterLine[2] + " " + SterLine[3] + " " + SterLine[4] + " " + SterLine[5] + " " + SterLine[6]);


        if (Input.touchCount > 0 && Pad && sumonbd == false&& summonRem==false )
        {


            //一つ目のタップ処理
            Touch touch = Input.GetTouch(0);


            //開始時
            if (touch.phase == TouchPhase.Began)
            {
                //召喚buttonの処理
                if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), summonButton.transform.position) < summonButtonRadius)
                {
                    if (sterLineamount == 0)
                    {
                        if (sumonMode == false && StageCobtroller.Shooting == false)
                        {
                            sumonbd = true;
                            sumonbdobj.transform.localPosition = new Vector3(275, 275, 0);
                            sumonbdobj.transform.localScale = new Vector3(1, 1, 1);
                            sumonbdobj.SetActive(true);
                            sumonbdobj.GetComponent<Animator>().SetTrigger("star");
                        }
                    }
                    else
                    {
                        summon = true;
                        Summon();
                    }
                }

                int radius = boardRadius;

                if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), new Vector2(boardObj.transform.position.x, boardObj.transform.position.y)) < radius + 50)
                {
                    for (int i = 0; i < SterPos.Length; i++)
                    {
                        if (Vector2.Distance(new Vector2(touch.position.x, touch.position.y), SterPos[i].transform.position) < SterRadius)
                        {
                            SterController = 1;
                            catchster = i + 1;
                            AudioManager.Instance.PlaySE(AudioManager.SeName.Follow);

                            oneLineCheck = true;

                            if (glowStar[catchster - 1] == true)
                            {
                                move = true;
                            }
                            else
                            {
                                move = false;
                                glowStar[catchster - 1] = true;
                                deleteSterNum = catchster - 1;
                            }


                            SterEfAnime[catchster - 1].SetBool("Change", true);
                            radius = (int)Vector2.Distance(new Vector2(touch.position.x, touch.position.y), SterPos[i].transform.position);




                            
                        }
                    }
                    //if (catchster != 0)
                    //{
                    //    glowStar[catchster - 1] = true;
                    //}
                }
                else
                {
                    angle = Mathf.Atan2(touch.position.y - boardObj.transform.position.y, touch.position.x - boardObj.transform.position.x);
                    if (angle < 0)
                    {
                        angle = angle + 2 * Mathf.PI;
                    }
                    angle = angle * 180 / Mathf.PI;
                    //angleController = true;
                }


                //if (underPos.transform.position .y > touch.position.y&&Vector2.Distance(new Vector2(touch.position.x, touch.position.y), new Vector2(boardObj.transform.position.x, boardObj.transform.position.y)) > boardRadius + 100 && Vector2.Distance(new Vector2(touch.position.x, touch.position.y), summonButton.transform.position) > summonButtonRadius)
                //{
                //    BoardReset();
                //    Debug.Log("うぇい");
                //}

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
                        deleteSterNum = -1;
                        move = true;
                        glowStar[catchster2 - 1] = true;
                        glowStar[catchster - 1] = true;


                        oneLineCheck = false;



                        int num = 0;
                        for (int a = 1; a <= SterPos.Length; a++)
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
                                        DeleteCheck(num,obj);
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
                                            DeleteCheck(num,obj);
                                        }
                                        else
                                        {
                                            oneLine = false;
                                        }
                                    }
                                }
                            }
                        }

                        if (!Summon())
                        {


                            SterEfAnime[catchster2 - 1].SetBool("Change", true);
                            SterEfAnime[catchster - 1].SetBool("Change", false);
                        }



                        //
                        catchster = catchster2;



                        if (StageCobtroller.Shooting)
                        {
                            ShootingChack();
                        }
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


                    boardObj.transform.eulerAngles += new Vector3(0, 0, ((angle) - (angle2)) * -1);
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
                    if(!oneLineCheck&&gameController.gameMode==2)
                    {
                        oneLine = false;
                    }



                    SterController = 0;
                    sterUILine.points[1] = new Vector2(0, 0);
                    sterUILine.points[0] = new Vector2(0, 0);
                    //if (move == false)
                    //{
                    //    glowStar[catchster - 1] = false;
                    //}

                    if(deleteSterNum != -1)
                    {
                        glowStar[deleteSterNum] = false;
                        deleteSterNum = -1;

                        //Debug.Log("   aa  ");
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





            if (sterLineamount != 0 && sumonMode == false)
            {
                //text.text  = "攻撃";
            }
            else
            {
                //text.text = "召喚";
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


        if (summonRem)
        {
            if(summonDelay < 0)
            {
                if (gameController.gameMode == 2)
                {
                    summonRemTime -= Time.deltaTime;
                }


                if(summonRemTime < 0)
                {
                    Instantiate(nazorePr, nazoP.transform.position , Quaternion.identity, nazoP.transform );
                    //Debug.Log("開始");
                    summonRem = false;
                    BlackLineDL();
                }


            }
            else
            {
                summonDelay -= Time.deltaTime;
                if(summonDelay < 0)
                {
                    Instantiate(oboeroPr, nazoP.transform.position, Quaternion.identity, nazoP.transform);
                    BlackLine();
                    summonRemTime = BLineTime;
                    //Debug.Log("カメラ移動完了");
                }
            }





        }







    }


    private void Success()
    {
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
        GameObject test= Instantiate(ster, ster.transform.position, Quaternion.identity,boardObj.transform );
        GameObject test2 = test.transform.GetChild(1).gameObject;

        for(int i=0;i<test2.transform.childCount; i++)
        {
            test2.transform.GetChild(i).GetChild(0).GetComponent<Image>().enabled = false;
        }

        test.GetComponent <Animator >().SetTrigger("Success");
        BoardReset();
    }


    private void DeleteCheck(int num,GameObject Obj)
    {
        int[] bfList;
        if (StageCobtroller.Shooting)
        {

            bfList = shooting.lineCode;

        }
        else
        {
            if (sumonMode == false)
            {
                bfList = gameController.nomalAttack[enj.summonNum].Code;
            }
            else
            {
                bfList = gameController.technique[sumonNum].Code;
            }
        }
        Array.Sort(bfList);
        Array.Reverse(bfList);
        Array.Resize(ref bfList, bfList.Length + 1);

        bool DCheck = true;

        for (int i = 0; i < bfList.Length; i++)
        {
            if (bfList[i] == num)
            {
                DCheck = false;
            }
        }

        if (DCheck)
        {
            GameObject obj1 = (GameObject)Instantiate(Obj, Obj.transform .position, Quaternion.identity, falseLineP.transform);
            obj1.GetComponent<LineUPdate>().falseLine = true;

            GameObject obj2 = (GameObject)Instantiate(lineParent, lineParent.transform.position, Quaternion.identity, falseLineP.transform);
            foreach (Transform n in obj2.transform)
            {
                n.gameObject.GetComponent<LineUPdate>().falseLine = true;
            }



            AudioManager.Instance.PlaySE(AudioManager.SeName .enemy_Deathblow );
            BoardReset();
            if (sumonMode)
            {
                gameController.ModeChange(23, 0);
                sumonMode = false;
            }
        }

    }

    private bool Summon()
    {
        bool end = false;
        if (gameController.gameMode == 2)
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
            if (sumonMode == false)
            {

                Array.Sort(SterLine);
                Array.Reverse(SterLine);

                int[] bfList = gameController.nomalAttack[enj.summonNum].Code;
                Array.Resize(ref bfList, bfList.Length + 1);

                int check = 0;
                for (int j = 0; j < bfList.Length; j++)
                {
                    if (bfList[j] == SterLine[j])
                    {
                        check++;
                    }
                }
                if (check == bfList.Length)
                {
                    if(oneLine||gameController .combo >0)
                    {
                        string popText = "";
                        if(gameController.combo > 0)
                        {
                            popText += (gameController.combo+1).ToString() + "回連続成功ボーナス\n";
                        }
                        if (oneLine)
                        {
                            popText += "一筆書きボーナス\n";
                        }

                        GameObject bf= Instantiate(oneLineText, textPos .transform.position, Quaternion.identity, boardObj.transform);
                        bf.transform.GetChild (1).GetComponent<Text>().text = popText;
                        
                    }




                    Success();
                    AudioManager.Instance.PlaySE(AudioManager.SeName.gauge);
                    gameController.ModeChange(3, 0);
                    enj.image.fillAmount = 0;
                    enj.time = statusManager.gageSpeed;
                    //enj.BoardReset();
                    //enj.RandSelect();
                    BlackLineDL();
                    nameobj.GetComponent<Animator>().SetBool("Name", false);
                    end = true;
                }
            }
            else
            {

                Array.Sort(SterLine);
                Array.Reverse(SterLine);

                int[] bfList = gameController.technique[sumonNum].Code;
                Array.Resize(ref bfList, bfList.Length + 1);

                int check = 0;
                for (int j = 0; j < bfList.Length; j++)
                {
                    if (bfList[j] == SterLine[j])
                    {
                        check++;
                    }
                }
                if (check == bfList.Length)
                {
                    Success();
                    AudioManager.Instance.PlaySE(AudioManager.SeName.gauge);
                    gameController.weapon = sumonNum;
                    gameController.ModeChange(8, 0);
                    enj.image.fillAmount = 0;
                    enj.time = statusManager.gageSpeed;
                    sumonMode = false;
                    BlackLineDL();
                    end = true;
                }
            }
            //BoardReset();
        }
        //Debug.Log(end);
        return end;

    }




    private void ShootingChack()
    {

        int[] sterLineBf = SterLine;

        Array.Sort(SterLine);
        Array.Reverse(SterLine);

        int[] bfList = shooting.lineCode;
        Array.Sort(bfList);
        Array.Reverse(bfList);
        Array.Resize(ref bfList, bfList.Length + 1);

        int check = 0;
        for (int j = 0; j < bfList.Length; j++)
        {
            if (bfList[j] == SterLine[j])
            {
                check++;
            }
        }
        if (check == bfList.Length)
        {
            AudioManager.Instance.PlaySE(AudioManager.SeName.gauge);
            shooting.lineNum++;
            shooting.BoardReset();
            shooting.RandSelect();
            Success();
        }
        else
        {
            SterLine = sterLineBf;
        }


    }


    public void BoardReset()
    {
        SterController = 0;
        sterUILine.points[1] = new Vector2(0, 0);
        sterUILine.points[0] = new Vector2(0, 0);
        sterLineamount = 0;
        SterLine = new int[91];
        for (int i = 0; i < glowSterImage.Length; i++)
        {

            glowStar[i] = false;

            glowSterImage[i].enabled = false;


        }
        foreach (Transform n in lineParent.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
    }



    public void BlackLine()
    {
        BlackLineDL();
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();


        for (int i = 0; i < gameController.technique[sumonNum].Code.Length; i++)
        {
            int num = 0;
            for (int a = 1; a <= 11; a++)
            {
                for (int b = a + 1; b <= 11; b++)
                {
                    num++;
                    if (gameController.technique[sumonNum].Code[i] == num)
                    {
                        //SterPos[a - 1].GetComponent<Image>().enabled = true;
                        //SterPos[b - 1].GetComponent<Image>().enabled = true;

                        GameObject obj = (GameObject)Instantiate(BLine, transform.position, Quaternion.identity, lineParent2.transform);
                        UILineRenderer data2 = obj.GetComponent<UILineRenderer>();
                        data2.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
                        data2.points[0] = new Vector2((SterPos[a - 1].transform.position.x - Screen.width / 2) / Screen.width * CanvasRect.sizeDelta.x, (SterPos[a - 1].transform.position.y - Screen.height / 2) / Screen.height * CanvasRect.sizeDelta.y);
                        data2.points[1] = new Vector2((SterPos[b - 1].transform.position.x - Screen.width / 2) / Screen.width * CanvasRect.sizeDelta.x, (SterPos[b - 1].transform.position.y - Screen.height / 2) / Screen.height * CanvasRect.sizeDelta.y);


                    }

                }
            }


        }
    }


    public void BlackLine2()
    {
#if false
        BlackLineDL();
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();


        for (int i = 0; i < gameController.nomalAttack[enj.summonNum].Code.Length; i++)
        {
            int num = 0;
            for (int a = 1; a <= 11; a++)
            {
                for (int b = a + 1; b <= 11; b++)
                {
                    num++;
                    if (gameController.nomalAttack[enj.summonNum].Code[i] == num)
                    {
                        //SterPos[a - 1].GetComponent<Image>().enabled = true;
                        //SterPos[b - 1].GetComponent<Image>().enabled = true;

                        GameObject obj = (GameObject)Instantiate(BLine, transform.position, Quaternion.identity, lineParent2.transform);
                        UILineRenderer data2 = obj.GetComponent<UILineRenderer>();
                        data2.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
                        data2.points[0] = new Vector2((SterPos[a - 1].transform.position.x - Screen.width / 2) / Screen.width * CanvasRect.sizeDelta.x, (SterPos[a - 1].transform.position.y - Screen.height / 2) / Screen.height * CanvasRect.sizeDelta.y);
                        data2.points[1] = new Vector2((SterPos[b - 1].transform.position.x - Screen.width / 2) / Screen.width * CanvasRect.sizeDelta.x, (SterPos[b - 1].transform.position.y - Screen.height / 2) / Screen.height * CanvasRect.sizeDelta.y);


                    }

                }
            }


        }
#endif
    }

    public void BlackLineDL()
    {
        foreach (Transform n in lineParent2.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
    }


    public void SummonCast()
    {
        if (sumonMode == false && StageCobtroller.Shooting == false&&Pad&&statusManager .summonGage >=100)
        {
            if(sumonbd == false)
            {
                if(!TutorialFlg .SummonOpen)
                {
                    gameController .ModeChange(29, 1);
                }
                sumonbd = true;
                newsummonanime.SetBool("Open", true);
                newsummonanime.SetBool("Close", false );

            }
            else
            {
                NewSummonCl();
            }

        }

    }


    public void NewSummonCl()
    {
        sumonbd = false;
        newsummonanime.SetBool("Open", false);
        newsummonanime.SetBool("Close", true);
    }



}
