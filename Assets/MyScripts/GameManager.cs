using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameHintScript gameHintScript;
    public GameObject jumpHintTrigger;
    public GameObject chestHintTrigger;
    public GameObject NVGHintTrigger;

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
    public ChangeSceneScript SceneManage;

    private bool tutorialFlag = true; //旗幟，表示現在正處於教學階段
    private bool NVGHintFlag = true; //用以表示夜視鏡是否教學過了，教完了之後會放下，變成false

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

        if(!chest.GetComponent<chestScript>().getChestExist() && NVGHintFlag) //如果寶箱內的寶物已被拿走
        {
            Enemy_outside.SetActive(true); //把那個隱形怪放出來
            NVGHintTrigger.SetActive(true); //打開夜視鏡教學觸發區，等一下玩家碰到時告訴玩家夜視鏡好處都有啥
        }

        if (NVGHintTrigger.GetComponent<CollisionListScript>().CollisionObjects.Count > 0 && NVGHintTrigger.active) //判斷sensor是否存在和裡面是否有人
        {
            if (NVGHintTrigger.GetComponent<CollisionListScript>().CollisionObjects[0].GetComponent<PlayerScript>() != null) //確認是否為玩家
            {
                gameHintScript.NVGHowToFight();
                NVGHintTrigger.SetActive(false); //關閉sensor的玩家感知功能
                NVGHintFlag = false;
            }
        }

        if(Enemy_outside.GetComponent<EnemyScript>().CurrentHP <= 0 && tutorialFlag)
        {
            gameHintScript.TutorialFinish(); //敵人被幹死了
            Invoke("EndTutorial", 5);
            tutorialFlag = false; //把訓練中的旗幟放下，代表訓練結束，也避免再次進到這個判斷式裡面
        }

        if (Player.GetComponent<PlayerScript>().CurrentHP <= 0)
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

    public void RespawnPlayer() //按下重生後復活玩家
    {
        Player.GetComponent<PlayerScript>().PlayerRespawn();
        DeadMenu.SetActive(false);
        Cursor.visible = false;
    }

    public void EndTutorial()
    {
        SceneManage.ChangeToScene("Menu");
    }
}
