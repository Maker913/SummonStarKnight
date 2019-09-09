using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct Technique
{
    public string Name;
    public float Power;
    public float Time;
    public int[] Code;
}




public class GameController : MonoBehaviour
{

    //現在のゲームモード
    public int gameMode;
    //技
    public Technique[] technique;
    public Technique[] nomalAttack;
    //選択された技
    public int weapon;

    ////Delay関数内で主に使うもの

    //Delay中に停止しておく時間
    private float delayTime;
    //経過時間
    private float elapsedTime;
    //次のゲームモードを保存しておくバッファ
    private int nextGameMode = 0;


    //PadControllerアクセス用
    [SerializeField]
    private GameObject padControllerObj;
    private PadController2 padController2;

    //テキストパットとか奴
    [SerializeField]
    private GameObject textPadObj;
    [SerializeField]
    private GameObject textObj;
    private Text text;
    [SerializeField]
    private GameObject nameObj;
    private Text nametext;

    string Ename;

    //2のみ開始時処理使用
    public  int startPas=0;


    //敵星座板の奴
    [SerializeField]
    private GameObject EnjObj;
    private Enj enj;

    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private GameObject gageobj;
    private Image gaged;

    private int dcont = 0;
    private float cameradTime = 0;


    [SerializeField]
    private GameObject StatusManagerObj;
    private StatusManager statusManager;

    private int enemyTurn;


    [SerializeField]
    private GameObject shootingObj;
    private ShootingEnj shooting;

    private float ShootingTime;
    private int countNum = 0;

    [SerializeField]
    private GameObject countObj;
    [SerializeField]
    private GameObject resultObj;
    [SerializeField]
    private GameObject resultObjtext;

    [SerializeField]
    private GameObject textPr;

    [SerializeField]
    private GameObject animeCon;
    private AnimationManager animationManager;

    private Vector3   AttackEfPos;

    private bool animeC=false ;

    private float summonTutorialTime = 0;

    [SerializeField]
    private GameObject NewTextObj;

    private float fastContTime = 0;
    public int combo = 0;

    [SerializeField]
    private GameObject startEf;
    [SerializeField]
    Text startText;

    public float sterTime;
    [SerializeField]
    GameObject summonobj;

    [SerializeField]
    GameObject startef;
    [SerializeField]
    GameObject endef;

    [SerializeField]
    GameObject AttackEf;

    [SerializeField]
    float[] effectdirayTime=new float[3];
    [SerializeField ]
    float[] BreakdirayTime = new float[3];
    [SerializeField]
    float[] refrectdirayTime = new float[3];
    [SerializeField]
    float[] damagedirayTime = new float[3];

    [SerializeField]
    GameObject Cant;

    [SerializeField]
    GameObject tekiHPObj;

    public AuraController auraController;

    [SerializeField]
    Sprite[] countdown;
    [SerializeField]
    GameObject countObjct;
    Image countinage;

    static public string result = "NULL";
    [Space(10)]
    [Header("ここからエフェクト")]




    //ここからエフェクト用
    [SerializeField]
    public GameObject teki;


    void Start()
    {
        if(StageCobtroller .stageNum ==1&&StageCobtroller .Shooting ==false)
        {
            TutorialFlg.TutorialReSet();
            StageCobtroller .Technique = new int[3]{ 0,-1,-1};
        }
        countinage = countObjct.GetComponent<Image>();
        enemyTurn = Random.Range(2, 5);
        gaged = gageobj.GetComponent<Image>();
        animationManager = animeCon.GetComponent<AnimationManager>();
        text = textObj.GetComponent<Text>();
        nametext = nameObj.GetComponent<Text>();
        padController2 = padControllerObj.GetComponent<PadController2>();
        enj = EnjObj.GetComponent<Enj>();
        statusManager = StatusManagerObj.GetComponent<StatusManager>();
        shooting = shootingObj.GetComponent<ShootingEnj>();
        startPas = 0;

        gameMode = 22;
        summonTutorialTime = 0;

        AudioManager.Instance.PlayBGM(AudioManager.BgmName.ThemeBGM);


        switch (StageCobtroller.stageNum)
        {
            case 1:
                Ename = "魚座";
                break;
            case 2:
                Ename = "蟹座";
                break;
            case 3:
                Ename = "蛇使い座";
                startText.GetComponent<Text>().fontSize = 64;
                break;

        }

    }



    void Update()
    {
        //Debug.Log(gameMode);
        //Debug.Log(Random.Range(StageCobtroller.stageNum, statusManager.EnemyActionrange + StageCobtroller.stageNum));
        gaged.fillAmount = (float)statusManager .summonGage  / 100f;
        switch (gameMode)
        {
            case 0:
                Delay();
                break;
            case 1:
                Stert();
                break;
            case 2:
                Battle();
                break;
            case 3:
                MyAtk();
                break;
            case 4:
                EnemyAtk();
                break;
            case 5:
                Menu();
                break;
            case 6:
                Win();
                break;
            case 7:
                Lose();
                break;
            case 8:
                Sumon();
                break;
            case 9:
                Stop();
                break;
            case 10:
                ChaneSceen();
                break;
            case 11:
                EnemyMove();
                break;
            case 12:
                Battlesoon();
                break;
            case 13:
                ShootingStart();
                break;
            case 14:
                Shooting();
                break;
            case 15:
                Shootingbefore();
                break;
            case 16:
                ShootingbeEnd();
                break;
            case 17:
                ShootingbeResult();
                break;
            case 18:
                Shootingcountdown();
                break;
            case 19:
                ShootingEndCount();
                break;
            case 20:
                WinNext();
                break;
            case 21:
                ShootingbeEnd2();
                break;

            case 22:
                Scenario();
                break;
            case 23:
                SumonMiss();
                break;
            case 24:
                summonTutorial();
                break;
            case 25:
                EndScenario();
                break;
            case 26:
                FastContactTutorial();
                break;

            case 27:
                FastAtk();
                break;
            case 28:
                GageMax();
                break;
            case 29:
                SummonOpen();
                break;
            case 30:
                SummonMissTuto();
                break;







            case 99:
                None();
                break;


            default:
                break;
        }
    }

    public void ObjPosSet(Vector3   apos)
    {
        AttackEfPos = apos;
    }



    private void SummonMissTuto()
    {
        if (startPas == 0 && !StageCobtroller.Shooting)
        {
            textPr.SetActive(true);
            NewTextObj.GetComponent<NewTextData>().TextDataRead("Tutorial/SummonMiss");
            TutorialFlg.SummonOpen = true;
            startPas = 1;
        }


        animationManager.Stop();


        padController2.Pad = false;
        textPadObj.SetActive(false);
        if (NewTextController.end || Input.GetKeyDown(KeyCode.Escape))
        {
            textPr.SetActive(false);
            animationManager.ReState();
            summonobj.GetComponent<SumonBoard>().Rep();
            ModeChange(2, 0);
            startPas = 1;
            TextController.end = false;
        }
    }



    private void SummonOpen()
    {
        if (startPas == 0 && !StageCobtroller.Shooting)
        {
            
            textPr.SetActive(true);
            NewTextObj.GetComponent<NewTextData>().TextDataRead("Tutorial/SyoukanMENUwo dasita ato");
            TutorialFlg.SummonOpen  = true;
            startPas = 1;
        }


        animationManager.Stop();


        padController2.Pad = false;
        textPadObj.SetActive(false);
        if (NewTextController.end || Input.GetKeyDown(KeyCode.Escape))
        {
            textPr.SetActive(false);
            animationManager.ReState();

            ModeChange(2, 0);
            startPas = 1;
            TextController.end = false;
        }
    }

    private void GageMax()
    {


        if (startPas == 0 && !StageCobtroller.Shooting)
        {
            TutorialFlg.CantAnyButton = true;
            textPr.SetActive(true);
            NewTextObj.GetComponent<NewTextData>().TextDataRead("Tutorial/SyoukanGAGEga tamattatoki");
            TutorialFlg.GageMax  = true;

            startPas = 1;
        }


        animationManager.Stop();


        padController2.Pad = false;
        textPadObj.SetActive(false);
        if (NewTextController.end || Input.GetKeyDown(KeyCode.Escape))
        {
            Invoke("CantInv", 0.99f);
            textPr.SetActive(false);
            animationManager.ReState();

            ModeChange(12, 0);
            TextController.end = false;
        }


    }

    private void CantInv()
    {
        Cant.SetActive(true);
    }
    private void FastAtk()
    {


        if (startPas == 0 && !StageCobtroller.Shooting)
        {
            textPr.SetActive(true);
            NewTextObj.GetComponent<NewTextData>().TextDataRead("Tutorial/Seizabanwo nazotta ato");
            TutorialFlg.FastAtk = true;

            startPas = 1;
        }


        animationManager.Stop();


        padController2.Pad = false;
        textPadObj.SetActive(false);
        if (NewTextController.end || Input.GetKeyDown(KeyCode.Escape))
        {
            textPr.SetActive(false);
            animationManager.ReState();

            ModeChange(11, 0); 
            TextController.end = false;
        }


    }


    private void FastContactTutorial()
    {


        if (startPas == 0 && !StageCobtroller.Shooting)
        {
            textPr.SetActive(true);
            NewTextObj.GetComponent<NewTextData>().TextDataRead("Tutorial/Seizabanwo nazorumae");
            TutorialFlg.FastContact = true;
            startPas = 1;
        }


        animationManager.Stop();


        padController2.Pad = false;
        textPadObj.SetActive(false);
        if (NewTextController.end || Input.GetKeyDown(KeyCode.Escape))
        {
            textPr.SetActive(false);
            animationManager.ReState();

            ModeChange(2, 0);
            startPas = 1;
            TextController.end = false;
        }


    }


    private void EndScenario()
    {
        if (startPas == 0&&!StageCobtroller .Shooting )
        {
            tekiHPObj.SetActive(false);
            textPr.SetActive(true);
            NewTextObj.GetComponent<NewTextData>().TextDataRead("MainStage/1Stage"+StageCobtroller .stageNum .ToString ()+"-2");
            startPas = 1;
        }


        animationManager.Stop();


        padController2.Pad = false;
        textPadObj.SetActive(false);
        if (NewTextController.end || Input.GetKeyDown(KeyCode.Escape))
        {
            result = "勝利";
            //text.text = "勝利";
            //textPadObj.SetActive(true);


            textPr.SetActive(false);
            animationManager.ReState();

            ModeChange(20, 1);
            startPas = 1;
            TextController.end = false;
        }
    }


    private void summonTutorial()
    {
        if (startPas == 0)
        {
            TutorialFlg.CantButton = false;
            TutorialFlg.CantAnyButton = false;
            textPr.SetActive(true);

            NewTextObj.GetComponent<NewTextData>().TextDataRead("Tutorial/SummonB");
            
            startPas = 1;
        }


        animationManager.Stop();


        padController2.Pad = false;
        textPadObj.SetActive(false);
        if (NewTextController.end || Input.GetKeyDown(KeyCode.Escape))
        {
            TutorialFlg.SummonBefore = true;
            textPr.SetActive(false);
            animationManager.ReState();

            ModeChange(2, 0);
            startPas = 1;
            TextController.end = false;
        }



    }





    private void Scenario()
    {

        if (startPas == 0)
        {
            textPr.SetActive(true);

            string folderName = "";
            string fileName = "";

            if (!StageCobtroller .Shooting)
            {
                folderName = "MainStage";
                fileName = "1Stage"+ StageCobtroller .stageNum.ToString ()+"-1";
                if (StageCobtroller.stageNum  == 1)
                {
                    StageCobtroller.Score = 0;
                }
            }
            else
            {
                folderName = "Shooting";
                fileName = "Shooting" + StageCobtroller.stageNum.ToString();
            }
            
            NewTextObj.GetComponent<NewTextData>().TextDataRead(folderName+"/"+fileName );

            //NewTextObj.GetComponent<NewTextData>().TextDataRead("MainStage/Scenario1");
            startPas = 1;
        }



        animationManager.Stop();


        padController2.Pad = false;
        textPadObj.SetActive(false );
        if (NewTextController .end||Input .GetKeyDown(KeyCode.Escape))
        {
            textPr.SetActive(false);
            animationManager.ReState ();
            if (StageCobtroller.Shooting == false)
            {
                ModeChange(1, 0);
            }
            else
            {
                ModeChange(13, 0);
            }
            TextController.end = false;
        }
    }

    private void None()
    {

    }

    private void ShootingEndCount()
    {
        padController2.Pad = false;
        //countObj.SetActive(true);
        //countObj.GetComponent<Text>().text = "GAME SET";
        if (startPas==0) {
            endef.GetComponent<Animator>().SetTrigger("set");
            startPas = 1;
        }
        ShootingTime += Time.deltaTime;
        if (ShootingTime > 1.3f)
        {
            countObj.SetActive(false);

            int datanum;
            do
            {
                datanum =  Random.Range(0, technique.Length);
            } while (datanum == StageCobtroller.Technique[0]|| datanum == StageCobtroller.Technique[1]|| datanum == StageCobtroller.Technique[2]);

            StageCobtroller.Technique[StageCobtroller.stageNum] = datanum;
            ModeChange(17, 0);
        }
    }

    private void Shootingcountdown()
    {
        countObjct.SetActive(true);
        textPadObj.SetActive(false);
        //countObj.SetActive(true);
        ShootingTime += Time.deltaTime;
        //countObj.GetComponent<Text>().text = (3 - countNum).ToString();
        countinage.sprite = countdown[countNum];
        if (ShootingTime>1)
        {
            countNum++;
            ShootingTime = 0;
            if(countNum >= 3)
            {
                ModeChange(15, 0);
                countObj.SetActive(false );
                countObjct.SetActive(false );
            }
            else
            {
                
            }
        }
    }

    private void ShootingbeResult()
    {

        resultObj.SetActive(true);
        
        resultObjtext.GetComponent<Text>().text = " スコア : " + shooting.lineNum + "\n\n "+technique [StageCobtroller.Technique[StageCobtroller.stageNum]].Name  +"を召喚できるようになりました";

        if (Input.touchCount != 0)
        {
            ModeChange(16, 0);
        }

    }
    private void ShootingbeEnd2()
    {
        SceneControl.Instance.LoadScene(SceneControl.SceneName.Stage1, true);
    }
    private void ShootingbeEnd()
    {
        StageCobtroller.stageNum++;
        StageCobtroller.Shooting = false;
        ModeChange(21, 0);
    }

    private void Shootingbefore()
    {
        padController2.Pad = true;
        textPadObj.SetActive(false);
        ShootingTime = 0;
        ModeChange(14, 0);
        shooting.RandSelect();
    }
    private void Shooting()
    {
        sterTime  -= Time.deltaTime;

        if (sterTime <= 0)
        {
            ModeChange(19, 0);
            padController2.Pad = false ;
            ShootingTime = 0;
        }


    }

    private void ShootingStart()
    {
        textPr.SetActive(false);
        ModeChange(18, 1.5f);
        startef.GetComponent<Animator>().SetTrigger("set");
        //text.text = "BONUS GAME";
        //countObj.SetActive(true);
        //countObj.GetComponent<Text>().text = "BONUS GAME";
        //textPadObj.SetActive(true);
        ShootingTime = 0;
        countNum = 0;
    }

    private void EnemyMove()
    {
        
        if (enemyTurn <=0)
        {
            AudioManager.Instance.PlaySE(AudioManager.SeName.enemy_Deathblow);
            
            nametext.text = Ename+"の妨害能力";
            
            text.text = statusManager.EnemyAction(Random.Range(StageCobtroller.stageNum, statusManager .EnemyActionrange+StageCobtroller .stageNum ));
            textPadObj.SetActive(true);
            Invoke("textpadOff", 2f);
            ModeChange(12, 2);
            enemyTurn = Random.Range(2, 5);
        }
        else
        {
            ModeChange(12, 0);
            enemyTurn--;
        }
        
    }


    private void textpadOff()
    {
        textPadObj.SetActive(false );
    }

    private void Battlesoon()
    {
        if(statusManager .yagichan)
        {
            statusManager.summonGage += 20;
        }
        if(!TutorialFlg.GageMax&&statusManager.summonGage >=100)
        {
            textPadObj.SetActive(false);
            ModeChange(28, 0);
            return;
        }

        startEf.SetActive(false);
        if (statusManager.enemyHP > 0)
        {
            animationManager.AnimationStart(0, 0, "Zodiac");
        }
        StageCobtroller.Score += 1;
        statusManager.EnemyTurnCheck();
        statusManager.TurnCheck();
        if (statusManager.enemyHP <= 0)
        {
            ModeChange(6, 1f);
        }
        else
        {
            ModeChange(2, 1f);
        }
        camera.GetComponent<CameraController2>().SetCamera(2, 1);
    }

    private void ChaneSceen()
    {
        SceneControl .Instance .LoadScene(SceneControl.SceneName.Result ,true);
        //SceneChanger.instance.LoadScene("resultkari", 1);
    }

    private void Sumon()
    {
        if(!TutorialFlg.FastSummonMiss)
        {
            TutorialFlg.FastSummonMiss = true;
        }
        //EffectControl.Instance.PlayEffect(EffectControl.Effect .Aura_Red,AttackEfPos ,Vector3 .zero);

        enj.GetComponent<Animator>().SetBool("Open", true);
        auraController.AuraOn(weapon);
        string textdata=statusManager.SummonCheck(weapon);
        text.text = textdata;
        nametext.text = " "+technique[weapon].Name + "の力";
        padController2.Pad = false;
        textPadObj.SetActive(true);
        animationManager.AnimationStart(0, 1, "transform");


            ModeChange(9, 2.5f);
        
        
    }

    private void SumonMiss()
    {
        if (TutorialFlg.FastSummonMiss)
        {
            enj.GetComponent<Animator>().SetBool("Open", true);
            text.text = " 能力を取得できませんでした";
            nametext.text = "召喚失敗";
            padController2.Pad = false;
            textPadObj.SetActive(true);
            ModeChange(9, 2.5f);
        }
        else
        {
            ModeChange(30, 0);
        }
    }


    private void Stop()
    {
        
        ModeChange(12, 0);
        textPadObj.SetActive(false );
        StageCobtroller.Score -= 1;
        //camera.GetComponent<CameraController2>().SetCamera(0, 1);
    }

    private void Lose()
    {
        result = "敗北";
        text.text = "敗北";
        padController2.Pad = false;
        //textPadObj.SetActive(true);
        StageCobtroller.Score = 0;
        StageCobtroller.stageNum = 1;
        ModeChange(10, 1);
    }

    private void Win()
    {

        //SceneControl.Instance.LoadScene(SceneControl.SceneName.Stage1, true);
        //animationManager.AnimationStart(0, 1, "victory");
        ModeChange(25, 0);
    }



    private void WinNext()
    {
        if (StageCobtroller.stageNum == 3)
        {
            StageCobtroller.Win = true;
            StageCobtroller.stageNum = 1;
            StageCobtroller.Technique[0] = 0;
            StageCobtroller.Technique[1] = -1;
            StageCobtroller.Technique[2] = -1;
            SceneControl.Instance.LoadScene(SceneControl.SceneName.Result, true);
        }
        else
        {
            StageCobtroller.Shooting = true;
            SceneControl.Instance.LoadScene(SceneControl.SceneName.Stage1, true);
        }
        ModeChange(99, 0);
    }



    private void EnemyAtk()
    {
        padController2.Pad = false;
        if (cameradTime == 0 || cameradTime > 1)
        {
            if (dcont == 0)
            {
               
                combo = 0;
                camera.GetComponent<CameraController2>().SetCamera(0, 0.75f);
                dcont++;
                cameradTime += Time.deltaTime;
                enj.GetComponent<Animator>().SetBool("Open", true);
            }
            else
            {
                AttackEf.GetComponent<AttackEffect>().btfalse(effectdirayTime[StageCobtroller.stageNum - 1]);
                dcont = 0;
                cameradTime = 0;


                //teki.GetComponent<Animator>().SetTrigger("Attack");
                animationManager.AnimationStart(0, 0, "Attack");
                animationManager.AnimationStart(damagedirayTime [StageCobtroller .stageNum -1], 1, "damage");
                //textPadObj.SetActive(true);
                //text.text = "ダメージアニメーション予定地";
                //AudioManager.Instance.PlaySE(AudioManager.SeName.player_attack);
                ///
                ModeChange(99, 0);
            }

        }
        else
        {
            cameradTime += Time.deltaTime;
        }


    }

    private void MyAtk()
    {
        
        padController2.Pad = false;
        if (cameradTime == 0 || cameradTime > 1)
        {
            if (dcont == 0)
            {
                if(!TutorialFlg.FastGageStop)
                {
                    TutorialFlg.FastGageStop = true;
                }

                combo++;
                animeC = true;
                // エフェクト修正箇所
                //EffectControl.Instance.PlayEffect(EffectControl.Effect.Attack, AttackEfPos );
                camera.GetComponent<CameraController2>().SetCamera(3, 0.75f);
                dcont++;
                cameradTime += Time.deltaTime;
                enj.GetComponent<Animator>().SetBool("Open", true);


            }
            else
            {
                AttackEf.GetComponent<AttackEffect>().bttrue(effectdirayTime [ StageCobtroller .stageNum -1]);
                dcont = 0;
                cameradTime = 0;


                animationManager.AnimationStart(0, 0, "Attack");
                animationManager.AnimationStart(BreakdirayTime[StageCobtroller.stageNum -1], 0, "Damage");
                animationManager.AnimationStart(refrectdirayTime[StageCobtroller.stageNum - 1], 1, "attack");
                //animationManager.AnimationStart(0, 0, "Damage");
                //AudioManager.Instance.PlaySE(AudioManager.SeName.player_attack);

                //textPadObj.SetActive(true);
                //text.text = "攻撃アニメーション予定地";

                ///
                ModeChange(99, 0);

            }

        }
        else
        {
            cameradTime += Time.deltaTime;
            if (cameradTime > 0.1f&&animeC)
            {
                //animationManager.AnimationStart(0, 1, "attack");
                animeC = false;
            }
        }

    }

    private void Battle()
    {
        if(startPas == 0)
        {

            padController2.oneLine = true;
            enj.GetComponent<Animator>().SetBool("Start", true);
            enj.GetComponent<Animator>().SetBool("Open", false);
            enj.NextGame();
            startPas = 1;



            
        }

        text.text = "";
        padController2.Pad = true;
        textPadObj.SetActive(false);



        if (padController2 .sumonMode)
        {
            summonTutorialTime += Time.deltaTime;

            if(summonTutorialTime>1&&!TutorialFlg.SummonBefore)
            {
                ModeChange(24,0);
            } 

        }

        if (!TutorialFlg.FastContact)
        {
            padController2.Pad = false;
            fastContTime += Time.deltaTime;
            if (fastContTime > 0.5f)
            {
                ModeChange(26, 0);
                return;
            }
        }




        
    }

    private void Stert()
    {
        camera.GetComponent<CameraController2>().SetCamera(0, 2);
        
        //textPadObj.SetActive(true);
        //text.text = "STAGE "+StageCobtroller .stageNum .ToString ();
        startEf.GetComponent<Animator>().SetBool("set", true);
        string name="";

        switch (StageCobtroller .stageNum)
        {
            case 1:
                name = "魚座";
                break;
            case 2:
                name = "蟹座";
                break;
            case 3:
                name = "蛇使い座";
                startText.GetComponent<Text>().fontSize =64;
                break;

        }

        startText.GetComponent<Text>().text = name;
        ModeChange(12, 3f);
    }

    private void Menu()
    {
        textPadObj.SetActive(false);
    }





    public void ModeChange(int nextGameMode2,float delayTime2)
    {
        startPas = 0;
        elapsedTime = 0;
        nextGameMode = nextGameMode2;
        delayTime = delayTime2;
        gameMode = 0;
    }

    private void Delay()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >delayTime)
        {
            elapsedTime = 0;
            gameMode = nextGameMode;
        }
    }



}

