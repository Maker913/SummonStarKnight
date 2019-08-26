using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VS : MonoBehaviour
{
    public void VSstart()
    {
        GetComponent<Animator>().SetBool("VSstart", true);
    }
}
