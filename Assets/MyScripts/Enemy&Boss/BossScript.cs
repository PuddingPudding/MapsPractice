using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float HPMax = 300;
    public GameObject target;
    public GameObject fireBallCandidate;
    public GameObject[] fireBallPosList; //將生成火球的幾個位子記下，在這些空物件的地方生成火球
    public float fireBallGeneratePeriod = 1.5f;
    public float fireBallCounter = -4;
    public GameObject enemyCandidate;
    public GameObject[] enemyPosList; //生成敵人的幾個位子
    public float enemyGeneratePeriod = 2.5f;
    public float enemyCounter = -4;
    public GameObject fireCircleCandidate;
    public float fireCircleHight = 91.5f;
    public float fireCircleGeneratePeriod = 5f;
    public float fireCircleCounter = 0;

    private float CurrentHP = 300;
    private float MinimumHitPeriod = 1f;
    private float HitCounter = 0;


    // Use this for initialization
    void Start()
    {
        CurrentHP = HPMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (HitCounter >= 0)
        {
            HitCounter -= Time.deltaTime;
        }

        if(CurrentHP > 200 )
        {
            if (fireBallCounter <= fireBallGeneratePeriod)
            {
                fireBallCounter += Time.deltaTime;
            }
            else
            {
                fireBallCounter -= fireBallGeneratePeriod;
                this.fireBallSpawn();
            }
        }        
        else if(CurrentHP > 100)
        {
            if (enemyCounter <= enemyGeneratePeriod)
            {
                enemyCounter += Time.deltaTime;
            }
            else
            {
                enemyCounter -= enemyGeneratePeriod;
                this.enemySpawn();
            }
        }        
        else if(CurrentHP > 0)
        {
            if (fireCircleCounter <= fireCircleGeneratePeriod)
            {
                fireCircleCounter += Time.deltaTime;
            }
            else
            {
                fireCircleCounter -= fireCircleGeneratePeriod;
                this.fireCircleSpawn();
            }
        }
    }

    public void fireBallSpawn()
    {
        GameObject newFireBall = GameObject.Instantiate(fireBallCandidate);
        int numTemp = Random.Range(0, fireBallPosList.Length);
        newFireBall.transform.position = fireBallPosList[numTemp].transform.position;
        newFireBall.GetComponent<FireBallScript>().InitAndShoot(this.target);
    }

    public void enemySpawn()
    {
        for(int i = 0; i<enemyPosList.Length; i++)
        {
            GameObject newEnemy = GameObject.Instantiate(enemyCandidate);
            newEnemy.transform.position = enemyPosList[i].transform.position - new Vector3(0, 1.2f, 0);
            newEnemy.GetComponent<EnemyScriptVerDestory>().enabled = false;
            newEnemy.transform.DOMoveY(1.2f, 2f).SetRelative(true).OnComplete(() =>
            {
                newEnemy.GetComponent<EnemyScriptVerDestory>().enabled = true;
                newEnemy.GetComponent<EnemyScriptVerDestory>().FollowTarget = target;
            });
        }       
    }

    public void fireCircleSpawn()
    {
        GameObject newFireCircle = GameObject.Instantiate(fireCircleCandidate);
        newFireCircle.transform.position = new Vector3 (target.transform.position.x,fireCircleHight,target.transform.position.z);
    }

    public void Hit(float value)
    {
        if (HitCounter <= 0)
        {
            HitCounter = MinimumHitPeriod;
            CurrentHP -= value;
            if (CurrentHP <= 0)
            {
                BuryTheBody();
            }
        }
    }

    public void BuryTheBody()
    {
        this.GetComponent<Collider>().enabled = false;
        this.transform.DOMoveY(-2f, 1f).SetRelative(true).SetDelay(1).OnComplete(() =>
        {
            this.transform.DOMoveY(-2f, 1f).SetRelative(true).SetDelay(3).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        });
    }
}
