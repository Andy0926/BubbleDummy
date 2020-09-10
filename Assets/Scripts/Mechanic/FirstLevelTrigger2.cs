using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelTrigger2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject disappearGround;

    void Start()
    {
        disappearGround.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bubble"))
        {
            Debug.Log("trigger1");
            disappearGround.SetActive(true);
            SoundManagerScript.PlaySound("Interact");
        }

    }
}
