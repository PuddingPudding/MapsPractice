using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSystemStage2 : MonoBehaviour
{

    //Mid 
    public GameObject box;
    public CollisionListScript grooveTrigger; //凹槽的觸發區域
    public CollisionListScript buttonTrigger;
    public CollisionListScript reductionArea; //中間箱子可還原區域
    public Text reductionText;
    public ReductionButtonScript reductionButtonScript;
    public OpenableDoor midDoor;
    public CollisionListScript table2FTrigger;
    public CollisionListScript tableB1Trigger;
    public CollisionListScript tableMidTrigger;
    public GameObject tableMid;
    public GameObject goggle2F;
    public GameObject goggleB1;
    public GameObject goggleFinal;
    public GameObject gameEndDoor;
    private bool goggle2FExist = false;
    private bool goggleB1Exist = false;
    private bool midDoorHasOpen = false;

    //2F
    public MonsterSpawner monsterSpawner;
    private bool GetGoggle2FEventFlag = true;
    public GameObject Goggle2F; //2樓的濾鏡
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

    //結束關卡時會用到
    public Image endScreen;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (goggle2FExist && goggleB1Exist)
        {
            tableMid.GetComponent<TalkToPlayerScript>().textImage = "組 合";
            tableMid.GetComponent<TalkToPlayerScript>().Message = "三個濾鏡合在一起了!";
        }

        if (this.GetComponent<PlayerManager>().hasGoggleFinal)
        {
            goggleFinal.GetComponentInParent<BoxCollider>().enabled = false;
            goggleFinal.GetComponentInParent<TalkToPlayerScript>().enabled = false;
            gameEndDoor.GetComponent<TalkToPlayerScript>().textImage = "開 啟";
            gameEndDoor.GetComponent<TalkToPlayerScript>().Message = "下一關!";
        }

        if (table2FTrigger.CollisionObjects.Count > 0) //判斷tableB2
        {
            if (table2FTrigger.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                if (!goggle2FExist) //如果檯子上還沒出現寶箱
                {
                    if (this.GetComponent<PlayerManager>().hasGoggle2F)
                    {
                        goggle2F.GetComponentInParent<BoxCollider>().enabled = false;
                        goggle2F.GetComponentInParent<TalkToPlayerScript>().enabled = false;
                        goggle2F.SetActive(true);
                        goggle2FExist = true;
                    }
                }
            }
        }

        if (tableB1Trigger.CollisionObjects.Count > 0) //判斷tableB1
        {
            if (tableB1Trigger.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                if (!goggleB1Exist) //如果檯子上還沒出現寶箱
                {
                    if (this.GetComponent<PlayerManager>().hasGoggleB1)
                    {
                        goggleB1.GetComponentInParent<BoxCollider>().enabled = false;
                        goggleB1.GetComponentInParent<TalkToPlayerScript>().enabled = false;
                        goggleB1.SetActive(true);
                        goggleB1Exist = true;
                    }
                }
            }
        }

        if (tableMidTrigger.CollisionObjects.Count > 0) //判斷tableMid
        {
            if (tableMidTrigger.CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                if (goggle2FExist && goggleB1Exist)
                {
                    tableMid.GetComponent<TalkToPlayerScript>().textImage = "組 合";
                    tableMid.GetComponent<TalkToPlayerScript>().Message = "三個濾鏡合在一起了!";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        goggleFinal.SetActive(true);
                    }
                }
            }
        }

        if (gameEndDoor.GetComponent<CollisionListScript>().CollisionObjects.Count > 0) //判斷GameEndDoor
        {
            if (gameEndDoor.GetComponent<CollisionListScript>().CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                if (this.GetComponent<PlayerManager>().hasGoggleFinal)
                {
                    gameEndDoor.GetComponent<TalkToPlayerScript>().textImage = "開 啟";
                    gameEndDoor.GetComponent<TalkToPlayerScript>().Message = "下一關!";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        DOTween.To(() => endScreen.color, (x) => endScreen.color = x, Color.black, 3f).OnComplete(() =>
                        {
                            Application.LoadLevel("three");
                        }); ;
                    }
                }
            }
        }

        if (goggleFinal.GetComponent<RewardScript>().hasBeenTaken)
        {
            this.GetComponent<PlayerManager>().hasGoggleFinal = true;
        }

        if (grooveTrigger.CollisionObjects.Count > 0)
        {
            if (grooveTrigger.CollisionObjects[0] == box.GetComponent<Collider>() && !midDoorHasOpen)
            {
                midDoor.OpenDoorRotate();
                midDoorHasOpen = true;
                Debug.Log("Open NOW!");
            }
        }

        if (reductionArea.CollisionObjects.Count > 0) //如果再範圍內則顯示上一部選單
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

        if (buttonTrigger.CollisionObjects.Count > 0) //當玩家走到拉桿面前時
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GetGoggle2FEventFlag)
            {
                DoorOpenSystemA();
            }
            if (!GetGoggle2FEventFlag)
            {
                DoorOpenSystemB();
            }
        }

        if (Goggle2F.GetComponent<RewardScript>().hasBeenTaken && GetGoggle2FEventFlag) //撿取二樓零件後
        {
            this.GetComponent<PlayerManager>().hasGoggle2F = true; //玩家二樓護目鏡取得
            GetGoggle2FEvent();
        }
    }

    void GetGoggle2FEvent()
    {
        GateA.CloseDoor();
        GateB.CloseDoor();
        GateC.CloseDoor();
        GetGoggle2FEventFlag = false;
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
