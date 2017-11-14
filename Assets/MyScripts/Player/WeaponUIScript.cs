using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIScript : MonoBehaviour {

    public Image Frame; //框
    public GameObject Ammo; //子彈條
    private Text Num; //框上的數字

    // Use this for initialization
    void Start () {
        Num = Frame.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenAmmoUI()
    {
        Frame.color = Color.green;
        Num.color = Color.green;
        Ammo.SetActive(true);
    }

    public void CloseAmmoUI()
    {
        Frame.color = Color.white;
        Num.color = Color.white;
        Ammo.SetActive(false);
    }
}
