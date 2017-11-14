using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerArrowScript : MonoBehaviour {

    public float FlyingSpeed;
    public float LifeTime;
    public Vector3 velocity = new Vector3(0, 0, 0);
    public ChangeSystem changeSystem;
    public ChangerUser changerUser;

    public void InitAndShoot(Vector3 Direction)
    {
        this.GetComponent<Rigidbody>().velocity = Direction * FlyingSpeed;

        //transform.eulerAngles -= new Vector3(90, 0, 0); //把弓箭轉成直線
    }
    void OnTriggerEnter(Collider other) //當干擾器碰到別的物件時
    {
        other.gameObject.SendMessage("Change", SendMessageOptions.DontRequireReceiver);//干擾該物件
        changeSystem.setChange(other.gameObject);//提醒changeSystem調整列表
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.transform.position = Vector3.zero;//把自己歸位
        changerUser.changerArrowCoolDown();//讓玩家的干擾器回復成準備狀態
    }
}
