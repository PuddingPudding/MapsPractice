using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemStage1 : MonoBehaviour
{
    public List<EnemyScript> enemyList; //用成List(C#動態陣列)才能夠直接以參照方式移除元素 註:enemyList.Remove(變數名稱)
    private bool keyAppearSwitch = false; //鑰匙顯現的開關，false表示鑰匙還沒掉出來
    //在第一關之中，走廊上的兩個敵人皆死亡後，鑰匙會落在最後一個敵人屍體的位子

    public keyScript goldKey;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach(EnemyScript enemy in enemyList)
        {
            if(enemy.CurrentHP <= 0) //當敵人死翹翹的時候
            {
                if(enemyList.Count == 1 && !keyAppearSwitch)//當我們要移除最後一個敵人時，把鑰匙傳送到屍體區域
                {
                    goldKey.transform.position = new Vector3(enemy.transform.position.x , goldKey.transform.position.y , enemy.transform.position.z);
                    //把鑰匙位子座標的X和Z改成敵人屍體的X和Z
                    goldKey.gameObject.SetActive(true);
                    keyAppearSwitch = true; //開啟，表示鑰匙已顯現
                    goldKey.transform.DOMoveY(goldKey.transform.position.y -1 , 2); //兩秒內移到原本y座標-1的位子
                    //DOMoveY(結束時的位置, 幾秒內完成);
                }
                enemyList.Remove(enemy);
            }
        }
    }
}
