using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSystemStage2 : MonoBehaviour
{

    //Mid 
    public GameObject box;
    public CollisionListScript grooveTrigger;
    public CollisionListScript buttonTrigger;
    public CollisionListScript reductionArea; //中間箱子可還原區域
    public Text reductionText;
    public ReductionButtonScript reductionButtonScript;
    public OpenableDoor midDoor;
    private bool midDoorHasOpen = false;

    //2F
    public MonsterSpawner monsterSpawner;
    private bool GetGoggleAEventFlag = true;
    public GameObject GoggleA;
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
    public CollisionListScript buttonTriggerC1;
    public ReductionButtonScript reductionButtonScriptC1;
    public CollisionListScript buttonTriggerC2;
    public ReductionButtonScript reductionButtonScriptC2;


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

        if(reductionArea.CollisionObjects.Count > 0) //如果再範圍內則顯示上一部選單
        {
            if (reductionArea.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                reductionText.text = "返回上一步";
            }
        }
        else //離開區域則不顯示
        {
            reductionText.text = null;
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

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(GetGoggleAEventFlag)
            {
                DoorOpenSystemA();
            }
            if (!GetGoggleAEventFlag)
            {
                DoorOpenSystemB();
            }
        }

        if(GoggleA.GetComponent<KeyScript>().hasBeenTaken && GetGoggleAEventFlag) //撿取二樓零件後
        {
            GetGoggleAEvent();
        }
    }

    void GetGoggleAEvent()
    {
        GateA.CloseDoor();
        GateB.CloseDoor();
        GateC.CloseDoor();
        GetGoggleAEventFlag = false;
    }

    public void DoorOpenSystemA() //二樓手把模式1
    {
        //F2_A1
        if (buttonTriggerA1.CollisionObjects.Count > 0)
        {
            if (buttonTriggerA1.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                reductionButtonScriptA1.RockerPress();
                GateA.CloseDoor();
                GateB.OpenDoor();
                GateC.CloseDoor();
            }
        }

        //F2_A2
        if (buttonTriggerA2.CollisionObjects.Count > 0)
        {
            if (buttonTriggerA2.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {     
                reductionButtonScriptA2.RockerPress();
                GateA.CloseDoor();
                GateB.CloseDoor();
                GateC.OpenDoor();
            }
        }

        //F2_B1
        if (buttonTriggerB1.CollisionObjects.Count > 0)
        {
            if (buttonTriggerB1.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                reductionButtonScriptB1.RockerPress();
                GateA.OpenDoor();
                GateB.OpenDoor();
                GateC.CloseDoor();
            }
        }

        //F2_B2
        if (buttonTriggerB2.CollisionObjects.Count > 0)
        {
            if (buttonTriggerB2.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                reductionButtonScriptB2.RockerPress();
                GateA.CloseDoor();
                GateB.OpenDoor();
                GateC.OpenDoor();
            }
        }
    }

    public void DoorOpenSystemB()//二樓手把模式2
    {
        //F2_A1
        if (buttonTriggerA1.CollisionObjects.Count > 0)
        {
            if (buttonTriggerA1.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                reductionButtonScriptA1.RockerPress();
                GateB.OpenDoor();
            }
        }

        //F2_A2
        if (buttonTriggerA2.CollisionObjects.Count > 0)
        {
            if (buttonTriggerA2.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                reductionButtonScriptA2.RockerPress();
                GateB.CloseDoor();
                GateC.OpenDoor();
            }
        }

        //F2_B1
        if (buttonTriggerB1.CollisionObjects.Count > 0)
        {
            if (buttonTriggerB1.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                reductionButtonScriptB1.RockerPress();
                GateA.CloseDoor();
                GateB.OpenDoor();
                GateC.CloseDoor();
            }
        }

        //F2_B2
        if (buttonTriggerB2.CollisionObjects.Count > 0)
        {
            if (buttonTriggerB2.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                reductionButtonScriptB2.RockerPress();
                monsterSpawner.ProduceMonster();
            }
        }

        //F2_C1
        if (buttonTriggerC1.CollisionObjects.Count > 0)
        {
            if (buttonTriggerC1.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                reductionButtonScriptC1.RockerPress();
                GateA.OpenDoor();
            }
        }

        //F2_C2
        if (buttonTriggerC2.CollisionObjects.Count > 0)
        {
            if (buttonTriggerC2.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                reductionButtonScriptC2.RockerPress();
                GateB.CloseDoor();
                GateC.OpenDoor();
            }
        }
    }

}
