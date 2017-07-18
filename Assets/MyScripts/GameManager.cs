using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameHintScript gameHintScript;
    public GameObject jumpHintTrigger;
    public GameObject chestHintTrigger;
    public GameObject key;
    public CollisionListScript keyCollsionList;
    public GameObject Enemy;
    public GameObject Door;
    public GameObject Player;
    public GameObject chest_open;
    float Lifetime;

    bool GetKeyEvent = true;

    // Use this for initialization
    void Start ()
    {
        Lifetime = gameHintScript.LifeTime + 0.5f;
        gameHintScript.StartText(); //產生開始字幕
        key.SetActive(false);
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

    }

}
