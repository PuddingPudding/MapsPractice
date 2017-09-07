using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRewardChanger : MonoBehaviour
{
    ChestScript chestScript;
    public CollisionListScript PlayerSensor;
    public bool rewardExist = true; //寶箱內的獎勵品依然存在
    public GameObject changer;
    public PlayerManager playerManager;
    public ChestSoundList chestSoundList;
    public GameObject pressG;
    private bool pressGflag = false; //G按鈕提示的旗標，若為假表示沉著，尚未浮現
    private Vector3 pressGposition; //G按鈕提示的原始座標

    // Use this for initialization
    void Start()
    {
        chestScript = this.GetComponent<ChestScript>();
        pressG.SetActive(false);
        pressGposition = pressG.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSensor.CollisionObjects.Count > 0 )
        {
            if(this.rewardExist && chestScript.isOpen)//若寶物仍存在且寶箱已經開啟了
            {
                if(Input.GetKeyDown(KeyCode.G)) //此時按下了G鍵，讓玩家拿走干擾器
                {
                    changer.SetActive(false);
                    this.rewardExist = false;
                    playerManager.hasChanger = true;
                    chestSoundList.playGetRewardSound();
                    pressG.transform.DOLocalMoveY(pressGposition.y, 0.8f);
                    DOTween.To(() => pressG.GetComponent<SpriteRenderer>().color, (x) => pressG.GetComponent<SpriteRenderer>().color = x, Color.clear, 0.8f).OnComplete(() =>
                    {
                        pressG.SetActive(false);
                    });
                }
                if(!pressG.active && !pressGflag && rewardExist) //若提示的圖片尚未顯現，製作飄上來的特效
                {
                    pressGflag = true;
                    pressG.SetActive(true);
                    pressG.transform.localPosition = pressGposition;
                    pressG.GetComponent<SpriteRenderer>().color = Color.clear;
                    pressG.transform.DOLocalMoveY(pressGposition.y + 0.6f , 0.8f);
                    DOTween.To(() => pressG.GetComponent<SpriteRenderer>().color,(x) => pressG.GetComponent<SpriteRenderer>().color = x, Color.white , 0.8f);
                }
            }
        }
        else if(pressG.active && pressGflag)//當玩家不在感應範圍，且提示的圖片已經浮現，則讓其沉下去並消失
        {
            pressGflag = false;
            pressG.transform.DOLocalMoveY(pressGposition.y, 0.8f);
            DOTween.To(() => pressG.GetComponent<SpriteRenderer>().color, (x) => pressG.GetComponent<SpriteRenderer>().color = x, Color.clear, 0.8f).OnComplete(()=>
            {
                pressG.SetActive(false);
            });
        }
    }
}
