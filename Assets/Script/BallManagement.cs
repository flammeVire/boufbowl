using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManagement : MonoBehaviour
{
    [SerializeField] PlayerBallDetection[] players;

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
    }

    void SetBallToPlayer(GameObject player)
    {
        transform.SetParent(player.transform);
    }
}
