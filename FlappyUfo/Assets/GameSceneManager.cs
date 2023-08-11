using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsFinised = false;
    public GameOverScreen GameOverScreen;
    public Camera camera;
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
        string playerScorestring = playerScore.text;
        if (!PlayerPrefs.HasKey("Score")|| int.Parse(playerScorestring)> PlayerPrefs.GetInt("Score"))
        {
            PlayerPrefs.SetInt("Score", int.Parse(playerScorestring));
        }
        camera.GetComponent<CameraWork>().CameraFollowSpeed = 0f;
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