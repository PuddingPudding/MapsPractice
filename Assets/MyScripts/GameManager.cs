using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameHintScript gameHintScript;
    public GameObject jumpHintTrigger;
    public GameObject chestHintTrigger;
    public GameObject key;
    //public CollisionListScript keyCollsionList;
    public GameObject Enemy; //鑰匙地牢裡面的怪物
    public GameObject Door;
    public GameObject Player;
    public GameObject chest; //整個寶箱
    public GameObject chest_open;
    public GameObject NVGUser;
    public GameObject Enemy_outside; //在外面的怪物
    float Lifetime;

    bool GetKeyEvent = true;

    public GameObject DeadMenu;

    // Use this for initialization
    void Start ()
    {
        Lifetime = gameHintScript.LifeTime + 0.5f;
        gameHintScript.StartText(); //產生開始字幕
        key.SetActive(false);
        DeadMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Lifetime > 0)
        {
            Lifetime -= Time.deltaTime;
        }
        else if (Lifetime < 0)
        {
            gameHintScript.OpenImage(); //產生鍵盤和滑鼠圖案
            Lifetime = 0;
        }

        if (jumpHintTrigger.GetComponent<CollisionListScript>().CollisionObjects.Count > 0 && jumpHintTrigger.active) //判斷sensor是否存在和裡面是否有人
        {
            if (jumpHintTrigger.GetComponent<CollisionListScript>().CollisionObjects[0].GetComponent<PlayerScript>() != null) //確認是否為玩家
            {
                gameHintScript.JumpText();
                jumpHintTrigger.SetActive(false); //關閉sensor
            }
        }

        if(chestHintTrigger.GetComponent<chestScript>().getFirstTrigger() == 1)
        {
            gameHintScript.SearchKey();
            chestHintTrigger.GetComponent<chestScript>().setFirstTrigger0();
            key.SetActive(true);
        }

        if(Door.active && Enemy.GetComponent<EnemyScript>().CurrentHP <= 0)
        {
            Door.SetActive(false);
            gameHintScript.EnemyDestroy();
        }

        if(key.GetComponent<keyScript>().hasBeenTaken && !Enemy.active && GetKeyEvent)
        {
            gameHintScript.Invoke("EnemyAppear", 2);
            //gameHintScript.EnemyAppear();
            Door.SetActive(true);
            Enemy.SetActive(true);
            GetKeyEvent = false;
        }

        if (chest_open.active)
        {
            if (!chest_open.transform.Find("coins").gameObject.active)
            {
                Player.GetComponentInChildren<PlayerMovement>().GetChestSpeed();
            }
        }

        if(!chest.GetComponent<chestScript>().getChestExist())
        {
            Enemy_outside.SetActive(true);
        }

        if(Player.GetComponent<PlayerScript>().CurrentHP <= 0)
        {
            this.GameOver();
        }
    }

    //控制NVGUser 來開啟或關閉怪物影形
    public void NVG_On()
    {
        Debug.Log("變成可以看見");
        Enemy_outside.GetComponent<translucentScript>().BecomeCanSee();
    }

    public void NVG_Off()
    {
        Debug.Log("變成不能看見");
        Enemy_outside.GetComponent<translucentScript>().BecomeTranslucent();
    }

    //用來秀出死亡後選單，在玩家死亡時被呼叫
    public void GameOver()
    {
        DeadMenu.SetActive(true);
        Cursor.visible = true;
    }

    public void RespawnPlayer()
    {
        Player.GetComponent<PlayerScript>().PlayerRespawn();
        DeadMenu.SetActive(false);
        Cursor.visible = false;
    }
}
