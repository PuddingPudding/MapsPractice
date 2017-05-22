﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestScript : MonoBehaviour {

    public CollisionListScript PlayerSensor;
    public bool isOpen = false;
    public GameObject openState;
    public GameObject closeState;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerSensor.CollisionObjects.Count > 0 && this.isOpen == false)
        {
            this.isOpen = true;
            this.openState.SetActive(true);
            this.closeState.SetActive(false);
        }      
	}
}
