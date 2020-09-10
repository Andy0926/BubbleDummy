using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevelTrigger1 : MonoBehaviour
{
    public GameObject disappearWall;
    // Start is called before the first frame update
    void Start()
    {
        disappearWall.SetActive(true);
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bubble"))
        {
            Debug.Log("trigger2");
            SoundManagerScript.PlaySound("Interact");
            disappearWall.SetActive(false);
            Destroy(collision.gameObject);
        }

    }
}
