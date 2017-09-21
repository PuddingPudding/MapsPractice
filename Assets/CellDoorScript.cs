using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDoorScript : MonoBehaviour
{

    //初始牢門狀態都是關閉才可以使用這個程式碼

    public float DoorMoveScale = 3.5f;
    public float DoorMoveTime = 3f;
    public bool OpenAtBeginning = false; //設定說這扇門的初始狀態是否為開啟
    Vector3 OpenDoorPosition; //紀錄開啟和關閉後的牢門位子
    Vector3 CloseDoorPosition;

    // Use this for initialization
    void Start()
    {
        OpenDoorPosition = this.transform.position + this.transform.forward * DoorMoveScale;
        CloseDoorPosition = this.transform.position;
        if(OpenAtBeginning)
        {
            this.transform.position = OpenDoorPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            this.OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            this.CloseDoor();
        }
    }

    void OpenDoor()
    {
        this.transform.DOMove(OpenDoorPosition, 3);
    }

    void CloseDoor()
    {
        this.transform.DOMove(CloseDoorPosition, 3);
    }
}
