using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    [SerializeField]
    private GameObject controllerObject;

    private PadController controllerPas;


    [SerializeField]
    private GameObject cameraObject;

    private CameraController  cameraPas;


    private Animator animator;

    [SerializeField]
    private float  speed;

    public Rigidbody rb;

    private bool gameStop;
    // Start is called before the first frame update
    void Start()
    {
        gameStop = false;
        controllerPas = controllerObject.GetComponent<PadController>();
        cameraPas = cameraObject.GetComponent<CameraController>();
        this.animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        if (gameStop == false && controllerPas.gameStop)
        {
            this.animator.speed = 0;
        }
        if (gameStop&& controllerPas.gameStop==false )
        {
            this.animator.speed = 1;
        }

        gameStop = controllerPas.gameStop;


        if (controllerPas.moveFlg)
        {
            this.animator.SetBool("Next", true);
            

            rb.MovePosition(transform.position- new Vector3(
                speed * Time.deltaTime * Mathf.Cos((cameraPas.angle + ((controllerPas.angle * 180 / Mathf.PI) + 90)) / 180 * Mathf.PI) * -1 * Mathf.Sqrt((controllerPas.ControllerMoveX * controllerPas.ControllerMoveX) + (controllerPas.ControllerMoveY * controllerPas.ControllerMoveY)),
                0,
                speed * Time.deltaTime * Mathf.Sin((cameraPas.angle + ((controllerPas.angle * 180 / Mathf.PI) + 90)) / 180 * Mathf.PI) * -1 * Mathf.Sqrt((controllerPas.ControllerMoveX * controllerPas.ControllerMoveX) + (controllerPas.ControllerMoveY * controllerPas.ControllerMoveY))
                )
            );

            if (Mathf.Abs(transform.eulerAngles.y - (-1*((controllerPas.angle*180/Mathf.PI)-90))) > 0.1f)
            {
                transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(transform.eulerAngles.y,-1*(cameraPas.angle +(controllerPas.angle * 180 / Mathf.PI)), Time.deltaTime*10), 0f);
            }
        }
        else
        {
            this.animator.SetBool("Next", false);
        }
    }
}
