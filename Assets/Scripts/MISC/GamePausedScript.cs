using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GamePausedScript : MonoBehaviour
{
    public GameObject gamePausedOverlay;
    // Start is called before the first frame update
    void Start()
    {
        gamePausedOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePausedOverlay.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        gamePausedOverlay.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Debug.Log("TryAgain");
        SceneManager.LoadScene("First Stage");
        Time.timeScale = 1f;
        Player.health = 3;
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif

    }
}
