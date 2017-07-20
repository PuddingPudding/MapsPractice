using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private Animator animator;
    public CollisionListScript PlayerSensor;

    private float MinimumHitPeriod = 1f;
    private float HitCounter = 0;
    public float CurrentHP = 100;
    public GameObject FollowTarget;
    public float MoveSpeed = 3f;

    private Rigidbody rigidbody;

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
                rigidbody.transform.position += this.transform.forward * MoveSpeed * Time.deltaTime;
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
            HitCounter = MinimumHitPeriod;
            CurrentHP -= value;
            animator.SetFloat("HP", CurrentHP);
            animator.SetTrigger("Hit");
            if (CurrentHP <= 0) { BuryTheBody(); }
        }
    }

    void BuryTheBody()
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

}
