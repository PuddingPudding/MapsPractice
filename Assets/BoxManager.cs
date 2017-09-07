using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public GameObject player;
    public float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
