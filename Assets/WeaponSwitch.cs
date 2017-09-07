using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

    public GameObject Bow;
    public GameObject Changer;
    public BowUser bowUser;
    public ChangerUser changerUser;

    // Use this for initialization
    void Start () {
        //一開始玩家是使用弓箭 所以開啟弓箭程式碼 關閉轉換器
        Bow.SetActive(true);
        bowUser.enabled = true;
        Changer.SetActive(false);
        changerUser.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Bow.SetActive(true);
            bowUser.enabled = true;
            Changer.SetActive(false);
            changerUser.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Bow.SetActive(false);
            bowUser.enabled = false;
            Changer.SetActive(true);
            changerUser.enabled = true;
        }
    }
}
