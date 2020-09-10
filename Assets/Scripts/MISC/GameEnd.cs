using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameEnd : MonoBehaviour
{
    public GameObject congratulationOverlay;
    private int nextSceneToLoad;

    public static bool congratulation = false;
    // Start is called before the first frame update
    void Start()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex - 2;
        congratulationOverlay.SetActive(false);
    }

    private void Update()
    {
        Congratulation();
    }
    public void Congratulation()
    {
        if (congratulation==true)
        {
            congratulationOverlay.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(nextSceneToLoad);
        Time.timeScale = 1f;
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
