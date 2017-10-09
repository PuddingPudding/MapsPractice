using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    public GameObject MonsterCandidate;
    public List<Transform> SpawnPoint;
    public GameObject initFollowTarget;
    private List<EnemyScriptVerDestory> EnemyList = new List<EnemyScriptVerDestory>();
    public int EnemyMax;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.ProduceMonster();
        }

        List<EnemyScriptVerDestory> enemyToRemove = new List<EnemyScriptVerDestory>();
        //這是foreach的細節之一，foreach的例外處理會希望你不要在走訪串列的過程中直接去移除或新增元素
        //所以我們要先用一個暫存的列表去記下哪些東西要移除
        foreach (EnemyScriptVerDestory enemy in EnemyList)
        {
            if(enemy.CurrentHP <= 0)
            {
                enemyToRemove.Add(enemy);
            }
        }
        foreach(EnemyScriptVerDestory enemy in enemyToRemove)
        {
            EnemyList.Remove(enemy);
        }
    }

    public void ProduceMonster()
    {
        if (EnemyList.Count < EnemyMax)
        {
            GameObject newMonster = GameObject.Instantiate(MonsterCandidate);
            EnemyList.Add(newMonster.GetComponent<EnemyScriptVerDestory>());
            newMonster.GetComponent<EnemyScriptVerDestory>().FollowTarget = initFollowTarget;
            newMonster.transform.position = SpawnPoint[Random.Range(0, SpawnPoint.Count)].position;
        }
    }
}
