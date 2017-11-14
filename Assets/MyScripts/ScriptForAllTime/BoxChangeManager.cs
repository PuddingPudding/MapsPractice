using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxChangeManager : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public GameObject chains;
    private Vector3 originPosition;

    // Use this for initialization
    void Start()
    {
        originPosition = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Change()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
        chains.SetActive(false);
    }
    void Reduction()
    {
        this.GetComponent<Rigidbody>().isKinematic = true; //運動學開啟時，其他人不能碰撞他，但他可以碰撞其他人
        chains.SetActive(true);
    }
    public void GetBack()
    {
        this.transform.localPosition = originPosition;
    }

    void OnCollisionEnter(Collision other)
    {        
        if (this.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity.normalized * speed;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        this.GetComponent<Rigidbody>().velocity.Set(0, 0, 0);
    }
}
