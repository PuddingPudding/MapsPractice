using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject DeadMenu;
    public PlayerScript playerScript;
    public PlayerSoundList playerSoundList;

    //玩家擁有的道具區
    public bool hasChanger;
    public bool hasKeyA;
    public bool hasGoggle2F;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.CurrentHP <= 0)
        {
            this.GameOver();
        }
    }

    //用來秀出死亡後選單，在玩家死亡時被呼叫
    public void GameOver()
    {
        DeadMenu.SetActive(true);
        Cursor.visible = true; //顯示游標
    }

    public void RespawnPlayer() //按下重生後復活玩家
    {
        playerSoundList.playReviveSound();
        playerScript.PlayerRespawn();
        DeadMenu.SetActive(false);
        Cursor.visible = false;
    }
}
