using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float FlyingSpeed;
    public float LifeTime;
    public Vector3 velocity = new Vector3(0,0,0);

    public void InitAndShoot(Vector3 Direction)
    {
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.velocity = Direction * FlyingSpeed;
        Debug.Log(rigidbody.velocity);
        Debug.Log(this.GetComponent<Rigidbody>().velocity);

        Transform transform = this.GetComponent<Transform>();
        transform.eulerAngles -= new Vector3(90, 0, 0); //把弓箭轉成直線

        Invoke("KillYourself", LifeTime);
    }


    public void KillYourself()
    {
        GameObject.Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        KillYourself();
    }

}
