﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeEnemyScript : MonoBehaviour
{
    private Animator animator;
    public CollisionListScript PlayerSensor;
    public CollisionListScript AttackSensor;

    private float MinimumHitPeriod = 1f;
    private float HitCounter = 0;
    public float CurrentHP = 100;
    public float MaxHP = 100;
    public GameObject FollowTarget;
    public float MoveSpeed = 3f;
    public float AttackValue = 30f;

    private Rigidbody rigidbody;
    private bool readyForIdle = false; //準備閒置，當感應區裡沒有玩家，會把這個bool調成false，並Invoke幾秒後不再追蹤
    public float readyForIdleTime = 4f;
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
            readyForIdle = true;
        }
        else if (PlayerSensor.CollisionObjects.Count == 0 && readyForIdle == true)
        {
            readyForIdle = false;
            Invoke("ClearFollowTarget", 4);
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
                    rigidbody.velocity = this.transform.forward * MoveSpeed;
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
            readyForIdle = true;
            HitCounter = MinimumHitPeriod;
            CurrentHP -= value;
            animator.SetFloat("HP", CurrentHP);
            animator.SetTrigger("Hit");
            if (CurrentHP <= 0)
            {
                enemySoundList.PlayDeadSound(); //死亡時撥放死亡聲
                this.Reduction(); //並讓他回復到死亡狀態
            }
        }
    }

    public void Change() //獻祭用的敵人在受到干擾器影響後會變成戰鬥狀態，讓他的X軸與Z軸變成可動
    {
        this.transform.DOKill(false); //結束所有該物件的transform上正執行的DOTween，後面的參數帶入false表示直接結束而不執行OnComplete
        this.CurrentHP = 100;
        this.animator.SetFloat("HP", CurrentHP);
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", false);
        this.rigidbody.isKinematic = false;
    }
    public void Reduction() //獻祭用的敵人在受到復原後會變成死亡狀態(遊戲物件不消失)
    {
        this.CurrentHP = 0;
        this.animator.SetFloat("HP", CurrentHP);
        this.rigidbody.isKinematic = true; //在復原時，讓這個東西變成運動學狀態，別人推不動他
    }

    public void ClearFollowTarget()
    {
        this.FollowTarget = null;
        animator.SetBool("Walk", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.SendMessage("setTriggered",SendMessageOptions.DontRequireReceiver);
    }
}
