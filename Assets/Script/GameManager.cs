using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private bool IsCounting;
    private bool WasClicked;

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
            if (!WasClicked)
            {
                WasClicked = true;
                LaunchTimer();
            }
        }
    }
    private void Start()
    {
        GameManager.instance = this;
        currentTimer = 0f;
        IsCounting = false;
        WasClicked = false;
    }

    #region timer
    public void ResetTimer()
    {
        if (IsCounting)
        {
            StopCoroutine(TimerRoutine);
            currentTimer = 0f;
        }
        else
        {
            currentTimer = 0f;
            LaunchTimer();
        }
    }

    public void LaunchTimer()
    {

        TimerRoutine = StartCoroutine(timerRoutine());
        Debug.Log("CoroutineLauched");
    }

    IEnumerator timerRoutine()
    {
        IsCounting = true;
        while (TimerIsFinish())
        {
            Debug.Log("Timer = " + currentTimer);
            yield return new WaitForSecondsRealtime(0.1f);
            currentTimer += 0.1f;
            QuartTimer();

        }
        IsCounting = false;
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

    public float GetTurnTimer()
    {
        return TurnTimer;
    }
}
