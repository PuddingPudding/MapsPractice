using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public CollisionListScript PlayerSensor;
    public bool hasBeenTaken = false; //是否已被拿走了
    public PlayerSoundList playerSoundList;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles += new Vector3(0, 120 * Time.deltaTime, 0);
        if (PlayerSensor.CollisionObjects.Count > 0)
        {
            playerSoundList.playGetKeySound();
            this.gameObject.SetActive(false);
            this.hasBeenTaken = true;
        }
    }
}
