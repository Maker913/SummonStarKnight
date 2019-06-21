using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffect : MonoBehaviour
{
    private static ParticleSystem particle;
    private Camera mainCamera = null;

    private void Awake()
    {
        if(particle == null)
        {
            particle = GetComponent<ParticleSystem>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // メインカメラを取得する
        if(mainCamera == null)
        {
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        // タップしたらエフェクトを再生
        if (Input.GetMouseButton(0))
        {
            var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition + mainCamera.transform.forward * 10);
            particle.transform.position = pos;
            //particle.Emit(5);
        }
    }
}
