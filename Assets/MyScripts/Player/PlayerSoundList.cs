using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundList : MonoBehaviour
{
    private AudioSource[] SoundArray;

    // Use this for initialization
    void Start()
    {
        SoundArray = this.GetComponents<AudioSource>();
    }

    // 0 死亡聲
    // 1 復活聲
    // 2 落地聲
    // 3 走路聲
    // 4 受傷聲
    // 5 撿鑰匙聲

    public void playDeadSound()
    {
        SoundArray[0].Play();
    }

    public void playReviveSound()
    {
        SoundArray[1].Play();
    }

    public void playLandingSound()
    {
        SoundArray[2].Play();
    }

    public void playWalkSound()
    {
        SoundArray[3].Play();
    }

    public void playHitSound()
    {
        SoundArray[4].Play();
    }

    public void playGetKeySound()
    {
        SoundArray[5].Play();
    }

    public AudioSource WalkSound()
    {
        if(SoundArray == null)
        {
            SoundArray = this.GetComponents<AudioSource>();
            //每次走路時都會發生一次walkSound的null exception，雖不會讓遊戲崩潰
            //但為了不要讓警告再跳出來，我們才加了這一行
        }
         return SoundArray[3];
    }
}
