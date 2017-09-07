using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingSound : MonoBehaviour {

    public PlayerSoundList playerSoundList; //引入玩家音樂包
    public JumpSensor jumpSensor;
    private bool ReadToPlay = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if(!jumpSensor.IsCanJump())
        {
            ReadToPlay = true;
        }

        if(ReadToPlay && jumpSensor.IsCanJump())
        {
            playerSoundList.playLandingSound();
            ReadToPlay = false;
        }
		
	}
}
