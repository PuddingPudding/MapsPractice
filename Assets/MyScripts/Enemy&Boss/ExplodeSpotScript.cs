using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSpotScript : MonoBehaviour
{

    public float ExpandSpeed;
    public Vector3 ExpandVector;
    public float RangeMax;
    public float ExplodeTime;
    public int Damage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (this.transform.localScale.x < RangeMax)
        {
            //this.transform.localScale = this.transform.localScale * ExpandSpeed;
            this.transform.localScale += ExpandVector*Time.deltaTime;
        }
        else if (this.transform.localScale.x >= RangeMax)
        {
            Invoke("KillYourself", ExplodeTime);
        }

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
        Destroy(this.gameObject);
    }
}
