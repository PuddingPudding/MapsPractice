using System.Collections;
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

    public float aimingSpeedScale = 0.5f;

    private float ChargingBar = 0; //集氣條
    public float ChargingValue;
    private float ReloadBar = 0;
    public float ReloadValue = 1; //換箭時間

    public GameObject arrowCandidate;
    public GameObject bow;
    public GameObject arrowOnBow; //置於弓上的箭，用來在射箭後暫時關閉，製作出射出的效果

    // Use this for initialization
    void Start()
    {
        scaleTemp = target.transform.localScale; //取準星大小比例
    }

    private void Reset()
    {
        ChargingBar = 0;
        ReloadBar = 0;
        bowAnimator.enabled = true;
        target.transform.localScale = scaleTemp;
        bowAnimator.SetBool("Charging", false);
    }

    public void RefreshBow()
    {
        this.Reset();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ReloadBar > 0)
        {
            ReloadBar -= Time.deltaTime;
            return;
        } //如果正處於換箭狀態，直接就不做下面的事情了
        else if (!arrowOnBow.active)
        {
            arrowOnBow.SetActive(true);
        }

        //if(Input.GetKeyUp(KeyCode.R))
        //{
        //    this.Reset();
        //}

        if (Input.GetMouseButton(0)) //按下滑鼠時，進入蓄氣狀態，速度減緩
        {
            Vector3 aimingVelocity = rigidbody.velocity;
            aimingVelocity.x *= aimingSpeedScale;
            aimingVelocity.z *= aimingSpeedScale;
            rigidbody.velocity = aimingVelocity;
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
            if (ChargingBar >= ChargingValue)
            {
                GameObject newArrow = GameObject.Instantiate(arrowCandidate);
                ArrowScript arrow = newArrow.GetComponent<ArrowScript>();
                arrowOnBow.SetActive(false); //射出箭了以後，把弓上的箭模組調成關閉
                ReloadBar = ReloadValue;
                Debug.Log("ReloadBar = " + ReloadBar);

                arrow.transform.position = bow.transform.position;
                arrow.transform.rotation = bow.transform.rotation;
                arrow.InitAndShoot(bow.transform.forward);
                //射箭
            }
            ChargingBar = 0;
            bowAnimator.SetBool("Charging", false);
        }
        bowAnimator.SetFloat("Speed", this.rigidbody.velocity.magnitude);
    }

    public float getChargingBar()
    {
        return this.ChargingBar;
    }
}
