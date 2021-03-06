﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{    
    public float CurrentHP = 100f;
    public float MaxHP = 100f;
    public Text HPText;
    public Image BloodBlur;
    public Image RespawnBlur;
    public float ImmueTimeMax = 3f;
    private float ImmueTime = 0;
    public PlayerSoundList playSoundList;
    public bool alive = true;

    // Use this for initialization
    void Start()
    {
        HPText.text = "HP: " + CurrentHP;
        BloodBlur.enabled = false; //血腥特效剛開頭時先關閉
        RespawnBlur.color = new Color(1, 1, 1, 0); //特別注意，重生特效得要設成透明，而非關閉
    }

    public void Hit(int value)
    {
        if(ImmueTime > 0 || !this.alive) { return; } //如果處於無敵狀態，則該函式直接結束
        //又或著玩家已經死亡時也會直接結束

        CurrentHP -= value;
        if(CurrentHP <= 0 && this.alive) //當生命值被扣到0以下，且我們還活著時，把玩家設定為死亡狀態
        {
            this.alive = false;
            playSoundList.playDeadSound();
            this.PlayerDie();            
        }
        else
        {
            playSoundList.playHitSound(); //播放受傷聲
            if (!BloodBlur.isActiveAndEnabled) //受傷的圖片平常是關閉的
            {
                BloodBlur.enabled = true;
                BloodBlur.color = new Color(1, 1, 1, 0.5f);
                DOTween.To(() => BloodBlur.color, (x) => BloodBlur.color = x, new Color(1, 1, 1, 0), 0.8f).OnComplete(() =>
                {
                    BloodBlur.enabled = false; //被打到的時候，在0.8秒內漸漸淡化血腥特效，結束後再次把圖片關閉
                }); //也因此，每0.8秒才會正常顯示一次血腥特效，避免被圍毆時畫面一直閃
            }
        }
        
        HPText.text = "HP: " + CurrentHP;
    }

    public void PlayerDie()
    {
        CurrentHP = 0;
        this.GetComponentInChildren<PlayerMovement>().enabled = false;
        this.GetComponentInChildren<BowUser>().enabled = false;
    }

    public void PlayerRespawn()
    {
        CurrentHP = MaxHP;
        this.GetComponentInChildren<PlayerMovement>().enabled = true;
        this.GetComponentInChildren<BowUser>().enabled = true;
        HPText.text = "HP: " + CurrentHP;

        ImmueTime = ImmueTimeMax;
        RespawnBlur.color = new Color(1, 1, 1, 1);
        DOTween.To(() => RespawnBlur.color, (x) => RespawnBlur.color = x, new Color(1, 1, 1, 0), ImmueTimeMax).OnComplete(() =>
        {
            ImmueTime = 0;
        });
        this.alive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}