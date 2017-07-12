using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private Animator animator;

    private float MinimumHitPeriod = 1f;
    private float HitCounter = 0;
    public float CurrentHP = 100;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHP > 0 && HitCounter > 0)
        {
            HitCounter -= Time.deltaTime;
        }
    }

    public void Hit(float value)
    {
        if (HitCounter <= 0)
        {
            HitCounter = MinimumHitPeriod;
            CurrentHP -= value;
            animator.SetFloat("HP", CurrentHP);
            if (CurrentHP <= 0) { BuryTheBody(); }
        }
    }

    void BuryTheBody()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Collider>().enabled = false;
        this.transform.DOMoveY(-0.4f, 1f).SetRelative(true).SetDelay(1).OnComplete(() =>
        {
            this.transform.DOMoveY(-0.4f, 1f).SetRelative(true).SetDelay(3).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        });
    }

}
