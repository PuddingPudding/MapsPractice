using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAVuser : MonoBehaviour {

    public GameObject UAV;
    public GoggleSystem emenysystem;
    public bool UAVenable = false;
    public bool usable = false;


    // Use this for initialization
    void Start ()
    {
        UAV.SetActive(UAVenable);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.usable && Input.GetKeyDown(KeyCode.N))
        {
            UAVenable = !UAVenable;
            UAV.SetActive(UAVenable);

            if (UAVenable) //判斷怪物是否看的見
            {
                emenysystem.translucentOn();
            }
            else
            {
                emenysystem.translucentOff();
            }
        }
    }
}
