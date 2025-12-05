using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowTimer : MonoBehaviour
{
    private float timer;
    private float tempTimer;
    private int intermediary;

    public TextMeshProUGUI timerDisplay;

   // public Image timeBar;
    // Update is called once per frame

    void Start()
    {
        timer = GameManager.instance.GetTurnTimer();
    }

    void Update()
    {
        tempTimer = GameManager.instance.GetTimer();
        intermediary = (int)tempTimer;
        timerDisplay.text = $"Timer : {timer - intermediary}";
       // timeBar.fillAmount = 1 - (tempTimer / timer);
    }
}
