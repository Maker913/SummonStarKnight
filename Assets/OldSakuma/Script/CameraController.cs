using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float angle = 0;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float height;
    [SerializeField]
    private float PadDown;
    [SerializeField]
    private GameObject pivotPosition;

    [SerializeField]
    private GameObject controllerObject;

    private PadController controllerPas;
    void Start()
    {
        controllerPas = controllerObject.GetComponent<PadController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Cos(angle / 180 * Mathf.PI) * distance + pivotPosition.transform.position.x,
            pivotPosition.transform.position.y + height+PadDown,
            Mathf.Sin(angle / 180 * Mathf.PI) * distance + pivotPosition.transform.position.z);

        this.transform.rotation = Quaternion.Euler(Mathf.Atan(height /distance)*180/Mathf.PI, -1*(angle+90), transform.rotation.z);
        //transform.Rotate( new Vector3(transform.rotation.x, angle, transform.rotation.z));
        //Debug.Log((Mathf.Atan(height / distance) * 180 / Mathf.PI));
        if (Input.GetKey(KeyCode.DownArrow))
        {

            angle++;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            angle--;

        }

        if(controllerPas.cameraangle != 0)
        {
            angle += (180 * controllerPas.cameraangle) / (Mathf.PI *distance *100);
        }





    }
}
