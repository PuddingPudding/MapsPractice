using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float HPMax = 1000;
    public float CurrentHP = 1000;
    public GameObject target;
    public GameObject fireBallCandidate;
    public GameObject[] fireBallPosList; //將生成火球的幾個位子記下，在這些空物件的地方生成火球
    public float fireBallGeneratePeriod = 1.5f;
    public float fireBallCounter = -4;
    private float MinimumHitPeriod = 1f;
    private float HitCounter = 0;
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(HitCounter >= 0)
        {
            HitCounter -= Time.deltaTime;
        }
        if(fireBallCounter <= fireBallGeneratePeriod)
        {
            fireBallCounter += Time.deltaTime;
        }
        else
        {
            fireBallCounter -= fireBallGeneratePeriod;
            GameObject newFireBall = GameObject.Instantiate(fireBallCandidate);
            int numTemp = Random.Range(0, fireBallPosList.Length);
            newFireBall.transform.position = fireBallPosList[numTemp].transform.position;
             newFireBall.GetComponent<FireBallScript>().InitAndShoot(this.target);
        }
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
