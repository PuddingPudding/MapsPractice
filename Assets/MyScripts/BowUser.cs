﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowUser : MonoBehaviour
{
    public Animator bowAnimator;
    public Rigidbody rigidbody;

    public Image target;
    public float shrinkSpeed; //準星縮小的速度
    private Vector3 scaleTemp;

    private float ChargingBar = 0; //集氣條
    public float ChargingValue;

    public GameObject arrowCandidate;
    public GameObject bow;

    // Use this for initialization
    void Start()
    {
        scaleTemp = target.transform.localScale; //取準星大小比例
    }

    // Update is called once per frame
    void FixedUpdate()
    {           
        if (Input.GetMouseButton(0)) //按下滑鼠時，進入蓄氣狀態，速度減緩
        {
            if (target.transform.localScale.sqrMagnitude > 0.15f)
            {
                target.transform.localScale *= shrinkSpeed;
            }
            else
            {
                bowAnimator.enabled = false;
            }
            ChargingBar += Time.deltaTime;
            bowAnimator.SetBool("Charging", true);
        }
        else //放開滑鼠時，判斷放箭與否
        {
            bowAnimator.enabled = true;
            target.transform.localScale = scaleTemp;
            Debug.Log(ChargingBar);
            if (ChargingBar >= ChargingValue)
            {
                GameObject newArrow = GameObject.Instantiate(arrowCandidate);
                ArrowScript arrow = newArrow.GetComponent<ArrowScript>();

                arrow.transform.position = bow.transform.position;
                arrow.transform.rotation = bow.transform.rotation;
                arrow.InitAndShoot(bow.transform.forward);
                //射箭
            }            
            ChargingBar = 0;
            bowAnimator.SetBool("Charging", false);
        }
        bowAnimator.SetFloat("Speed", this.rigidbody.velocity.magnitude);   }
}