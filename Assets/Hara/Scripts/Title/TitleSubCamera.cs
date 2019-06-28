using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSubCamera : MonoBehaviour
{
    private Camera stageCamera;
    public RenderTexture CameraRender { set { stageCamera.targetTexture = value; } }

    // Start is called before the first frame update
    void Start()
    {
        stageCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stageCamera != null)
        {
            
        }
    }
}
