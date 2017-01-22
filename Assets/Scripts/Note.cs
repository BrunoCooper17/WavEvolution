using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Note : MonoBehaviour {

    public Animator noteAnimation;
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

        Init();
    }

    public void Init()
    {
        bIsplayed = false;
        if(noteAnimation)
            noteAnimation.Play("idle");
    }

    public void PlaySong(bool isGood)
    {
        if (isGood)
        {
            noteAnimation.Play("Good");
            sound.clip = clipGood;
        } else
        {
            //noteAnimation.Play("Bad");
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
