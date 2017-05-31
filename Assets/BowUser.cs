using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowUser : MonoBehaviour
{

    public Animator bowAnimator;
    private Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

           this.bowAnimator.SetFloat("speed", this.rigidbody.velocity.sqrMagnitude);


    }
}
