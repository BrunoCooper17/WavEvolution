using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempSounds : MonoBehaviour {

    public AudioClip audio1;
    public AudioClip audio2;
    public AudioClip audio3;
    public AudioClip audio4;

    public bool bA = false;
    public bool bB = false;
    public bool bC = false;
    public bool bD = false;

    public AudioSource source;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A) && !bA)
        {
            bA = true;
            source.clip = audio1;
            source.Play();
        }
        else if (Input.GetKeyUp(KeyCode.A) && bA)
        {
            bA = false;
        }

        if (Input.GetKeyDown(KeyCode.S) && !bB)
        {
            bB = true;

            source.clip = audio2;
            source.Play();
        }
        else if (Input.GetKeyUp(KeyCode.S) && bB)
        {
            bB = false;
        }

        if (Input.GetKeyDown(KeyCode.D) && !bC)
        {
            bC = true;
            source.clip = audio3;
            source.Play();
        }
        else if (Input.GetKeyUp(KeyCode.D) && bC)
        {
            bC = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && !bD)
        {
            bD = true;
            source.clip = audio4;
            source.Play();
        }
        else if (Input.GetKeyUp(KeyCode.F) && bD)
        {
            bD = false;
        }
    }
}
