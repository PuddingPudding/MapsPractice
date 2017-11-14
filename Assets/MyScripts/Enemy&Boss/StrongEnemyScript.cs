using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemyScript : MonoBehaviour {

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
    private bool readyForIdle = false; //準備閒置，當感應區裡沒有玩家，會把這個bool調成false，並Invoke幾秒後不再追蹤
    public float readyForIdleTime = 4f;
    public EnemySoundList enemySoundList;

    public bool FirstAttack; //需要第一次攻擊才會動
    public bool CountingStart; //是否開始倒數
    public float StartTime; //怪物開始動起來時間
    public BoxCollider PureCollider;

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
        if(CountingStart && StartTime >= 0) //時間大於等於0和開始才做
        {
            StartTime -= Time.deltaTime;
        }

        if(StartTime < 0)
        {
            FirstAttack = true;
        }

        if (PlayerSensor.CollisionObjects.Count > 0)
        {
            FollowTarget = PlayerSensor.CollisionObjects[0].gameObject;
            readyForIdle = true;
        }

        if (CurrentHP > 0 && HitCounter > 0)
        {
            HitCounter -= Time.deltaTime;
        }
        else if (CurrentHP > 0)
        {
            if (FollowTarget != null)
            {
                if(FirstAttack)
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
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    public void Hit(float value)
    {
        FirstAttack = true;
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
                BuryTheBody();
            }
        }
    }

    void BuryTheBody()  //把自己埋葬
    {
        PureCollider.enabled = false; //防止怪物死掉後和其他東西碰撞
        this.GetComponent<Collider>().enabled = false;
        this.transform.DOMoveY(-0.2f, 1f).SetRelative(true).SetDelay(1).OnComplete(() =>
        {
            this.transform.DOMoveY(-2f, 1f).SetRelative(true).SetDelay(1).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        });
    }

    public void ClearFollowTarget()
    {
        this.FollowTarget = null;
        animator.SetBool("Walk", false);
    }

}
