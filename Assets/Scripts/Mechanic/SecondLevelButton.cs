using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button;
    void Start()
    {
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //GameControl.totalItem = 3;
        Debug.Log(GameControl.totalItem);
        if (GameControl.totalItem == 3)
        {
            button.SetActive(true);
        }
    }
}
