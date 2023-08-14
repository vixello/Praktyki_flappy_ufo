using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{
    public bool IsFinised = false;
    public GameOverScreen GameOverScreen;
    public Camera SceneCamera;
    [SerializeField] private TextMeshProUGUI playerScore;
    private void FixedUpdate()
    {
        if (IsFinised)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        // if current player score is better than all time best, update it
        string playerScorestring = playerScore.text;
        if (!PlayerPrefs.HasKey("Score")|| int.Parse(playerScorestring)> PlayerPrefs.GetInt("Score"))
        {
            PlayerPrefs.SetInt("Score", int.Parse(playerScorestring));
        }
        SceneCamera.GetComponent<CameraWork>().CameraFollowSpeed = 0f;
        GameOverScreen.ShowGameOverScreen( int.Parse(playerScorestring), PlayerPrefs.GetInt("Score"));
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}