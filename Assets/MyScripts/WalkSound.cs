using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    public PlayerSoundList playerSoundList; //引入玩家音樂包
    public Rigidbody rigidbody;
    public JumpSensor jumpSensor;
    private AudioSource walkSound; //存取音樂包的走路聲

    // Use this for initialization
    void Start()
    {
        walkSound = playerSoundList.WalkSound();
    }

    // Update is called once per frame
    void Update()
    {
        if(walkSound == null)
        {
            walkSound = playerSoundList.WalkSound();
        }
        if (rigidbody.velocity.magnitude > 3.5f && jumpSensor.IsCanJump() ) //判斷速度是否足夠以及是否站在地面
        {
            if (!walkSound.isPlaying) //如果沒有播放走路聲才會播放
            {
                walkSound.Play();
                Debug.Log("play");
            }

        }
        else
        {
            if(walkSound.isPlaying)
            {
                walkSound.Stop();
                Debug.Log("pause");
            }            
        }
    }
}
