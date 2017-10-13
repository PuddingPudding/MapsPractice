using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrayScript : MonoBehaviour
{
    //這個是MagicArrayScript，是用來製造魔法陣啟動效果的，他會儲存魔法陣的開關狀態
    //並提供兩個方法已供外界呼叫，分別是setNormal()跟setTriggered()
    //這程式碼只負責被呼叫，不負責執行
    public bool hasBeenTriggered = false; //表示這個魔法陣是否已被觸發過了
    public bool isWorking = true; //表示這個魔法陣是否是啟用的
    public Color originColor = Color.white;
    public Color triggeredColor = Color.red;
    public Color blinkColor = Color.yellow;
    public float transformTime = 1;
    public float blinkTime = 1;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //spriteRenderer = this.GetComponent<SpriteRenderer>();
        if (Input.GetKeyDown(KeyCode.I))
        {
            this.blink();
        }
        if(this.hasBeenTriggered)
        {
            spriteRenderer.color = triggeredColor;
        }
        else
        {
            spriteRenderer.color = originColor;
        }
    }

    public void setNormal()
    {
        if(this.hasBeenTriggered && isWorking)
        {
            spriteRenderer.material.DOColor(originColor, transformTime).OnComplete(() =>
            {
                hasBeenTriggered = false;
            });//利用Dotween方法，確保 "這個魔法陣啟動了沒" 這個布林值只在顏色轉換完後才會改變真假
        }        
    }
    public void setTriggered()
    {
        if(!this.hasBeenTriggered && isWorking)
        {
            spriteRenderer.material.DOColor(triggeredColor, transformTime).OnComplete(()=>
            {
                hasBeenTriggered = true;
            });            
        }        
    }
    public void blink()//用來閃爍的函式
    {
        if (!this.hasBeenTriggered && isWorking)
        {
            spriteRenderer.material.DOColor(blinkColor, blinkTime / 2).OnComplete(() =>
            {
                spriteRenderer.material.DOColor(originColor, blinkTime / 2);
            });
        }
    }
}
