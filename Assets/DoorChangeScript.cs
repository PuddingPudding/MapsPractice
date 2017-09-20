using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChangeScript : MonoBehaviour
{
    public GameObject originForm;
    public GameObject changedForm;

    // Use this for initialization
    void Start()
    {
        originForm.SetActive(true);
        changedForm.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Change()
    {
        originForm.SetActive(false);
        changedForm.SetActive(true);
        this.GetComponent<Collider>().enabled = false;
    }
    void Reduction()
    {
        originForm.SetActive(true);
        changedForm.SetActive(false);
        this.GetComponent<Collider>().enabled = true;
    }
}
