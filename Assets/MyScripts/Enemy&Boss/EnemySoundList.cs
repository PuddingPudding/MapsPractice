using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundList : MonoBehaviour {

    private AudioSource[] SoundArray;

    // Use this for initialization
    void Start () {
        SoundArray = this.GetComponents<AudioSource>();
    }
	
    // 0 受傷聲
    // 1 死亡聲

    public void PlayHitSound()
    {
        SoundArray[0].Play();
    }

    public void PlayDeadSound()
    {
        SoundArray[1].Play();
    }
}
