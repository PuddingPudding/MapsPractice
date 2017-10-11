using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrayEventSystem : MonoBehaviour
{
    public List<MagicArrayScript> magicArrayList = new List<MagicArrayScript>();
    public int activeArrayNum = 4; //有幾個魔法陣是要拿來啟動的
    private int[] activeArray; //隨機抓X個魔法陣，只把他們幾個enable=true
    private List<int> numArray = new List<int>(); //一個可以取出元素陣列，用此法做到每次取亂數時都不會有重複數字

    // Use this for initialization
    void Start()
    {
        initCeremony();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void initCeremony()
    {
        for(int i = 0;  i < magicArrayList.Count; i++)
        {
            magicArrayList[i].enabled = false;
            numArray.Add(i); //把所有魔法陣都先關掉，然後把數字依序塞入numArray
        }
        if(activeArrayNum > magicArrayList.Count) //避免等下取亂數決定哪些魔法陣啟動時不小心爆掉
        {
            activeArrayNum = magicArrayList.Count;
        }
        activeArray = new int[activeArrayNum];
        for(int i = 0 ; i < activeArrayNum; i++)
        {
            int numTemp = Random.Range(0, numArray.Count);
            activeArray[i] = numArray[numTemp];
            
            magicArrayList[activeArray[i]].enabled = true;
            numArray.RemoveAt(numTemp);
        }
    }
}
