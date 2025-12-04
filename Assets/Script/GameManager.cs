using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("TimerData")]
    [SerializeField] float TurnTimer;
    private float currentTimer;
    private Coroutine TimerRoutine;
    public event Action TimerChanged;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("InputPressed");
            LaunchTimer();
        }
    }
    private void Start()
    {
        GameManager.instance = this;
        currentTimer = 0f;
    }

    #region timer
    public void ResetTimer()
    {
        StopCoroutine(TimerRoutine);
        TurnTimer = 0f;
    }

    public void LaunchTimer()
    {

        TimerRoutine = StartCoroutine(timerRoutine());
        Debug.Log("CoroutineLauched");
    }

    IEnumerator timerRoutine()
    {

        while (TimerIsFinish())
        {
            Debug.Log("Timer = " + currentTimer);
            yield return new WaitForSecondsRealtime(0.1f);
            currentTimer += 0.1f;
            QuartTimer();

        }
    }

    void QuartTimer()
    {
        if(currentTimer >= TurnTimer / 4)
        {
            Debug.LogWarning("CameraShake");
            this.GetComponent<CameraController>().StartShake();
        }
    }

    public float GetTimer() { return currentTimer; }

    public bool TimerIsFinish()
    {
        if(TurnTimer >= currentTimer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion
}
