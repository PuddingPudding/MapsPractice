using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControllerScript : MonoBehaviour {

    public GameObject Menu;
    bool MenuEnable = false;

    // Use this for initialization
    void Start ()
    {
        Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuEnable = !MenuEnable;
            Menu.SetActive(MenuEnable);
            Cursor.visible = MenuEnable;
        }
    }
}
