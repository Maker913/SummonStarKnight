using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountTimer : MonoBehaviour
{
    Text countText;
    [SerializeField]
    GameObject circleObj;
    Image circleImg;

    [SerializeField]
    float countTimer;

    [SerializeField]
    GameObject gameConObj;
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        countText = GetComponent<Text>();
        circleImg = circleObj.GetComponent<Image>();
        gameController = gameConObj.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update() {

        countTimer = gameController.sterTime-0.00001f;

        if (countTimer > 0)
        {
            countText.text = ((int)countTimer +1).ToString();

            
            circleImg.fillAmount = countTimer-(int)countTimer;
            if(countTimer < 5)
            {
                countText.color = Color.red;
                circleImg.color = Color.red;
            }
            
        }else{
            countText.text = "0";
            circleImg.fillAmount = 0;
        }
        
    }

}
