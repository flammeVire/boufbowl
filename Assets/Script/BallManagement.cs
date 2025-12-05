using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManagement : MonoBehaviour
{
    [SerializeField] PlayerBallDetection[] players;
    [SerializeField] Scoring scoring;

    private void Start()
    {
        canScore = true;
    }
    void PlayerOnRange()
    {
        for(int i = 0; i < players.Length; i++)
        {
            if (players[i].TryToDetectBall())
            {
                SetBallToPlayer(players[i].gameObject);
            }
        }
    }

    private void Update()
    {
        PlayerOnRange();
        BallOnGoal();
    }

    void SetBallToPlayer(GameObject player)
    {
        transform.SetParent(player.transform);
    }
    bool canScore = true;
    void BallOnGoal()
    {
        if (this.gameObject.transform.position.x <= -9) //bleu
        {
            if (canScore)
            {
                canScore = false;
                scoring.RedGetPoint();
                MapManager.instance.ResetAllSpawn();
            }
        }
        else if (this.gameObject.transform.position.x >= 9) //red
        {
            if (canScore)
            {
                canScore = false;
                scoring.BlueGetPoint();
                MapManager.instance.ResetAllSpawn();
            }
        }
    }
}
