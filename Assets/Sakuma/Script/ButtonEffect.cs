using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{

    [SerializeField]
    private GameObject obj1;
    [SerializeField]
    private GameObject obj2;

    private Material mat1;
    private Material mat2;

    public float late;

    [SerializeField]
    private float max1;
    [SerializeField]
    private float max2;


    [SerializeField]
    private GameObject padobj;
    private PadController2 pad;

    [SerializeField]
    private GameObject stateobj;
    private StatusManager state;

    private Animator animator;

    [SerializeField]
    private GameObject ButtonObj;
    private Image button;

    [SerializeField]
    private Color32 On;
    [SerializeField]
    private Color32 Off;

    void Start()
    {
        late = 0;
        mat1 = obj1.GetComponent<Image>().material;
        mat2 = obj2.GetComponent<Image>().material;

        pad = padobj.GetComponent<PadController2>();
        state = stateobj.GetComponent<StatusManager>();

        animator = GetComponent<Animator>();

        button = ButtonObj.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        mat1.SetFloat("_Radius", late * max1);
        mat2.SetFloat("_Radius", late * max2);


        if(state.summonGage >=100&&!pad .sumonbd)
        {
            animator.SetBool("Set", true);
        }
        else
        {
            animator.SetBool("Set", false);
        }


        if(state.summonGage >= 100)
        {
            button.color = On;
        }
        else
        {
            button.color = Off;
        }
    }
}
