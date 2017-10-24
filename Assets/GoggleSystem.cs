using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoggleSystem : MonoBehaviour
{
    public TranslucentScript[] translucentEnemyList;
    public float goggleDuration = 4f; //濾鏡持續時間
    public bool goggleOn = false; //濾鏡開啟狀態與否
    public HightLight[] highLightEnemyList;
    //public GameObject

    public IllusionMinion[] illusionMinionList;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(turnOnGoggle('N'));
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(turnOnGoggle('B'));
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(turnOnGoggle('V'));
        }

    }
    //控制UAVuser 來開啟或關閉怪物影形
    public void translucentOn()
    {
        for(int i = 0;  i < translucentEnemyList.Length; i++)
        {
            if(translucentEnemyList[i] != null)
            {
                translucentEnemyList[i].BecomeTranslucent();
            }            
        }
    }
    public void translucentOff()
    {
        for (int i = 0; i < translucentEnemyList.Length; i++)
        {
            if (translucentEnemyList[i] != null)
            {
                translucentEnemyList[i].BecomeCanSee();
            }
        }
    }
    public void illusionOn()
    {
        for(int i = 0; i < illusionMinionList.Length; i++)
        {
            illusionMinionList[i].disguise(); //叫所有隱形怪偽裝
        }
    }
    public void illusionOff()
    {
        for (int i = 0; i < illusionMinionList.Length; i++)
        {
            illusionMinionList[i].showFake(); //叫所有隱形怪現出原形
        }
    }
    public void highLightOn()
    {
        for (int i = 0; i < highLightEnemyList.Length; i++)
        {
            if(highLightEnemyList[i] != null)
            {
                highLightEnemyList[i].BecomeHighLight();
            }
        }
    }
    public void highLightOff()
    {
        for (int i = 0; i < highLightEnemyList.Length; i++)
        {
            if (highLightEnemyList[i] != null)
            {
                highLightEnemyList[i].BecomeUnHighLight();
            }
        }
    }

    public IEnumerator turnOnGoggle(char inputKey)
    {
        if (!goggleOn)
        {
            goggleOn = true; //將濾鏡調成開啟狀態，避免其他濾鏡中途開起來
            if(inputKey == 'N')
            {
                this.translucentOff();
                yield return new WaitForSeconds(goggleDuration);
                this.translucentOn();
            }
            else if(inputKey == 'B')
            {
                this.illusionOff();
                yield return new WaitForSeconds(goggleDuration);
                this.illusionOn();
            }
            else if (inputKey == 'V')
            {
                this.highLightOn();
                yield return new WaitForSeconds(goggleDuration);
                this.highLightOff();
            }
            goggleOn = false;
        }
        yield return new WaitForSeconds(0);
    }
}
