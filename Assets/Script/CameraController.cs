using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 initialPosition;
    private float shakeTimer;
    private bool isShaking;
    [SerializeField] Camera cam;

    void Start()
    {
        initialPosition = cam.transform.position;
        isShaking = false;
        
    }

    public void StartShake()
    {
        shakeTimer = 0f;
        isShaking = true;
    }

    private void Update()
    {
        if (isShaking)
        {
            shakeTimer += Time.deltaTime;
            cam.transform.position = initialPosition
                + new Vector3(
                    Random.Range(-0.06f, 0.06f),
                    Random.Range(-0.06f, 0.06f),
                    0f);
        }

        if (shakeTimer > 0.4f)
            isShaking = false;
    }
}
