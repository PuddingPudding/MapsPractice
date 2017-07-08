using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyScript : MonoBehaviour
{
    public CollisionListScript PlayerSensor;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles += new Vector3(0, 120 * Time.deltaTime, 0);
        if (PlayerSensor.CollisionObjects.Count > 0)
        {
            PlayerSensor.CollisionObjects[0].SendMessage("getKey", "gold");
            this.gameObject.SetActive(false);
        }
    }
}
