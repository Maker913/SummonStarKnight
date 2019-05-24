using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{

    /// <summary>
    /// SE再生。AUDIO.SE_BUTTONがSEのファイル名
    ///AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);
    ///BGM再生。AUDIO.BGM_BATTLEがBGMのファイル名
    ///AudioManager.Instance.PlayBGM(AUDIO.BGM_BATTLE, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
    ///BGMフェードアウト
    ///AudioManager.Instance.FadeOutBGM();
    /// </summary>
    public static AudioManager Instance;
    //音量保存
    private const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";
    private const string SE_VOLUME_KEY = "SE_VOLUME_KEY";
    private const float BGM_VOLUME_DEFULT = 1.0f;
    private const float SE_VOLUME_DEFULT = 1.0f;
    //
    public const float BGM_FADE_SPEED_HIGH = 0.9f;
    public const float BGM_FADE_SPEED_LOW = 0.3f;
    private float _BgmFadeSpeedRate = BGM_FADE_SPEED_HIGH;
    //次に流すやつ
    private string _nextBGMName;
    private string _nextSEName;
    //フェードアウト判定
    private bool _isfadeOut = false;
    //SEとBGMの区別
    public AudioSource BGMSource, SESource;
    //Audio保持
    private Dictionary<string, AudioClip> _bgmDic, _seDic;
    //
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        _bgmDic = new Dictionary<string, AudioClip>();
        _seDic = new Dictionary<string, AudioClip>();
        object[] bgmList = Resources.LoadAll("Audio/BGM");
        object[] seList = Resources.LoadAll("Audio/SE");

        foreach(AudioClip bgm in bgmList)
        {
            _bgmDic[bgm.name] = bgm;
        }
        foreach(AudioClip se in seList)
        {
            _seDic[se.name] = se;
        }
    }
    private void Start()
    {
        BGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
        SESource.volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);
    }
    //SE
    public void PlaySE(string seName,float delay = 0.0f)
    {
        if (!_seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "という名前はSEがありません");
            return;
        }
        _nextSEName = seName;
        Invoke("DelayPlaySE", delay);
    }
    private void DelayPlaySE()
    {
        SESource.PlayOneShot(_seDic[_nextSEName] as AudioClip);
    }
    //BGM
    public void PlayBGM(string bgmname,float fadeSpeedRate = BGM_FADE_SPEED_HIGH)
    {
        if (!_bgmDic.ContainsKey(bgmname))
        {
            Debug.Log(bgmname + "という名前のBGMはありません");
            return;
        }
        if (!BGMSource.isPlaying)
        {
            _nextBGMName = "";
            BGMSource.clip = _bgmDic[bgmname] as AudioClip;
            BGMSource.Play();
        }
        else if(BGMSource.clip.name != bgmname)
        {
            _nextBGMName = bgmname;
            FadeOutBGM(fadeSpeedRate);
        }
    }
    //現在の曲をフェードアウト
    public void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_LOW)
    {
        _BgmFadeSpeedRate = fadeSpeedRate;
        _isfadeOut = true;
    }

    private void Update()
    {
        if (!_isfadeOut)
        {
            return;
        }
        BGMSource.volume -= Time.deltaTime * _BgmFadeSpeedRate;
        if(BGMSource.volume <= 0)
        {
            BGMSource.Stop();
            BGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
            _isfadeOut = false;

            if (!string.IsNullOrEmpty(_nextBGMName))
            {
                PlayBGM(_nextBGMName);
            }
        }
    }
    //音量調整
    public void ChangeVolume(float BGMVolume, float SEVolume)
    {
        BGMSource.volume = BGMVolume;
        SESource.volume = SEVolume;

        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, BGMVolume);
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, SEVolume);
    }
}
