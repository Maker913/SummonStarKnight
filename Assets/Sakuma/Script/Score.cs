using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private Text text;
    private float time;
    private int cont;

    [SerializeField]
    private Sprite[] image;

    [SerializeField]
    private GameObject imageObj;
    private Image data;
    // Start is called before the first frame update
    void Start()
    {
        data = imageObj.GetComponent<Image>();
        data.color=new Color (0,0,0,0);
        text = GetComponent<Text>();
        time = -0.5f;
        cont = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool check = false;
        time += Time.deltaTime;

        if (time >= 0.5f)
        {
            cont++;
            time = 0;
            check = true;
        }

        if(check)
        {
            switch (cont)
            {
                case 1:
                    if (StageCobtroller.Win)
                    {
                        text.text = "\nゲームクリア!!\n\n";
                    }
                    else
                    {
                        text.text = "\nゲームオーバー\n\n";
                    }
                    break;
                case 2:
                    text.text += "使用ターン数 : "+StageCobtroller .Score .ToString ()+ "\n\n\n";
                    break;
                case 3:
                    text.text += "総合評価 : ";
                    break;
                case 4:
                    data.color = new Color(1, 1, 1, 1);
                    if (StageCobtroller.Score < 15&& StageCobtroller.Win)
                    {
                        data.sprite = image[0];
                    }else if (StageCobtroller.Score < 20 && StageCobtroller.Win)
                    {
                        data.sprite = image[1];
                    }
                    else if (StageCobtroller.Score < 25 && StageCobtroller.Win)
                    {
                        data.sprite = image[2];
                    }
                    else
                    {
                        data.sprite = image[3];
                    }
                    StageCobtroller.Win = false;
                        break;
                default:
                    break;



            }
        }








    }
}
