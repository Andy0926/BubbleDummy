using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key1 : MonoBehaviour
{
    //public bool keyState;
    public GameObject key;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Key1");
            key.SetActive(false);
            Interaction.key1 = true;
            SoundManagerScript.PlaySound("Pick");
        }

    }
}
