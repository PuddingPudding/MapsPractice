using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingSound : MonoBehaviour {

    private AudioSource landingSound;
    public JumpSensor jumpSensor;
    private bool ReadToPlay = false;

    // Use this for initialization
    void Start () {
        landingSound = this.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if(!jumpSensor.IsCanJump())
        {
            ReadToPlay = true;
        }

        if(ReadToPlay && jumpSensor.IsCanJump())
        {
            landingSound.Play();
            ReadToPlay = false;
        }
		
	}
}
