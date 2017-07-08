using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform rotateYTransform;
    public Transform rotateXTransform;

    public float aimingSpeedScale = 0.5f;

    public float rotateSpeed = 1.5f;
    public float currentSpeed;
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    public Rigidbody rigidbody;
    public float currentRotateX = 0;

    public JumpSensor jumpSensor;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //決定鍵盤input的結果
        Vector3 movDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { movDirection.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movDirection.z -= 1; }
        if (Input.GetKey(KeyCode.D)) { movDirection.x += 1; }
        if (Input.GetKey(KeyCode.A)) { movDirection.x -= 1; }
        movDirection = movDirection.normalized;

        //以下將移動方向轉換成自己所面對的方向
        Vector3 worldSpaceDirection = movDirection.z * rotateYTransform.transform.forward +
                                      movDirection.x * rotateYTransform.transform.right;
        Vector3 velocity = this.rigidbody.velocity;
        velocity.x = worldSpaceDirection.x * moveSpeed;
        velocity.z = worldSpaceDirection.z * moveSpeed;

        if (Input.GetMouseButton(0)) //按下滑鼠時，進入蓄氣狀態，速度減緩
        {
            velocity.x *= aimingSpeedScale;
            velocity.z *= aimingSpeedScale;
        }
        if (Input.GetKey(KeyCode.Space) && jumpSensor.IsCanJump()) //判斷是否可跳躍
        {
            velocity.y = jumpSpeed;
        }
        rigidbody.velocity = velocity;

        this.rigidbody.velocity = velocity;

        //水平視角移動
        rotateYTransform.transform.localEulerAngles += new Vector3(0, Input.GetAxis("Horizontal"), 0) * rotateSpeed;

        //垂直視角移動，為避免物體翻過去
        Vector3 finalRotation = rotateXTransform.localEulerAngles;
        currentRotateX -= Input.GetAxis("Vertical") * rotateSpeed;

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
        rotateXTransform.localEulerAngles = finalRotation;
               
    }
}
