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
    public void Close()
    {
        Pad.GetComponent<PadController2>().sumonbd = false;
        sumonbd.SetActive(false);
        //Debug.Log(123454);
    }


    public void Sumon(int num)
    {if (game.GetComponent<GameController>().gage >= 100)
        {
            game.GetComponent<GameController>().gage = 0;
            Pad.GetComponent<PadController2>().sumonMode = true;
            Pad.GetComponent<PadController2>().sumonbd = false;
            sumonbd.SetActive(false);
            Pad.GetComponent<PadController2>().sumonNum = num;
            Pad.GetComponent<PadController2>().BlackLine();

            camera.GetComponent<CameraController2>().SetCamera(1, 1);
        }
        else
        {
            Debug.Log("召喚に必要なゲージが足りません");
        }

    }






}
