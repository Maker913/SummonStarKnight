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

    //HP
    //public int myHP;
    //public int tekiHP;
    //public int gage;


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


    [SerializeField]
    private GameObject startEf;
    [SerializeField]
    Text startText;

    public float sterTime;


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
        }

        enemyTurn = Random.Range(2, 5);
        gaged = gageobj.GetComponent<Image>();
        animationManager = animeCon.GetComponent<AnimationManager>();
        text = textObj.GetComponent<Text>();
        padController2 = padControllerObj.GetComponent<PadController2>();
        enj = EnjObj.GetComponent<Enj>();
        statusManager = StatusManagerObj.GetComponent<StatusManager>();
        shooting = shootingObj.GetComponent<ShootingEnj>();
        startPas = 0;

        gameMode = 22;
        summonTutorialTime = 0;

        AudioManager.Instance.PlayBGM(AudioManager.BgmName.ThemeBGM);
    }



    void Update()
    {
        //Debug.Log(gameMode);
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
            textPr.SetActive(false);
            animationManager.ReState();

            ModeChange(12, 0);
            TextController.end = false;
        }


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
            textPr.SetActive(true);
            NewTextObj.GetComponent<NewTextData>().TextDataRead("MainStage/Stage"+StageCobtroller .stageNum .ToString ()+"-2");
            startPas = 1;
        }


        animationManager.Stop();


        padController2.Pad = false;
        textPadObj.SetActive(false);
        if (NewTextController.end || Input.GetKeyDown(KeyCode.Escape))
        {
            result = "勝利";
            text.text = "勝利";
            textPadObj.SetActive(true);


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
            textPr.SetActive(true);

            NewTextObj.GetComponent<NewTextData>().TextDataRead("Tutorial/SummonB");
            TutorialFlg.SummonBefore = true;
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
                fileName = "Stage"+ StageCobtroller .stageNum.ToString ()+"-1";
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
        countObj.SetActive(true);
        countObj.GetComponent<Text>().text = "終了";
        ShootingTime += Time.deltaTime;
        if (ShootingTime > 1)
        {
            countObj.SetActive(false);

            int datanum;
            do
            {
                datanum = Random.Range(0, technique.Length);
            } while (datanum == StageCobtroller.Technique[0]|| datanum == StageCobtroller.Technique[1]|| datanum == StageCobtroller.Technique[2]);

            StageCobtroller.Technique[StageCobtroller.stageNum] = datanum;
            ModeChange(17, 0);
        }
    }

    private void Shootingcountdown()
    {
        textPadObj.SetActive(false);
        countObj.SetActive(true);
        ShootingTime += Time.deltaTime;
        countObj.GetComponent<Text>().text = (3 - countNum).ToString();
        if (ShootingTime>1)
        {
            countNum++;
            ShootingTime = 0;
            if(countNum >= 3)
            {
                ModeChange(15, 0);
                countObj.SetActive(false );
            }
            else
            {
                
            }
        }
    }

    private void ShootingbeResult()
    {

        resultObj.SetActive(true);
        
        resultObjtext.GetComponent<Text>().text = "結果\n\n\n" + shooting.lineNum + "\n\n\n"+technique [StageCobtroller.Technique[StageCobtroller.stageNum]].Name  +"を召喚できるようになりました";

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
        ModeChange(18, 2);
        text.text = "ボーナスゲーム開始";
        textPadObj.SetActive(true);
        ShootingTime = 0;
        countNum = 0;
    }

    private void EnemyMove()
    {
        
        if (enemyTurn <=0)
        {
            AudioManager.Instance.PlaySE(AudioManager.SeName.enemy_Deathblow);
            text.text = "2ターンの間制限時間減少";
            statusManager.EnemyAction(Random.Range(1, statusManager .EnemyActionrange +1));
            textPadObj.SetActive(true);
            ModeChange(12, 2);
            enemyTurn = Random.Range(2, 5);
        }
        else
        {
            ModeChange(12, 0);
            enemyTurn--;
        }
        
    }


    private void Battlesoon()
    {

        if(!TutorialFlg.GageMax&&statusManager.summonGage >=100)
        {
            textPadObj.SetActive(false);
            ModeChange(28, 0);
            return;
        }

        startEf.SetActive(false);
        animationManager.AnimationStart(0,0,"Zodiac");
        StageCobtroller.Score += 1;
        statusManager.EnemyTurnCheck();
        statusManager.TurnCheck();
        ModeChange(2, 1f);
        camera.GetComponent<CameraController2>().SetCamera(2, 1);
    }

    private void ChaneSceen()
    {
        SceneControl .Instance .LoadScene(SceneControl.SceneName.Result ,true);
        //SceneChanger.instance.LoadScene("resultkari", 1);
    }

    private void Sumon()
    {
        enj.GetComponent<Animator>().SetBool("Open", true);
        string textdata=statusManager.SummonCheck(weapon);
        text.text = textdata;
        padController2.Pad = false;
        textPadObj.SetActive(true);
        animationManager.AnimationStart(0, 1, "transform");
        ModeChange(9, 2.5f);
    }

    private void SumonMiss()
    {
        enj.GetComponent<Animator>().SetBool("Open", true);
        text.text = "召喚に失敗しました";
        padController2.Pad = false;
        textPadObj.SetActive(true);
        ModeChange(9, 2.5f);
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
        textPadObj.SetActive(true);
        StageCobtroller.Score = 0;
        StageCobtroller.stageNum = 1;
        ModeChange(10, 1);
    }

    private void Win()
    {
        
        //SceneControl.Instance.LoadScene(SceneControl.SceneName.Stage1, true);

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
                camera.GetComponent<CameraController2>().SetCamera(0, 0.75f);
                dcont++;
                cameradTime += Time.deltaTime;
                enj.GetComponent<Animator>().SetBool("Open", true);
            }
            else
            {
                dcont = 0;
                cameradTime = 0;

                statusManager.summonGage += Random.Range(10, 20);
                if (statusManager.summonGage > 100)
                {
                    statusManager.summonGage = 100;
                }
                //teki.GetComponent<Animator>().SetTrigger("Attack");
                animationManager.AnimationStart(0, 0, "Attack");
                animationManager.AnimationStart(0, 1, "damage");
                //textPadObj.SetActive(true);
                //text.text = "ダメージアニメーション予定地";
                AudioManager.Instance.PlaySE(AudioManager.SeName.player_attack);
                statusManager.playerHP  -= statusManager.enemyAtk ;
                statusManager.BarrierCheck();
                if (statusManager.playerHP <= 0)
                {
                    ModeChange(7, 1.5f);
                }
                else
                {
                    ModeChange(11, 1.5f);
                }
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
                animeC = true;
                EffectControl.Instance.PlayEffect(EffectControl.Effect.Attack, AttackEfPos );
                camera.GetComponent<CameraController2>().SetCamera(3, 0.75f);
                dcont++;
                cameradTime += Time.deltaTime;
                enj.GetComponent<Animator>().SetBool("Open", true);


            }
            else
            {
                dcont = 0;
                cameradTime = 0;

                statusManager.summonGage += Random .Range (20,40);
                if (statusManager.summonGage > 100)
                {
                    statusManager.summonGage = 100;
                }
                
                animationManager.AnimationStart(0, 0, "Damage");
                AudioManager.Instance.PlaySE(AudioManager.SeName.player_attack);

                //textPadObj.SetActive(true);
                //text.text = "攻撃アニメーション予定地";
                if (padController2.oneLine)
                {
                    statusManager.enemyHP -= (int)(statusManager.playerAtk*1.5f);
                }
                else
                {
                    statusManager.enemyHP -= statusManager.playerAtk;
                }
                if (statusManager.enemyHP <= 0)
                {
                    ModeChange(6, 1.5f);
                }
                else
                {
                    if (!TutorialFlg.FastAtk)
                    {
                        ModeChange(27, 1.5f);
                    }
                    else
                    {
                        ModeChange(11, 1.5f);
                    }
                }
            }

        }
        else
        {
            cameradTime += Time.deltaTime;
            if (cameradTime > 0.1f&&animeC)
            {
                animationManager.AnimationStart(0, 1, "attack");
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
        text.text = "STAGE "+StageCobtroller .stageNum .ToString ();
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

