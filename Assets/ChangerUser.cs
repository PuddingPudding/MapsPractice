using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangerUser : MonoBehaviour {

    public Image target;

    public GameObject arrowCandidate; //未來名稱請做改變
    public GameObject bow;
    public GameObject arrowOnBow; //置於弓上的箭，用來在射箭後暫時關閉，製作出射出的效果


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0))
        {
            GameObject newArrow = GameObject.Instantiate(arrowCandidate);
            ArrowScript arrow = newArrow.GetComponent<ArrowScript>();
            arrowOnBow.SetActive(false); //射出箭了以後，把弓上的箭模組調成關閉

            arrow.transform.position = bow.transform.position;
            arrow.transform.rotation = bow.transform.rotation;
            arrow.InitAndShoot(bow.transform.forward);
            //射箭
        }


    }
}
