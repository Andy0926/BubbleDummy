using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameControl.totalLife = 0;
            health = 0;
            SoundManagerScript.PlaySound("Hurt");
            Application.LoadLevel(Application.loadedLevel);
            Debug.Log("Die");
        }
    }
}
