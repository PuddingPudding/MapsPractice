using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool hasGoldKey = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getKey(string keyType)
    {
        if(keyType.ToLower().Equals("gold") )
        {
            this.hasGoldKey = true;
        }
    }
}
