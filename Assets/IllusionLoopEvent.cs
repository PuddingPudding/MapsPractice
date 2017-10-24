using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionLoopEvent : MonoBehaviour
{
    public CollisionListScript rewindCheckPoint;
    public Collider rewindPoint;
    public float rewindTime = 4f;
    public GameObject player;
    //public GameObject allEnemy;
    public EnemyScript[] enemyList;
    public EnemyScript trueEnemy;
    private Vector3[] enemyOriginPosition;
    private bool combating = true; //敵人被清掉的時候此bool轉為false代表戰鬥結束
    private bool looping = true; //代表依然還在輪迴中，直到玩家殺死正確的敵人後才會停

    // Use this for initialization
    void Start()
    {
        enemyOriginPosition = new Vector3[enemyList.Length];
        for(int i = 0; i < enemyOriginPosition.Length; i++)
        {
            enemyOriginPosition[i] = enemyList[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemyList.Length && combating && looping; i++)
        {
            if(enemyList[i].CurrentHP <= 0) //當有其中一個敵人死掉時，殺掉他們全部
            {
                if(enemyList[i] == trueEnemy) //如果你殺掉的目標就是真正的敵人的話，把輪迴結束
                {
                    looping = false;
                }
                for(int j = 0;j<enemyList.Length;j++)
                {
                    enemyList[j].Hit(enemyList[j].CurrentHP);
                }
                combating = false;
            }
        }

        if(rewindCheckPoint.CollisionObjects.Count >= 1 && looping)
        {
            loopRestart();
        }
    }

    void loopRestart() //當玩家殺錯怪的時候，在他要離開的時候令一切回復(包含敵人回復原樣)
    {
        player.transform.DOMove(rewindPoint.transform.position, rewindTime).OnComplete(() =>
        {
            for (int i = 0; i < enemyList.Length; i++)
            {
                enemyList[i].gameObject.SetActive(true);
                enemyList[i].Respawn();
                enemyList[i].transform.position = enemyOriginPosition[i];
            }
            combating = true;
        });        
    }
}
