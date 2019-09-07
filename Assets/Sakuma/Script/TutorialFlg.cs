using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFlg : MonoBehaviour
{
    static public bool SummonBefore = false;
    static public bool FastContact = false;
    static public bool FastAtk = false;
    static public bool GageMax = false;
    static public bool SummonOpen = false;
    static public bool FastGageStop = false;
    static public bool FastSummonMiss = false;
    static public bool CantAnyButton = false;
    static public bool CantButton = false;
    static public void TutorialReSet()
    {
        SummonBefore = false;
        FastContact = false;
        FastAtk = false;
        GageMax = false;
        SummonOpen = false;
        FastGageStop = false;
        FastSummonMiss = false;
        CantAnyButton = false;
        CantButton = false;
    }
}
