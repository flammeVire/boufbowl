using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private bool IsCounting;
    private bool WasClicked;

    public bool canMove = true;
    private void Update()
    {
        /*
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("InputPressed");
            if (!WasClicked)
            {
                WasClicked = true;
                LaunchTimer();
            }
        }
        */
    }
    private void Start()
    {
        GameManager.instance = this;
        currentTimer = 0f;
        IsCounting = false;
        WasClicked = false;
        StartCoroutine(TurnManagement());
        LaunchTimer();
    }

    #region timer
    [Header("TimerData")]
    [SerializeField] float TurnTimer;
    private float currentTimer;
    private Coroutine TimerRoutine;
    public event Action TimerChanged;
    public void ResetTimer()
    {
        if (IsCounting)
        {
            StopCoroutine(TimerRoutine);
            currentTimer = 0f;
            Debug.Log("Reset : " + currentTimer);
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

            currentTimer += 0.1f;
           //Debug.Log("Timer = " + currentTimer);
            yield return new WaitForSecondsRealtime(0.1f);
            QuartTimer();
        }
        IsCounting = false;
    }

    void QuartTimer()
    {
        if(currentTimer >= TurnTimer * 0.75)
        {
            this.GetComponent<CameraController>().StartShake();
        }
    }

    public float GetTimer() { return currentTimer; }
    public float GetTurnTimer() {return TurnTimer;}

    public bool TimerIsFinish()
    {
        if(TurnTimer > currentTimer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion
    [SerializeField] IAManagement iaManagement;
    #region Turn
    [SerializeField] int MaxTurn;
    int CurrentTurn;

    void IncrementTurn()
    {
        CurrentTurn++;
    }

    public int GetCurrentTurn() { return CurrentTurn;}

    public bool AllPlayerHaveMoved = false;

    IEnumerator TurnManagement()
    {
        yield return new WaitUntil(() => TimerIsFinish() == false || AllPlayerHaveMoved);
        canMove = false;
        Debug.Log("Current turn : " + CurrentTurn);
        if (GameIsFinish())
        {
            Debug.Log("GameFinish");
        }
        else
        {
            IncrementTurn();
            AllPlayerHaveMoved = false;
            iaManagement.LaunchCoroutine();
            //ici on peut mettre les trucs avant le reset
            ResetTimer();
            yield return null;
            LaunchTimer();
            Debug.Log("Relunch turn");
            yield return null;
            StartCoroutine(TurnManagement());
            canMove = true;
        }
    }

    bool GameIsFinish()
    {
        if(CurrentTurn >= MaxTurn)
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
