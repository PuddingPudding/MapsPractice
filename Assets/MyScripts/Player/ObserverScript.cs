﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverScript : MonoBehaviour
{

    public float rotateSpeed = 1.5f;
    public float currentSpeed;
    public float moveSpeed;
    public Rigidbody rigidBody;
    public float currentRotateX = 0;

    // Use this for initialization
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { movDirection += this.transform.forward; }
        if (Input.GetKey(KeyCode.S)) { movDirection -= this.transform.forward; }
        if (Input.GetKey(KeyCode.D)) { movDirection += this.transform.right; }
        if (Input.GetKey(KeyCode.A)) { movDirection -= this.transform.right; }
        movDirection = movDirection.normalized;

        //if (movDirection.magnitude == 0)
        //{
        //    currentSpeed = 0;
        //}
        //else
        //{
        //    if (movDirection.z < 0)
        //    {
        //        currentSpeed = -moveSpeed;
        //    }
        //    else
        //    {
        //        currentSpeed = moveSpeed;
        //    }
        //}

        //Vector3 worldSpaceDirection = movDirection.z * this.transform.forward +
        //                              movDirection.x * this.transform.right;
        //Vector3 velocity = rigidBody.velocity;
        //velocity.x = Vector3.right *  currentSpeed;
        //velocity.z = worldSpaceDirection.z * moveSpeed;

        rigidBody.velocity = movDirection * moveSpeed;

        this.transform.localEulerAngles += new Vector3(0, Input.GetAxis("Horizontal"), 0) * rotateSpeed;
        Vector3 finalRotation = this.transform.localEulerAngles;
        currentRotateX -= Input.GetAxis("Vertical") * rotateSpeed; ;

        if (currentRotateX > 90)
        {
            //this.transform.eulerAngles = new Vector3(90, 0, 0);
            currentRotateX = 90;
        }
        if (currentRotateX < -90)
        {
            //this.transform.localEulerAngles.Set(-90, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
            currentRotateX = -90;
        }
        finalRotation.x = currentRotateX;
        this.transform.localEulerAngles = finalRotation;
    }
}
