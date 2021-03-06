﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip jumpSound, reflectSound, hitSound,
        hurtSound, fireSound, pickSound, interactSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("Jump");
        reflectSound = Resources.Load<AudioClip>("Reflect");
        hitSound = Resources.Load<AudioClip>("Hit");
        hurtSound = Resources.Load<AudioClip>("Hurt");
        fireSound = Resources.Load<AudioClip>("Fire");
        pickSound = Resources.Load<AudioClip>("ItemPick");
        interactSound = Resources.Load<AudioClip>("Interact");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip) {
            case "Jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "Reflect":
                audioSrc.PlayOneShot(reflectSound);
                break;
            case "Hit":
                audioSrc.PlayOneShot(hitSound);
                break;
            case "Hurt":
                audioSrc.PlayOneShot(hurtSound);
                break;
            case "Fire":
                audioSrc.PlayOneShot(fireSound);
                break;
            case "Pick":
                audioSrc.PlayOneShot(pickSound);
                break;
            case "Interact":
                audioSrc.PlayOneShot(interactSound);
                break;
        }
        return;
    }
}
