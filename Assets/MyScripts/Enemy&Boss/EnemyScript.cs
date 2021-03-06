﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Animator animator;
    public CollisionListScript PlayerSensor;
    public CollisionListScript AttackSensor;

    private float MinimumHitPeriod = 1f;
    private float HitCounter = 0;
    public float CurrentHP = 100;
    public GameObject FollowTarget;
    public float MoveSpeed = 3f;
    public float AttackValue = 30f;

    private Rigidbody rigidbody;
    public EnemySoundList enemySoundList;

    public void AttackPlayer()
    {
        if (AttackSensor.CollisionObjects.Count > 0)
        {
            AttackSensor.CollisionObjects[0].SendMessage("Hit", AttackValue);
        }
    }


    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSensor.CollisionObjects.Count > 0)
        {
            FollowTarget = PlayerSensor.CollisionObjects[0].gameObject;
        }

        if (CurrentHP > 0 && HitCounter > 0)
        {
            HitCounter -= Time.deltaTime;
        }
        else if (CurrentHP > 0)
        {
            if (FollowTarget != null)
            {
                Vector3 lookAt = FollowTarget.gameObject.transform.position;
                lookAt.y = this.gameObject.transform.position.y; //讓敵人去面向要跟蹤的對象，但是y軸不要有所變動 (讓怪物永遠都站在地上)
                this.transform.LookAt(lookAt);
                animator.SetBool("Walk", true);
                if (AttackSensor.CollisionObjects.Count > 0)
                {
                    animator.SetBool("Attack", true);
                    animator.SetBool("Walk", false);
                    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                else
                {
                    animator.SetBool("Attack", false);
                    rigidbody.transform.position += this.transform.forward * MoveSpeed * Time.deltaTime;
                }                
            }
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    public void Hit(float value)
    {
        if (HitCounter <= 0)
        {
            enemySoundList.PlayHitSound();
            FollowTarget = GameObject.FindGameObjectWithTag("Player");
            HitCounter = MinimumHitPeriod;
            CurrentHP -= value;
            animator.SetFloat("HP", CurrentHP);
            animator.SetTrigger("Hit");
            if (CurrentHP <= 0)
            {
                enemySoundList.PlayDeadSound(); //死亡時撥放死亡聲
                BuryTheBody();
            }
        }
    }

    void BuryTheBody()  //把自己埋葬
    {
        this.GetComponent<Collider>().enabled = false;
        this.transform.DOMoveY(-0.2f, 1f).SetRelative(true).SetDelay(1).OnComplete(() =>
        {
            this.transform.DOMoveY(-0.2f, 1f).SetRelative(true).SetDelay(3).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        });
    }

    public void Respawn()
    {
        this.transform.DOKill(false); //結束所有該物件的transform上正執行的DOTween，後面的參數帶入false表示直接結束而不執行OnComplete
        this.CurrentHP = 100;
        this.animator.SetFloat("HP", CurrentHP);
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", false);
        this.GetComponent<Collider>().enabled = true;
    }

}
