using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightLight : MonoBehaviour
{

    public Collider weakPointCollider;
    public GameObject weakPointMark;
    bool highlightFlag = false;

    // Use this for initialization
    void Start()
    {
        weakPointCollider.enabled = highlightFlag;
        weakPointMark.SetActive(highlightFlag);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BecomeHighLight()
    {
        weakPointCollider.enabled = true;
        weakPointMark.SetActive(true);
    }
    public void BecomeUnHighLight()
    {
        weakPointCollider.enabled = false;
        weakPointMark.SetActive(false);
    }
}
