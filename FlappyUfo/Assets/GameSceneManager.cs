using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsFinised = false;
    public Camera camera;
    private void FixedUpdate()
    {
        if (IsFinised)
        {
            camera.GetComponent<CameraWork>().CameraFollowSpeed = 0f;
            StartCoroutine(RepeatLevel());
        }
    }
    private void LoadLevelAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator RepeatLevel()
    {
        yield return new WaitForSeconds(2);

        LoadLevelAgain();
        yield return null;
    }
}