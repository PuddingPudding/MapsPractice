using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrayEventSystem : MonoBehaviour
{
    public List<MagicArrayScript> magicArrayList = new List<MagicArrayScript>();
    public int activeArrayNum = 4; //有幾個魔法陣是要拿來啟動的
    public Camera lookAtArrayCamera; //用來在提示的時候切換成該camera並依序閃爍一次提示玩家
    public GameObject player;
    public CollisionListScript instructionCollisionList; //用來偵測玩家碰到了指示用魔法陣的觸發區
    public List<GameObject> disableWhenInstruct;
    public SacrificeEnemyScript sacrificeEnemy; //用來獻祭的敵人
    private Vector3 enemyOriginPosition; //用來記錄敵人一剛開始的位子
    private int[] activeArray; //隨機抓X個魔法陣，只把他們幾個的isWorking設為true
    private List<int> numArray = new List<int>(); //一個可以取出元素陣列，用此法做到每次取亂數時都不會有重複數字
    private bool isBlinking = false; //用以表示魔法陣列表正在依序閃爍著，開始閃的時候會先變true，閃玩變成false
    private List<int> compareActiveArray = new List<int>(); //玩家去觸發魔法陣時，將觸發的魔法陣依序存入此處，再拿來跟activeArray

    // Use this for initialization
    void Start()
    {
        enemyOriginPosition = sacrificeEnemy.transform.localPosition;
        initCeremony();
        lookAtArrayCamera.enabled = false;        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isBlinking)
        //{
        //    StartCoroutine(blinkArray());
        //}
        if(Input.GetKeyDown(KeyCode.F) && instructionCollisionList.CollisionObjects.Count > 0)
        {
            StartCoroutine(showBlinkingArray());
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            initCeremony();
        }
    }
    
    private void initCeremony() //開始和踩錯後觸發，會重新設置哪幾個魔法陣已被啟動
    {
        sacrificeEnemy.Reduction();
        sacrificeEnemy.transform.localPosition = enemyOriginPosition;
        numArray.Clear();
        for (int i = 0;  i < magicArrayList.Count; i++)
        {
            magicArrayList[i].isWorking = false;
            magicArrayList[i].setNormal();
            Debug.Log("MagicArrayList第" + i + "號正常");
            numArray.Add(i); //把所有魔法陣都先關掉，然後把數字依序塞入numArray
        }
        Debug.Log("MagicArrayList長度" + magicArrayList.Count);
        if(activeArrayNum > magicArrayList.Count) //避免等下取亂數決定哪些魔法陣啟動時不小心爆掉
        {
            activeArrayNum = magicArrayList.Count;
        }
        activeArray = new int[activeArrayNum];
        Debug.Log(activeArrayNum + "個魔法陣被啟動");
        for(int i = 0 ; i < activeArrayNum; i++)
        {
            int numTemp = Random.Range(0, numArray.Count);
            activeArray[i] = numArray[numTemp];

            magicArrayList[activeArray[i]].isWorking = true;
            numArray.RemoveAt(numTemp);
        }
        
    }

    IEnumerator showBlinkingArray()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Camera>().enabled = false;
        lookAtArrayCamera.enabled = true;
        isBlinking = true; //表示開始閃爍
        foreach(GameObject gameObject in disableWhenInstruct)
        {
            gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1);
        
        for (int i = 0; i < activeArray.Length && isBlinking; i++)
        {
            Debug.Log("即將錯誤");
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
}
