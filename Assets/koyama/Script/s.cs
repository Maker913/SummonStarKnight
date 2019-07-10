using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s : MonoBehaviour
{
    private void Start()
    {
        //SE再生。AUDIO.SE_BUTTONがSEのファイル名
        //AudioManager.Instance.PlaySE(sounds2);
        //BGM再生。AUDIO.BGM_BATTLEがBGMのファイル名
        //AudioManager.Instance.PlayBGM(AUDIO.BGM_BATTLE, AudioManager.BGM_FADE_SPEED_HIGH);
        //BGMフェードアウト
        //AudioManager.Instance.FadeOutBGM();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlaySE(AudioManager.SeName.button);
            Debug.Log("鳴った");
        }
    }
}
