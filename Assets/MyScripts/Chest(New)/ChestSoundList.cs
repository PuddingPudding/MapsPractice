using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSoundList : MonoBehaviour {

    private AudioSource[] SoundArray;

    // Use this for initialization
    void Start ()
    {
        SoundArray = this.GetComponents<AudioSource>();
    }

    // 0 打開寶箱聲
    // 1 拿取寶物聲

    public void playOpenSound()
    {
        SoundArray[0].Play();
    }

    public void playGetRewardSound()
    {
        SoundArray[1].Play();
    }
}
