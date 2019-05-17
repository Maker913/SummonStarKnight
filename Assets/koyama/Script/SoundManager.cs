using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour {
    public class Sound
    {
        //SE数
        const int SE_CHANNEL = 2;
        enum Type
        {
            Bgm,
            Se,
        }
        static Sound _singlton = null;
        public static Sound GetInstance()
        {
            return _singlton ?? (_singlton = new Sound());
        }
        GameObject _object = null;
        AudioSource _sourceBgm = null;
        AudioSource _sourceSeDefault = null;
        AudioSource[] _sourceSeArray;
        //BGMアクセステーブル
        Dictionary<string, _Data> _poolBgm = new Dictionary<string, _Data>();
        //SEアクセステーブル
        Dictionary<string, _Data> _poolSe = new Dictionary<string, _Data>();

        class _Data
        {
            public string Key;
            public string NumberName;
            public AudioClip Clip;

            public _Data(string key, string number)
            {
                Key = key;
                NumberName = "sounds/" + number;
                Clip = Resources.Load(NumberName) as AudioClip;
            }
        }
        public Sound()
        {
            _sourceSeArray = new AudioSource[SE_CHANNEL];
        }

        AudioSource _GetAudioSource(Type type, int channel = 1)
        {
            if (_object == null)
            {
                _object = new GameObject("Sound");
                GameObject.DontDestroyOnLoad(_object);

                _sourceBgm = _object.AddComponent<AudioSource>();
                _sourceSeDefault = _object.AddComponent<AudioSource>();

                for (int i = 0; i < SE_CHANNEL; i++)
                {
                    _sourceSeArray[i] = _object.AddComponent<AudioSource>();
                }
            }
            if (type == Type.Bgm)
            {
                //bgm
                return _sourceBgm;
            }
            else
            {
                //se
                if (0 <= channel && channel < SE_CHANNEL)
                {
                    return _sourceSeArray[channel];
                }
                else
                {
                    return _sourceSeDefault;
                }
            }
        }
        //音源の読み込み
        public static void LoadBgm(string key,string NumberName)
        {
            GetInstance()._LoadBgm(key, NumberName);
        }
        public static void LoadSe(string key, string NumberName)
        {
            GetInstance()._LoadSe(key, NumberName);
        }
        void _LoadBgm(string key, string NumberName)
        {
            if (_poolBgm.ContainsKey(key))
            {
                _poolBgm.Remove(key);
            }
            _poolBgm.Add(key, new _Data(key, NumberName));
        }
        void _LoadSe(string key, string NumberName)
        {
            if (_poolBgm.ContainsKey(key))
            {
                _poolBgm.Remove(key);
            }
            _poolBgm.Add(key, new _Data(key, NumberName));
        }
        //BGM音源の再生
        public static bool PlayBgm(string key)
        {
            return GetInstance()._PlayBgm(key);
        }
        bool _PlayBgm(string key)
        {
            if (_poolBgm.ContainsKey(key) == false)
            {
                return false;
            }
            _StopBgm();
            var _data = _poolBgm[key];
            //再生
            var source = _GetAudioSource(Type.Bgm);
            source.loop = true;
            source.clip = _data.Clip;
            source.Play();
            return true;
        }
        public static bool StopBgm()
        {
            return GetInstance()._StopBgm();
        }
        bool _StopBgm()
        {
            _GetAudioSource(Type.Bgm).Stop();
            return true;
        }

        //SE音源の再生
        public static bool PlayBgm(string key,int channel=-1)
        {
            return GetInstance()._PlaySe(key);
        }
        bool _PlaySe(string key)
        {
            if (_poolBgm.ContainsKey(key) == false)
            {
                return false;
            }
            _StopSe();
            var _data = _poolBgm[key];
            //再生
            var source = _GetAudioSource(Type.Bgm);
            source.loop = true;
            source.clip = _data.Clip;
            source.Play();
            return true;
        }
        public static bool StopSe()
        {
            return GetInstance()._StopSe();
        }
        bool _StopSe()
        {
            _GetAudioSource(Type.Bgm).Stop();
            return true;
        }
    }

}
