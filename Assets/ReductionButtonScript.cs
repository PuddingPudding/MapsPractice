using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReductionButtonScript : MonoBehaviour
{

    public GameObject rockerUp;
    public GameObject rockerDown;

    // Use this for initialization
    void Start()
    {
        rockerUp.SetActive(true);
        rockerDown.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RockerUp()
    {
        rockerUp.SetActive(true);
        rockerDown.SetActive(false);
    }

    public void RockerDown()
    {
        rockerUp.SetActive(false);
        rockerDown.SetActive(true);
    }

}
