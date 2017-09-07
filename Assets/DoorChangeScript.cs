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
        if(Input.GetKeyDown(KeyCode.Y) )
        {
            this.Change();
        }
    }

    void Change()
    {
        originForm.SetActive(false);
        changedForm.SetActive(true);
    }
    void Reduction()
    {
        originForm.SetActive(true);
        changedForm.SetActive(false);
    }
}
