using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelTrigger1 : MonoBehaviour
{

    public GameObject trigger, disappearGround;

    void Start()
    {
        disappearGround.SetActive(true);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Load"))
        {
            Debug.Log("trigger1");
            SoundManagerScript.PlaySound("Interact");
            disappearGround.SetActive(false);
            trigger.SetActive(false);
        }

    }
}
