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
    private Light light;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        light = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (this.hasBeenTriggered)
        //{
        //    spriteRenderer.material.color = triggeredColor;
        //}
        //else
        //{
        //    spriteRenderer.material.color = originColor;
        //}
    }

    public void setNormal()
    {
        DOTween.To(() => spriteRenderer.color, x => spriteRenderer.color = x, originColor, transformTime);
        hasBeenTriggered = false;
    }
    public void setTriggered()
    {
        if (this.isWorking)
        {
            DOTween.To(() => spriteRenderer.color, x => spriteRenderer.color = x, triggeredColor, transformTime).OnComplete(() =>
           {
               hasBeenTriggered = true;
           });    //利用Dotween方法，確保 "這個魔法陣啟動了沒" 這個布林值只在顏色轉換完後才會改變真假        
        }
        else
        {
            hasBeenTriggered = true; //因為就連為working的魔法陣在被踩到時也要設為hasBeenTriggered，上述邏輯無法動到，所以要額外做一個else分支
        }
    }
    //public void blink()//用來閃爍的函式
    //{
    //    if (!this.hasBeenTriggered && isWorking)
    //    {
    //        spriteRenderer.material.DOColor(blinkColor, blinkTime / 2).OnComplete(() =>
    //        {
    //            spriteRenderer.material.DOColor(originColor, blinkTime / 2);
    //        });
    //    }
    //}

    public void blink()//用來閃爍的函式
    {
        if (!this.hasBeenTriggered && isWorking)
        {
            light.color = blinkColor;
            light.DOIntensity(100, blinkTime / 2).OnComplete(() =>
            {
                light.DOIntensity(0, blinkTime / 2);
            });
        }
    }

    public void disappear()
    {
        spriteRenderer.material.DOColor(Color.clear, transformTime).OnComplete(() =>
       {
           Destroy(this.gameObject);
       });
    }
}
