using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    [SerializeField]
    private GameObject PadObj;
    private PadController2 Pad;

    [SerializeField]
    private GameObject Gameobj;
    private GameController Game;

    [SerializeField]
    private GameObject menu;
    private Animator menuanime;

    [SerializeField]
    private GameObject amine;
    private AnimationManager animationManager;

    void Start()
    {
        animationManager = amine.GetComponent<AnimationManager>();
        Pad = PadObj.GetComponent<PadController2>();
        Game = Gameobj.GetComponent<GameController>();
        menuanime = menu.GetComponent<Animator>();
    }



    public void TitleBack()
    {
        StageCobtroller.stageNum = 1;
        StageCobtroller.Shooting = false;
        StageCobtroller.Score = 0;
        SceneControl.Instance.LoadScene(SceneControl.SceneName.Title, true);
    }


    public void MenuOn()
    {
        if (!TutorialFlg.CantAnyButton) {
            animationManager.Stop();

            if (Game.gameMode == 2 && Pad.sumonbd == false) {
                Pad.Pad = false;
                Game.ModeChange(5, 0);
                menu.SetActive(true);
                menuanime.SetBool("Change", true);
            }
        }
    }


    public void MenuOff()
    {

        animationManager.ReState ();
        Pad.Pad = true;
        Game.ModeChange(2, 0);
        Game.startPas = 1;
        menu.SetActive(false);
        menuanime.SetBool("Change",false );
    }
}
