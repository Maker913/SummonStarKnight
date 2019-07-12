using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    private AudioMixer audioMixer;
    public void SetMaster(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }
    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }
    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SE", volume);
    }
}
