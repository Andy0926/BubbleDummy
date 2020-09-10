using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevelTrigger2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject finalDoor;
    // Start is called before the first frame update
    void Start()
    {
        finalDoor.SetActive(true);
    }

    // Update is called once per frame

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bubble"))
        {
            Debug.Log("trigger2");
            SoundManagerScript.PlaySound("Interact");
            finalDoor.SetActive(false);
            Destroy(collision.gameObject);
        }

    }
}
