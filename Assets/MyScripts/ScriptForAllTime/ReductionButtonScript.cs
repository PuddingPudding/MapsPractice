using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReductionButtonScript : MonoBehaviour
{
    public GameObject rockerUp;
    public GameObject rockerDown;

    private AudioSource[] SoundArray;

    // Use this for initialization
    void Start()
    {
        rockerUp.SetActive(true);
        rockerDown.SetActive(false);
        SoundArray = this.GetComponents<AudioSource>();
        //拉桿的音效陣列，其列表內的音效與編號如下
        //0號:壓下音效
        //1號:彈回音效
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RockerUp()
    {
        rockerUp.SetActive(true);
        rockerDown.SetActive(false);
        SoundArray[1].Play();
    }

    public void RockerDown()
    {
        rockerUp.SetActive(false);
        rockerDown.SetActive(true);
        SoundArray[0].Play();
    }

    public void RockerPress()
    {
        this.RockerDown();
        Invoke("RockerUp", 1f);
    }

}
