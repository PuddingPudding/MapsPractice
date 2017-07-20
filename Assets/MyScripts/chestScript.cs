using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chestScript : MonoBehaviour
{
    public CollisionListScript PlayerSensor;
    public bool isOpen = false;
    public GameObject openState;
    public GameObject closeState;
    public GameHintScript gameHintScript; //控制字幕程式碼
    private float time = 0;//計算觸發時間
    int FirstTrigger = 0; //被碰過的次數
    bool ChestExist = true; //判斷寶物是否被拿取了

    public int getFirstTrigger()
    {
        return this.FirstTrigger;
    }

    public void setFirstTrigger0()
    {
        FirstTrigger = 0;
    }
    
    public void setFirstTrigger1()
    {
        FirstTrigger = 1;
    }

    public bool getChestExist()
    {
        return ChestExist;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSensor.CollisionObjects.Count > 0)
        {
            if (PlayerSensor.CollisionObjects[0].GetComponent<PlayerScript>() != null) //確認是否為玩家
            {
                //下面這行為開箱判斷
                if (PlayerSensor.CollisionObjects[0].GetComponent<PlayerScript>().hasGoldKey == true && this.isOpen == false)
                {
                    this.isOpen = true;
                    this.openState.SetActive(true);
                    this.closeState.SetActive(false);
                    gameHintScript.OpenChest();
                }
                else if (time == 0 && this.isOpen == false) //在尚未打開前
                {
                    Invoke("setFirstTrigger1", 2); 
                    gameHintScript.NotFoundKey();
                    time = gameHintScript.LifeTime;
                }

                //當玩家在範圍內，按下G便可拿走寶物
                if(this.openState.active)
                {
                    Debug.Log("寶箱正處於開啟狀態");
                    if(Input.GetKey(KeyCode.G) && ChestExist) //寶箱內有寶物時才會進入
                    {
                        ChestExist = false;
                        openState.transform.Find("coins").gameObject.SetActive(false);
                        gameHintScript.TextDisappear();
                        gameHintScript.GetCoins();
                        PlayerSensor.CollisionObjects[0].GetComponent<NVGUser>().usable = true;
                        gameHintScript.Invoke("NVGHint", gameHintScript.LifeTime);
                    }
                }
            }
        }       

        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0;
        }
    }
}