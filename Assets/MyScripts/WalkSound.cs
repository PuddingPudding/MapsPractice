using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    private AudioSource walkSound;
    public Rigidbody rigidbody;
    public JumpSensor jumpSensor;

    // Use this for initialization
    void Start()
    {
        walkSound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.velocity.magnitude > 3.5f && jumpSensor.IsCanJump() )
        {
            if (!walkSound.isPlaying)
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
