using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NVGUser : MonoBehaviour
{
    public GameObject NVG;
    public bool NVGenable = false;
    public bool usable = false;

    // Use this for initialization
    void Start()
    {
        NVG.SetActive(NVGenable);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.usable && Input.GetKeyDown(KeyCode.N))
        {
            NVGenable = !NVGenable;
            NVG.SetActive(NVGenable);
        }
    }
}
