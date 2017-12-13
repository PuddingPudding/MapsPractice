using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeartScript : MonoBehaviour
{

    public GameObject chains;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Change()
    {
        //以下稍作解釋，在Unity當中，用來鎖定鋼體移動或旋轉的東西(constraints)，其使用上採用位元運算方式
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        chains.SetActive(false);
    }
    public void Reduction()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        chains.SetActive(true);
    }
}
