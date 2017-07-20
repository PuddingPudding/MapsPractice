using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NVGUser : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject NVG;
    public bool NVGenable = false; //開啟或關閉
    public bool usable = false; //可不可以用(是否取得道具)

    // Use this for initialization
    void Start()
    {
        NVG.SetActive(NVGenable);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.usable && Input.GetKeyDown(KeyCode.N))
        {
            NVGenable = !NVGenable;
            NVG.SetActive(NVGenable);

            if(NVGenable) //判斷怪物是否看的見
            {
                gameManager.NVG_On();
            }
            else
            {
                gameManager.NVG_Off();
            }
        }
    }
}
