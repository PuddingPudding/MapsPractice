﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    public GameObject Bow;
    public GameObject Changer;
    public BowUser bowUser;
    public ChangerUser changerUser;

    //控制現在拿的武器UI程式碼
    public WeaponUIScript BowUI;
    public WeaponUIScript ChangerUI;

    // Use this for initialization
    void Start()
    {
        //一開始玩家是使用弓箭 所以開啟弓箭程式碼 關閉轉換器
        Bow.SetActive(true);
        BowUI.OpenAmmoUI();
        bowUser.enabled = true;
        Changer.SetActive(false);
        ChangerUI.CloseAmmoUI();
        changerUser.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButton(0)) //假如玩家正處在按下左鍵的狀態時，不要讓它切換武器
        {//當你沒有按著左鍵時才會被允許進入下面的切換動作
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Bow.SetActive(true);
                BowUI.OpenAmmoUI();
                bowUser.enabled = true;
                bowUser.RefreshBow();
                Changer.SetActive(false);
                ChangerUI.CloseAmmoUI();
                changerUser.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Bow.SetActive(false);
                BowUI.CloseAmmoUI();
                bowUser.enabled = false;
                Changer.SetActive(true);
                ChangerUI.OpenAmmoUI();
                changerUser.enabled = true;
            }
        }
       
    }
}
