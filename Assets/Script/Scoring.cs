using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    public TextMeshProUGUI blueScoreDisplay;
    public TextMeshProUGUI redScoreDisplay;

    public int blueScore = 0;
    public int redScore = 0;

    // Cette fonction update les scores dans la scene
    private void UpdateDisplay()
    {
        blueScoreDisplay.text = blueScore.ToString();
        redScoreDisplay.text = redScore.ToString();
    }

    // Cette fonction remet les 2 score à 0
    public void ResetScore()
    {
        blueScore = 0;
        redScore = 0;
        UpdateDisplay();
    }

    // Cette fonction Donne 1 point à l'équipe bleu
    public void BlueGetPoint()
    {
        blueScore += 1;
        UpdateDisplay();
    }

    // Cette fonction Donne 1 point à l'équipe rouge
    public void RedGetPoint()
    {
        redScore += 1;
        UpdateDisplay();
    }

    // retourne le score de l'équipe bleu sous forme d'INT
    public int GetBlueScore()
    {
        return blueScore;
    }

    // retourne le score de l'équipe rouge sous forme d'INT
    public int GetRedScore()
    {
        return redScore;
    }

    // retourne la difference de point entre le score de l'équipe bleu et rouge
        // si le nombre est positif, l'équipe bleu est en train de gagner
        // si le nombre est négatif, l'équipe rouge est en train de gagner
        // si le nombre est 0, les équipe sont ex aequo (à égalité)
    public int IsWinning()
    {
        return (blueScore - redScore);
    }
}
