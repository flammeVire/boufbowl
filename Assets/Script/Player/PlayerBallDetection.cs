using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallDetection : MonoBehaviour
{
    public int DetectionRange = 1;
    [SerializeField] GameObject Ball;
    public bool TryToDetectBall()
    {
        if (Vector3.Distance(transform.position, Ball.transform.position) <= DetectionRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
       // Debug.Log("TryToDetectBall: " + TryToDetectBall());
    }
}
