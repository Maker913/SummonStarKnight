using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundModel : MonoBehaviour
{
    [SerializeField,Tooltip("音源のリスト")]
    protected List<AudioClip> audioClips;
}
