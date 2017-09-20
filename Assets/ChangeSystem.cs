using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSystem : MonoBehaviour
{
    public GameObject[] changableList;
    public GameObject changeNow = null;
    public GameObject changeTemp = null;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setChange(GameObject input)
    {
        changeNow = input;
        bool canRefresh = false; //以此法檢查是否可以進行刷新，每當你干擾了一個新東西，讓其他物品都回復
        //因為當干擾器碰到任何東西時，他都會提醒ChangeSystem，所以需要先用下面這個迴圈確認是否為可干擾之物
        for (int i = 0; i < changableList.Length && !canRefresh; i++) 
        {
            if (input == changableList[i])
            {
                canRefresh = true;
            }
        }
        //當我們確認了干擾器碰到的東西為可以干擾之物後，呼叫刷新函式，把所有其他物件回復成正常狀態
        for (int i = 0; i < changableList.Length && canRefresh; i++)
        {
            if(input != changableList[i])
            {
                changableList[i].SendMessage("Reduction" , SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
