using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    // Start is called before the first frame update

    //Object interaction
    public GameObject itemPick1, itemPick2, itemPick3;

    public GameObject lock1, lock2 ;

    public GameObject congratulationOverlay;

    public static bool key1 = false;
    public static bool key2 = false;

    private int nextSceneToLoad;

    private void Start()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        congratulationOverlay.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item1"))
        {
            Debug.Log("Item1Picked");
            itemPick1.SetActive(false);
            GameControl.totalItem += 1;
            SoundManagerScript.PlaySound("Pick");
        }
        if (collision.gameObject.CompareTag("Item2"))
        {
            Debug.Log("Item2Picked");
            itemPick2.SetActive(false);
            GameControl.totalItem += 1;
            SoundManagerScript.PlaySound("Pick");
        }
        if (collision.gameObject.CompareTag("Item3"))
        {
            Debug.Log("Item3Picked");
            itemPick3.SetActive(false);
            GameControl.totalItem += 1;
            SoundManagerScript.PlaySound("Pick");
        }
        if (collision.gameObject.CompareTag("Lock1"))
        {
            if (key1==true)
            {
                Debug.Log("DoorUnlocked");
                lock1.SetActive(false);
                SoundManagerScript.PlaySound("Interact");
            }          
        }

        if (collision.gameObject.CompareTag("Lock2"))
        {
            if (key2 == true)
            {
                Debug.Log("DoorUnlocked");
                lock2.SetActive(false);
                SoundManagerScript.PlaySound("Interact");
            }
        }

        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            //SceneManager.LoadScene(nextSceneToLoad);
            //GameControl.totalItem = 3;
            Debug.Log(GameControl.totalItem);
            if (GameControl.totalItem==3)
            {
                SceneManager.LoadScene(nextSceneToLoad);
            }
        }
        if (collision.gameObject.CompareTag("Princess"))
        {
            GameEnd.congratulation=true;
            Time.timeScale = 0f;
        }
    }
}
