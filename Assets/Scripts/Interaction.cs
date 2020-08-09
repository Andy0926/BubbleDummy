using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    // Start is called before the first frame update

    //Object interaction
    public GameObject itemPick1, itemPick2, itemPick3;

    public GameObject lock1;

    public static bool key1 = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item1"))
        {
            Debug.Log("Item1Picked");
            itemPick1.SetActive(false);
            GameControl.totalItem += 1;
        }
        if (collision.gameObject.CompareTag("Item2"))
        {
            Debug.Log("Item2Picked");
            itemPick2.SetActive(false);
            GameControl.totalItem += 1;
        }
        if (collision.gameObject.CompareTag("Item3"))
        {
            Debug.Log("Item3Picked");
            itemPick3.SetActive(false);
            GameControl.totalItem += 1;
        }
        if (collision.gameObject.CompareTag("Lock1"))
        {
            if (key1==true)
            {
                Debug.Log("DoorUnlocked");
                lock1.SetActive(false);
            }          
        }
    }
}
