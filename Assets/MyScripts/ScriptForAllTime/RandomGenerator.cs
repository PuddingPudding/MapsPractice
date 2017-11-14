using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{

    public float positionX;
    public float positionY;
    public float positionZ;

    public float RangeMinX;
    public float RangeMaxX;
    public float RangeMinZ;
    public float RangeMaxZ;

    public GameObject ExplodeSpot;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomMaker();
        }
    }

    public void RandomMaker()
    {
        positionX = Random.Range(RangeMinX, RangeMaxX);
        positionZ = Random.Range(RangeMinZ, RangeMaxZ);
        GameObject newExplodeSpot = GameObject.Instantiate(ExplodeSpot);
        newExplodeSpot.transform.position = new Vector3(positionX, positionY, positionZ);
    }
}
