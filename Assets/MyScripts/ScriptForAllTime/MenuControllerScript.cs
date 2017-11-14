using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControllerScript : MonoBehaviour {

    public PlayerMovement playerMovement;
    public GameObject Menu;
    bool MenuEnable = false;

    // Use this for initialization
    void Start ()
    {
        Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuEnable = !MenuEnable;
            playerMovement.enabled = !MenuEnable; //如果選單開啟則玩家無法行動(後面可考慮加入武器無法發射)
            Menu.SetActive(MenuEnable);
            Cursor.visible = MenuEnable;
        }
    }
}
