using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemStage2 : MonoBehaviour
{
    public GameObject box;
    public CollisionListScript grooveTrigger;
    public CollisionListScript buttonTrigger;
    public ReductionButtonScript reductionButtonScript;
    public OpenableDoor midDoor;
    private bool midDoorHasOpen = false;

    //2F
    public CellDoorScript GateA;
    public CellDoorScript GateB;
    public CellDoorScript GateC;
    public CollisionListScript buttonTriggerA1;
    public ReductionButtonScript reductionButtonScriptA1;
    public CollisionListScript buttonTriggerA2;
    public ReductionButtonScript reductionButtonScriptA2;
    public CollisionListScript buttonTriggerB1;
    public ReductionButtonScript reductionButtonScriptB1;
    public CollisionListScript buttonTriggerB2;
    public ReductionButtonScript reductionButtonScriptB2;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (grooveTrigger.CollisionObjects.Count > 0)
        {
            if (grooveTrigger.CollisionObjects[0] == box.GetComponent<Collider>() && !midDoorHasOpen)
            {
                midDoor.OpenDoorRotate();
                midDoorHasOpen = true;
                Debug.Log("Open NOW!");
            }
        }

        if(buttonTrigger.CollisionObjects.Count > 0) //當玩家走到拉桿面前時
        {
            if (buttonTrigger.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                if (Input.GetKeyDown(KeyCode.R) && !midDoorHasOpen) //開門後開關無效
                {
                    Debug.Log("Za Warudo~!");
                    reductionButtonScript.RockerPress();
                    box.GetComponent<BoxChangeManager>().GetBack();
                }
            }
        }

        //F2_A1
        if(buttonTriggerA1.CollisionObjects.Count > 0)
        {
            if (buttonTriggerA1.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                if(Input.GetKeyDown(KeyCode.R))
                {
                    reductionButtonScriptA1.RockerPress();
                    GateA.CloseDoor();
                    GateB.OpenDoor();
                    GateC.CloseDoor();
                }
            }
        }

        //F2_A2
        if (buttonTriggerA2.CollisionObjects.Count > 0)
        {
            if (buttonTriggerA2.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    reductionButtonScriptA2.RockerPress();
                    GateA.CloseDoor();
                    GateB.CloseDoor();
                    GateC.OpenDoor();
                }
            }
        }

        //F2_B1
        if (buttonTriggerB1.CollisionObjects.Count > 0)
        {
            if (buttonTriggerB1.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    reductionButtonScriptB1.RockerPress();
                    GateA.OpenDoor();
                    GateB.OpenDoor();
                    GateC.CloseDoor();
                }
            }
        }

        //F2_B2
        if (buttonTriggerB2.CollisionObjects.Count > 0)
        {
            if (buttonTriggerB2.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    reductionButtonScriptB2.RockerPress();
                    GateA.CloseDoor();
                    GateB.OpenDoor();
                    GateC.OpenDoor();
                }
            }
        }

    }
}
