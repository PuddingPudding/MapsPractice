using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
        //決定鍵盤input的結果
        Vector3 movDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { movDirection.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movDirection.z -= 1; }
        if (Input.GetKey(KeyCode.D)) { movDirection.x += 1; }
        if (Input.GetKey(KeyCode.A)) { movDirection.x -= 1; }
        movDirection = movDirection.normalized;

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
