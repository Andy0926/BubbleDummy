using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverOverlay;
    // Start is called before the first frame update
    public static bool gameIsOver = false;

    void Start()
    {
        gameOverOverlay.SetActive(false);
    }

    void Update()
    {
        GameOver();
    }

    // Update is called once per frame
    void GameOver()
    {
        if (gameIsOver==true)
        {
            Debug.Log("gameover");
            gameOverOverlay.SetActive(true);
            Time.timeScale = 0f;
            //gameIsOver = false;
        }

    }

    public void TryAgain()
    {
        Debug.Log("TryAgain");
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1f;
        gameIsOver = false;
        GameControl.totalItem = 0;
        GameControl.totalLife = 3;
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
