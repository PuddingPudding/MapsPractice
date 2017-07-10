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
    int FirstTrigger = 0;

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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSensor.CollisionObjects.Count > 0 && this.isOpen == false)
        {
            if (PlayerSensor.CollisionObjects[0].GetComponent<PlayerScript>() != null) //確認是否為玩家
            {
                if (PlayerSensor.CollisionObjects[0].GetComponent<PlayerScript>().hasGoldKey == true)
                {
                    this.isOpen = true;
                    this.openState.SetActive(true);
                    this.closeState.SetActive(false);
                    gameHintScript.OpenChest();
                }
                else if (time == 0)
                {
                    Invoke("setFirstTrigger1", 2); ;
                    gameHintScript.NotFoundKey();
                    time = gameHintScript.LifeTime;
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