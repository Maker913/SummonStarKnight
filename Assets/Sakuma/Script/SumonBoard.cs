using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumonBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField]
    private GameObject sumonbd;
    [SerializeField]
    private GameObject Pad;
    [SerializeField]
    private GameObject game;
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private GameObject nameobj;
    [SerializeField]
    private GameObject gameCon;

    public void Close()
    {
        Pad.GetComponent<PadController2>().sumonbd = false;
        sumonbd.SetActive(false);
        //Debug.Log(123454);
    }


    public void Sumon(GameObject Button)
    {
        int num = Button.GetComponent<ButtonNum>().Num;

        if (game.GetComponent<StatusManager>().summonGage  >= 100&&gameCon.GetComponent<GameController >().gameMode ==2 )
        {
            nameobj.GetComponent<Animator>().SetBool("Name", false);
            game.GetComponent<StatusManager>().summonGage = 0;
            Pad.GetComponent<PadController2>().sumonMode = true;
            Pad.GetComponent<PadController2>().sumonbd = false;
            Pad.GetComponent<PadController2>().NewSummonCl ();
            sumonbd.SetActive(false);
            Pad.GetComponent<PadController2>().sumonNum = num;
            Pad.GetComponent<PadController2>().summonDelay =1;
            Pad.GetComponent<PadController2>().summonRem = true;
            Pad.GetComponent<PadController2>().BoardReset();


            camera.GetComponent<CameraController2>().SetCamera(1, 1);
        }
        else
        {
            //Debug.Log("召喚に必要なゲージが足りません");
        }

    }






}
