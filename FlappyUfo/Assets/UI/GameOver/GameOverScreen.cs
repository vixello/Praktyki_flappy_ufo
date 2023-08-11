using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI PointScore;
    public TextMeshProUGUI BestScore;
    public void ShowGameOverScreen(int pointScore, int bestScore)
    {
        gameObject.SetActive(true);
        PointScore.text = $"{pointScore} points";
        BestScore.text = $"BEST SCORE:\n{bestScore}";
    }
}
