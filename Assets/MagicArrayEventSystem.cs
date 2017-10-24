using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicArrayEventSystem : MonoBehaviour
{
    public MagicArrayScript middleMagicArray;
    public List<MagicArrayScript> magicArrayList = new List<MagicArrayScript>();
    public int activeArrayNum = 4; //有幾個魔法陣是要拿來啟動的
    public Camera lookAtArrayCamera; //用來在提示的時候切換成該camera並依序閃爍一次提示玩家
    public GameObject player;
    public CollisionListScript instructionCollisionList; //用來偵測玩家碰到了指示用魔法陣的觸發區
    public List<GameObject> disableWhenInstruct;
    public SacrificeEnemyScript sacrificeEnemy; //用來獻祭的敵人
    public Text messageDialog;
    public AudioSource restartSound;
    public GameObject goggleB1;
    public SpriteRenderer giantMagicArray; //巨大魔法陣，用來最後調量的
    private Vector3 enemyOriginPosition; //用來記錄敵人一剛開始的位子
    private int[] activeArray; //隨機抓X個魔法陣，只把他們幾個的isWorking設為true
    private List<int> numArray = new List<int>(); //一個可以取出元素陣列，用此法做到每次取亂數時都不會有重複數字
    private bool isBlinking = false; //用以表示魔法陣列表正在依序閃爍著，開始閃的時候會先變true，閃玩變成false
    private List<int> compareActiveArray = new List<int>(); //玩家去觸發魔法陣時，將觸發的魔法陣依序存入此處，再拿來跟activeArray
    private bool arrayClearFlag = false; //代表是否成功踩對了
    private bool arrayClearEventFlag = true; //當你成功踩對了後就會觸發事件，為確保只觸發一次所以使用該旗標

    // Use this for initialization
    void Start()
    {
        enemyOriginPosition = sacrificeEnemy.transform.localPosition;
        initCeremony();
        lookAtArrayCamera.enabled = false;
        goggleB1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && instructionCollisionList.CollisionObjects.Count > 0)
        {
            StartCoroutine(showBlinkingArray());
        }

        bool restartFlag = false; //記錄說是否需要重來
        bool mask = false; //以此mask來檢查activeArray裡頭的東西是真是假，在看到第一個F之後才會被啟用
        int checkingOne; //下面檢查是否過關或踩錯時所使用的檢索值變數

        for (int i = 0; i < activeArray.Length && !restartFlag; i++)
        { 
            //檢查魔法陣觸發的順序，只有可能是 TF TT FF，不可能是FT (True/False)
            //也就是說，只要我在看到了第一個F之後，接下來就不可以再看到T，否則就表示你沒有按照順序觸發
            checkingOne = activeArray[i];
            if (!magicArrayList[checkingOne].hasBeenTriggered && !mask)
            {
                mask = true;
            }
            restartFlag = (mask && magicArrayList[checkingOne].hasBeenTriggered);
            if (restartFlag) //如果在此發現踩錯順序而需重設的話，秀出踩錯訊息
            {
                StartCoroutine(setMessageForSec("搞錯順序! 可惡，儀式重來了。", 2));
            }
        }
        arrayClearFlag = !mask; //如果成功解開了，那所有陣列中的魔法陣一定都已trigger，那mask就不會有機會啟用，故以此檢測是否已解開

        for (int i = 0; i < numArray.Count && !restartFlag; i++)
        {//上面檢查完有啟動的魔法陣之後，檢查玩家是否有讓敵人誤踩未啟動的魔法陣
            checkingOne = numArray[i];
            restartFlag = (magicArrayList[checkingOne].hasBeenTriggered); //未被啟動的魔法陣不應該被敵人踩到，所以不會被trigger
            if (restartFlag) //如果在此發現踩錯而需重設的話，秀出踩錯訊息
            {
                StartCoroutine(setMessageForSec("踩錯了! 糟糕，儀式得重來。", 2));
            }
        }

        if (restartFlag) //假如今天restartFlag舉起(變為真)，代表該重設了
        {
            restartSound.Play();
            isBlinking = false; //先將閃爍中斷
            initCeremony();
        }
        else if(arrayClearFlag) //另外來看，假如成功解開了，啟動成功事件，將魔法陣移除
        {
            eventClear();
        }

        if(goggleB1.GetComponent<RewardScript>().hasBeenTaken)
        {
            this.GetComponent<PlayerManager>().hasGoggleB1 = true;
        }
    }

    private void initCeremony() //開始和踩錯後觸發，會重新設置哪幾個魔法陣已被啟動
    {
        sacrificeEnemy.Reduction();
        sacrificeEnemy.transform.localPosition = enemyOriginPosition;
        numArray.Clear();
        for (int i = 0; i < magicArrayList.Count; i++)
        {
            magicArrayList[i].isWorking = false;
            magicArrayList[i].setNormal();
            Debug.Log("MagicArrayList第" + i + "號正常");
            numArray.Add(i); //把所有魔法陣都先關掉，然後把數字依序塞入numArray
        }
        Debug.Log("MagicArrayList長度" + magicArrayList.Count);
        if (activeArrayNum > magicArrayList.Count) //避免等下取亂數決定哪些魔法陣啟動時不小心爆掉
        {
            activeArrayNum = magicArrayList.Count;
        }
        activeArray = new int[activeArrayNum];
        Debug.Log(activeArrayNum + "個魔法陣被啟動");
        for (int i = 0; i < activeArrayNum; i++)
        {
            int numTemp = Random.Range(0, numArray.Count);
            activeArray[i] = numArray[numTemp];

            magicArrayList[activeArray[i]].isWorking = true;
            numArray.RemoveAt(numTemp);
        }

    }

    private void eventClear()
    {
        if(arrayClearEventFlag)
        {
            sacrificeEnemy.Hit(sacrificeEnemy.CurrentHP);
            sacrificeEnemy.Reduction();
            sacrificeEnemy.enabled = false;
            middleMagicArray.disappear();
            for (int i = 0; i < magicArrayList.Count; i++)
            {
                magicArrayList[i].disappear();
            }
            DOTween.To(() => giantMagicArray.color, x => giantMagicArray.color = x, Color.red, 2f).OnComplete(() =>
            {
                goggleB1.SetActive(true);
                goggleB1.transform.DOMoveY(goggleB1.transform.position.y + 1.2f, 1.2f);
                //由於寶箱的y軸是躺著的，但我們要的效果是升起，故不使用localPosition而直接使用position
            });
            sacrificeEnemy.transform.DOMoveY(sacrificeEnemy.transform.position.y - 2 , 2f).SetDelay(1f).OnComplete(() =>
            {
                Destroy(sacrificeEnemy.gameObject);
            });

            arrayClearEventFlag = false; //事件結束後放下該旗標
        }        
    }

    IEnumerator showBlinkingArray()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Camera>().enabled = false;
        lookAtArrayCamera.enabled = true;
        isBlinking = true; //表示開始閃爍
        foreach (GameObject gameObject in disableWhenInstruct)
        {
            gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1);

        for (int i = 0; i < activeArray.Length && isBlinking; i++)
        {
            int blinkingOne = activeArray[i]; //以此代表第幾號魔法陣現在正在閃爍
            magicArrayList[blinkingOne].blink();
            for (float j = 0; j < magicArrayList[blinkingOne].blinkTime; j += Time.deltaTime)
            {
                //magicArrayList[blinkingOne].blinkTime用以確認目前正閃爍的那個魔法陣會閃多久
                //等完之後再讓下一個魔法陣繼續閃爍
                yield return 0;
            }
        }
        yield return new WaitForSeconds(1);

        foreach (GameObject gameObject in disableWhenInstruct)
        {
            gameObject.SetActive(true);
        }
        isBlinking = false;
        lookAtArrayCamera.enabled = false;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Camera>().enabled = true;
    }

    IEnumerator setMessageForSec(string message, float time)
    {
        messageDialog.text = message;

        yield return new WaitForSeconds(time);

        messageDialog.text = "";
    }
}
