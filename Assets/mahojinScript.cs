using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mahojinScript : MonoBehaviour {

    //放在mahojinCircle上

    public float waitTime;
    public int Damage;
    private int childCount;

    // Use this for initialization
    void Start () {
        childCount = gameObject.transform.childCount;
        StartCoroutine(changeColor());
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    
    IEnumerator changeColor()
    {
        for (int i = 0; i < childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(waitTime);
        }
        KillYourself();
    }

    public void KillYourself()
    {
        if (this.GetComponentInChildren<CollisionListScript>().CollisionObjects.Count > 0)
        {
            if (this.GetComponentInChildren<CollisionListScript>().CollisionObjects[0].GetComponent<PlayerScript>() != null)
            {
                this.GetComponentInChildren<CollisionListScript>().CollisionObjects[0].GetComponent<PlayerScript>().Hit(Damage);
                Debug.Log("damage" + Damage);
                //this.GetComponentInChildren<CollisionListScript>().CollisionObjects.Clear();
            }
        }
        this.GetComponentInParent<AudioSource>().Play(); //爆炸聲
        this.gameObject.SetActive(false); //魔法符文消失
        Destroy(this.transform.parent.gameObject, 1f); //一秒後摧毀該魔法陣所有物件
    }
}
