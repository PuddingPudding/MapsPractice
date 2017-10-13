using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChangeScript : MonoBehaviour
{
    public GameObject originForm;
    public Vector3 changedPosition;//受干擾後改變的物件位置
    private Vector3 originPosition;//原本的物件位置
    public GameObject pointUI;
    private bool hasBeenChanged;

    // Use this for initialization
    void Start()
    {
        originPosition = originForm.transform.localPosition;
        hasBeenChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Change()
    {
        originForm.transform.localPosition = changedPosition;
        this.GetComponent<TalkToPlayerScript>().enabled = false;
        this.GetComponent<Collider>().enabled = false;
            //hasBeenChanged = true
    }
    void Reduction()
    {
        originForm.transform.localPosition = originPosition;
        this.GetComponent<TalkToPlayerScript>().enabled = true;
        this.GetComponent<Collider>().enabled = true;
        //hasBeenChanged = false;
    }
}
