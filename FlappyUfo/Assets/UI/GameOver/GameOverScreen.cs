using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI PointScore;
    public TextMeshProUGUI BestScore;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.PlaySoundEffects(audioManager.deathSound);
    }
    public void RestartButton()
    {
        LoadAgain();
    }
    public void ShowGameOverScreen(int pointScore, int bestScore)
    {
        gameObject.SetActive(true);
        PointScore.text = $"{pointScore} points";
        BestScore.text = $"BEST SCORE:\n{bestScore}";
    }

    public void LoadAgain()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
