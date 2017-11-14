using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMListScript : MonoBehaviour
{
    private AudioSource[] SoundArray;
    // Use this for initialization
    void Start()
    {
        SoundArray = this.GetComponents<AudioSource>();
    }

    //0恐怖音效

    // Update is called once per frame
    void Update()
    {

    }

    public void playHorrorBGM()
    {
        SoundArray[0].Play();
    }

    public AudioSource getHorrorBGM()
    {
        return SoundArray[0];
    }
}
