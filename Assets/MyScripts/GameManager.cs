using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameHintScript gameHintScript;
    public GameObject jumpHintTrigger;
    public GameObject chestHintTrigger;
    public GameObject key;
    float Lifetime;

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
    }
}
