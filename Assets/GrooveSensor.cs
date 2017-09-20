using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrooveSensor : MonoBehaviour {

    public GameObject Box;
    public GameObject MidDoor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Box)
        {
            MidDoor.GetComponent<OpenableDoor>().OpenDoorRotate();
        }
    }
}
