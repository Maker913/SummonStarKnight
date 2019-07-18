using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SummonGage : MonoBehaviour
{
    [SerializeField]
    private GameObject statusObj;
    private StatusManager statusManager;

    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        statusManager = statusObj.GetComponent<StatusManager>();
        material = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("_Gage", statusManager.summonGage /100);
    }
}
