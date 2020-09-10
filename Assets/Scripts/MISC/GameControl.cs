using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject health1, health2, health3;
    public GameObject item1, item2, item3;

    public static int totalLife;
    public static int totalItem;

    // Start is called before the first frame update
    void Start()
    {
        totalLife = 3;
        totalItem = 0;
        health1.gameObject.SetActive(true);
        health2.gameObject.SetActive(true);
        health3.gameObject.SetActive(true);
        item1.gameObject.SetActive(false);
        item2.gameObject.SetActive(false);
        item3.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (totalLife)
        {
            case 3:
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(true);
                health3.gameObject.SetActive(true);
                break;
            case 2:
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(true);
                health3.gameObject.SetActive(false);
                break;
            case 1:
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(false);
                health3.gameObject.SetActive(false);
                break;
            case 0:
            case -1:
                health1.gameObject.SetActive(false);
                health2.gameObject.SetActive(false);
                health3.gameObject.SetActive(false);
                GameOverScript.gameIsOver=true;
                break;
        }
        switch (totalItem)
        {
            case 3:
                item1.gameObject.SetActive(true);
                item2.gameObject.SetActive(true);
                item3.gameObject.SetActive(true);
                break;
            case 2:
                item1.gameObject.SetActive(true);
                item2.gameObject.SetActive(true);
                item3.gameObject.SetActive(false);
                break;
            case 1:
                item1.gameObject.SetActive(true);
                item2.gameObject.SetActive(false);
                item3.gameObject.SetActive(false);
                break;
            case 0:
                item1.gameObject.SetActive(false);
                item2.gameObject.SetActive(false);
                item3.gameObject.SetActive(false);
                break;
        }
    }
}
