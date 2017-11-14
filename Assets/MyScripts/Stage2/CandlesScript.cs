using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlesScript : MonoBehaviour {

    public CellDoorScript Gate;
    public GameObject Flames;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Gate.getDoorIsOpen())
        {
            Flames.SetActive(true);
        }
        else
        {
            Flames.SetActive(false);
        }
	}
}
