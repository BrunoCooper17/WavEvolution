using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Note : MonoBehaviour {

    private Animator noteAnimation;
    private AudioSource sound;
    public AudioClip clipGood;
    public AudioClip clipWrong;

    private bool bIsplayed = true;

    // Use this for initialization
    void Start () {
        // Animators
        noteAnimation = GetComponent<Animator>();

        // Songs
        sound = GetComponent<AudioSource>();
    }

    public void Init()
    {
        bIsplayed = false;
    }

    public void PlaySong(bool isGood)
    {
        if (isGood)
        {
            noteAnimation.Play("good");
            sound.clip = clipGood;
        } else
        {
            noteAnimation.Play("Bad");
            sound.clip = clipWrong;
        }

        sound.Play();
        bIsplayed = true;
    }

    public bool IsPlayed()
    {
        return bIsplayed;
    }
}
