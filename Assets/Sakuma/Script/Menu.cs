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

    void Start()
    {
        Pad = PadObj.GetComponent<PadController2>();
        Game = Gameobj.GetComponent<GameController>();
        menuanime = menu.GetComponent<Animator>();
    }

    public void MenuOn()
    {
        if (Game.gameMode ==2) {
            Pad.Pad = false;
            Game.ModeChange(5, 0);
            menu.SetActive(true);
            menuanime.SetBool("Change", true);
        }
    }


    public void MenuOff()
    {
        Pad.Pad = true;
        Game.ModeChange(2, 0);
        menu.SetActive(false);
        menuanime.SetBool("Change",false );
    }
}
