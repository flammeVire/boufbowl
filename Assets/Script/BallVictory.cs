using UnityEngine;
using TMPro;

public class BallVictory : MonoBehaviour
{
    public Scoring scoring;
    public TextMeshProUGUI textScoreRed;
    public TextMeshProUGUI textScoreBlue;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("ball"))
        {
            Debug.Log("Ball detecter");
            if (other.gameObject.CompareTag("RedGoal"))
            {
                Debug.Log("But marqué dans la zone ROUGE !");
                if (scoring != null)
                {
                    scoring.RedGetPoint();
                }
            }
            else if (other.gameObject.CompareTag("BlueGoal"))
            {
                Debug.Log("But marqué dans la zone BLEU !");
                if (scoring != null)
                {
                    scoring.BlueGetPoint();
                }
            }
        }
    }
}