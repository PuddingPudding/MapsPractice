using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrayScript : MonoBehaviour
{
    //這個是MagicArrayScript，是用來製造魔法陣啟動效果的，他會儲存魔法陣的開關狀態
    //並提供兩個方法已供外界呼叫，分別是setNormal()跟setTriggered()
    //這程式碼只負責被呼叫，不負責執行
    public bool hasBeenTriggered = false;
    public Color originColor;
    public Color triggeredColor;
    public float transformTime = 1;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            this.setTriggered();
        }
    }

    public void setNormal()
    {
        if(this.hasBeenTriggered)
        {
            spriteRenderer.material.DOColor(originColor, transformTime).OnComplete(() =>
            {
                hasBeenTriggered = false;
            });//利用Dotween方法，確保 "這個魔法陣啟動了沒" 這個布林值只在顏色轉換完後才會改變真假
        }        
    }
    public void setTriggered()
    {
        if(!this.hasBeenTriggered)
        {
            spriteRenderer.material.DOColor(triggeredColor, transformTime).OnComplete(()=>
            {
                hasBeenTriggered = true;
            });            
        }        
    }
}
