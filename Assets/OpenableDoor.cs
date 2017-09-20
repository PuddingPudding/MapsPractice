using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableDoor : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoorRotate()
    {
        this.transform.DOLocalRotate(new Vector3(0, -55, 0), 2);
    }
}
