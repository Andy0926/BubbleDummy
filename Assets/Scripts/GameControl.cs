using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject health, health1, health2;
    public static int totalLife;

    // Start is called before the first frame update
    void Start()
    {
        totalLife = 3;
        health.gameObject.SetActive(true);
        health1.gameObject.SetActive(true);
        health2.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (totalLife)
        {
            case 3:
                health.gameObject.SetActive(true);
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(true);
                break;
            case 2:
                health.gameObject.SetActive(true);
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(false);
                break;
            case 1:
                health.gameObject.SetActive(true);
                health1.gameObject.SetActive(false);
                health2.gameObject.SetActive(false);
                break;
            case 0:
                health.gameObject.SetActive(false);
                health1.gameObject.SetActive(false);
                health2.gameObject.SetActive(false);
                break;
        }
    }
}
