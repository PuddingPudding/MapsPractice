using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    public float flyingSpeed = 0.5f;
    public float lifeTime = 15;
    public float damageValue = 10;
    public GameObject followTarget;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitAndShoot(Vector3 Direction)
    {
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        Transform transform = this.GetComponent<Transform>();
        transform.LookAt(this.transform.position += Direction);
        //讓他看向自己的位置會啥都看不到，也就不會旋轉，但如果加上一個向量的話就能指定他面朝著該向量的方向了
        rigidbody.velocity = transform.forward * flyingSpeed;
        Invoke("KillYourself", lifeTime);
    }
    public void InitAndShoot(GameObject target)//第二種射擊函式的參數為目標物，帶入玩家後讓火球瞄準玩家飛過去
    {
        this.transform.LookAt(target.transform.position);
        Debug.Log(this.transform.forward);
        InitAndShoot(this.transform.forward);
    }
    public void KillYourself()
    {
        GameObject.Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerScript>() != null)//假如對方是玩家，裝有PlayerScript的話才會觸發傷害
        {//這麼做是為了避免誤傷怪物
            other.gameObject.SendMessage("Hit", damageValue, SendMessageOptions.DontRequireReceiver);
        }        
        KillYourself();
    }
}
