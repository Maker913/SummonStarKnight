using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    private GameObject padControllerObj;
    private PadController padController;

    [SerializeField]
    private GameObject Exit;

    private bool boardCast;
    private Animator animator;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        padController = padControllerObj.GetComponent<PadController>();
        boardCast = false;
        animator = GetComponent<Animator>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(boardCast ==false && padController.boardcast)
        {
            animator.SetBool("On", true);
            time = 0;
        }

        boardCast = padController.boardcast;


        if (boardCast && time > 0.5f)
        {
            Exit.gameObject.SetActive(true);
        }
    }
    void OnEnable()
    {
        padController = padControllerObj.GetComponent<PadController>();
        Exit.gameObject.SetActive(false);
        padController.boardcast = false;
    }

    public void BoardExit()
    {
        animator.SetBool("On",false);
        Exit.gameObject.SetActive(false);
        padController.boardcast = false;
    }

}
