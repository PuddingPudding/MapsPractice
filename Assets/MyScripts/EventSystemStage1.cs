using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSystemStage1 : MonoBehaviour
{
    public List<EnemyScript> enemyList; //用成List(C#動態陣列)才能夠直接以參照方式移除元素 註:enemyList.Remove(變數名稱)
    public List<ChestScript> chestListA;//需要keyA來打開的寶箱

    private bool enemyClearEventFlag = true; //旗標，用以表示清光敵人後的事件正在等待被觸發，觸發後把旗標放下
    //在第一關之中，走廊上的兩個敵人皆死亡後，鑰匙會落在最後一個敵人屍體的位子
    private bool stageClearEventFlag = true;//破關的旗標，在你撿完寶箱的內容物後觸發動畫並放下旗標

    private PlayerManager playerManager;
    public PlayerMovement playerMovement;
    public GameObject endDoor;
    public KeyScript keyA;
    public ChestRewardChanger chestRewardChanger; //寶箱獎勵(內容物:干擾器)
    public Image endScreen;
    public BGMListScript BGMList;
    private AudioSource horrorBGM;

    // Use this for initialization
    void Start()
    {
        playerManager = this.GetComponent<PlayerManager>();
        horrorBGM = BGMList.getHorrorBGM();
    }

    // Update is called once per frame
    void Update()
    {
        List<EnemyScript> enemyToRemove = new List<EnemyScript>(); 
        //這是foreach的細節之一，foreach的例外處理會希望你不要在走訪串列的過程中直接去移除或新增元素
        //所以我們要先用一個暫存的列表去記下哪些東西要移除
        foreach (EnemyScript enemy in enemyList)
        {
            if (enemy.CurrentHP <= 0) //當敵人死翹翹的時候
            {
                if (enemyList.Count == 1)//當我們要移除最後一個敵人時，把鑰匙傳送到屍體區域
                {
                    EnemyClearEvent(enemy); //殺死走廊上所有敵人後所觸發的事件，帶入的參數為最後死亡的敵人
                }
                enemyToRemove.Add(enemy); //先把要移除的東西存到一個列表之中
            }
        }
        foreach(EnemyScript enemy in enemyToRemove)//接著在依照這個列表去一一刪除原列表中的東西
        {
            enemyList.Remove(enemy);
        }

        foreach(ChestScript chest in chestListA)
        {
            if(chest.PlayerTouched() ) //檢查每個需要keyA之寶箱，當他被碰到時
            {
                if(playerManager.hasKeyA && !chest.isOpen) //檢查玩家是否有keyA且寶箱尚未開啟
                {
                    chest.OpenChest();
                }
            }
        }

        if(!chestRewardChanger.rewardExist) //若寶箱內的寶物被拿走了
        {
            stageClearEvent();//那本關便結束，撥放結束的效果
        }

        if(keyA.hasBeenTaken)
        {
            playerManager.hasKeyA = true;
        }
    }

    void EnemyClearEvent(EnemyScript enemy) //殺死走廊上所有敵人後所觸發的事件，帶入的參數為最後死亡的敵人
    {
        if (enemyClearEventFlag == true)
        {
            keyA.transform.position = new Vector3(enemy.transform.position.x, keyA.transform.position.y, enemy.transform.position.z);
            //把鑰匙位子座標的X和Z改成敵人屍體的X和Z
            keyA.gameObject.SetActive(true);
            keyA.transform.DOMoveY(keyA.transform.position.y - 1, 2); //兩秒內移到原本y座標-1的位子
            //DOMoveY(結束時的位置, 幾秒內完成);
            endDoor.GetComponent<AudioSource>().Play();
            endDoor.transform.DOMoveZ(endDoor.transform.position.z + 2.5f, 3).OnComplete(()=>
            {
                horrorBGM.Play();
            });
            
            enemyClearEventFlag = false; //把此事件的旗標放下，表示不會再觸發一次了
        }
    }

    void stageClearEvent()
    {
        if (stageClearEventFlag == true)
        {
            playerMovement.enabled = false;
            Vector3 lookAt = endDoor.transform.position + new Vector3(0,0,-2f);
            lookAt.y = playerManager.playerScript.transform.position.y;
            lookAt.z -= 2.5f;
            playerManager.playerScript.transform.DOLookAt(lookAt,3f).SetDelay(1); //把玩家的脊椎(observerRoot)調成 "看向牢房"
            playerMovement.transform.DOLookAt(lookAt, 3f).SetDelay(1).OnComplete(()=>
            {//以上把鏡頭拉過去看向牢房
                endDoor.GetComponent<AudioSource>().Play();
                endDoor.transform.DOMoveZ(endDoor.transform.position.z - 2.5f, 3).OnComplete(() =>
                {
                    playerManager.playerScript.transform.DOLocalMove(playerManager.playerScript.transform.position + playerManager.playerScript.transform.forward*2.5f , 1.5f).OnComplete(() =>
                    {
                        playerManager.playerScript.Hit(0);
                        DOTween.To(() => endScreen.color, (x) => endScreen.color = x, Color.black, 3f);
                        horrorBGM.DOFade(0f, 3f).SetDelay(2f).OnComplete(() =>//與上方黑頻同時並進，也就是完全黑頻後還會有2秒的微弱音樂
                        {
                            Application.LoadLevel("second");
                        }); 
                    });
                });
            });
            stageClearEventFlag = false;
        }
    }
}
