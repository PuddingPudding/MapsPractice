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
                    reductionButtonScript.RockerDown();
                    reductionButtonScript.Invoke("RockerUp", 1f); //按鈕一秒後彈起來
                    box.GetComponent<BoxChangeManager>().GetBack();
                }
            }
        }

    }
}
