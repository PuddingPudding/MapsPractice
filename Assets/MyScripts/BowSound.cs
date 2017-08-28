using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSound : MonoBehaviour {

    private AudioSource[] SoundArray;
    private AudioSource pullSound;
    private AudioSource pushSound;
    public BowUser bowUser;
    private bool CanPush = false;
    private float reloadBar = 0;

    // Use this for initialization
    void Start ()
    {
        SoundArray = this.GetComponents<AudioSource>();
        pullSound = SoundArray[0];
        pushSound = SoundArray[1];

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (reloadBar > 0)
        {
            reloadBar -= Time.deltaTime;
            return;
        } //如果正處於換箭狀態，直接就不做下面的事情了

        if (Input.GetMouseButton(0))
        {
            if (!pullSound.isPlaying)
            {
                pullSound.Play();
            }
            if(bowUser.getChargingBar() >= bowUser.ChargingValue)
            {
                CanPush = true;
                pullSound.Stop();
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            pullSound.Stop();
        }

        if(Input.GetMouseButtonUp(0) && CanPush)
        {
            pushSound.Play();
            CanPush = false;
            reloadBar = bowUser.ReloadValue;
        }
    }
}

