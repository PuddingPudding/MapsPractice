using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkToPlayerScript : MonoBehaviour {

    public string Message;
    public GameObject PointUI;
    public Text GameObjectMessage;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<PlayerScript>())
        {
            PointUI.SetActive(true);
        }
    }

    void OnTriggerStay()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObjectMessage.text = Message;
        }
    }

    void OnTriggerExit(Collider other)
    {
        PointUI.SetActive(false);
        GameObjectMessage.text = null;
    }
}
