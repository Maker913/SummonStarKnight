using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField]
    float countTimer;
    [SerializeField]
    int recastTime;
    int visionTimer;

    [SerializeField]
    int fewI;           //少数の数

    Image[] digit;
    Text Text;
    Sprite[] number;
    int childNum;

    public bool flag;

    // Start is called before the first frame update
    void Start()
    {
        childNum = transform.childCount;
        Text = gameObject.GetComponent<Text>();
        number = Resources.LoadAll<Sprite>("Number"); //画像一括読み込み
        digit = new Image[childNum];
        int count = 0;
        foreach(Transform child in transform) {
            digit[count] = child.gameObject.GetComponent<Image>();
            count++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        visionTimer = (int)countTimer + 1;


        if (flag == false) {
            DrawTimer(countTimer);
        }



        if (flag == true) {
            if (countTimer < 10) {
                Text.text = "0" + countTimer.ToString("0.000");
            } else {
                Text.text = countTimer.ToString("0.000");
            }
        }

        countTimer -= Time.deltaTime;
        if (countTimer < 0) {
            countTimer = recastTime;
        }
    }

    void DrawTimer(float time) {
        int work = 10;
        int i = 1;
        int temp = 0;
        //桁数計算
        while ((int)time >= work) {
            i++;
            work *= 10;
        }
        //使わなくなった桁を0表示
        if (i < childNum - fewI) {
            for (int j = childNum - fewI; j > i; j--) {
                digit[j - 1].sprite = number[0];
            }
        }
        
        //桁数繰り返す
        for (; i > 0; i--) {
            temp = (int)time / ((int)Mathf.Pow(10.0f, (i - 1)));
            time = time % ((int)Mathf.Pow(10.0f, (i - 1)));
            digit[i - 1].sprite = number[temp];
        }
        
        //小数点表示
        for(int k = 0; k < childNum - (childNum - fewI); k++) {
            temp = (int)(time * 10.0f);
            time = time * 10.0f % 1;
            digit[k + (childNum - fewI)].sprite = number[temp];
        }
    }
}
