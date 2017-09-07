using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public CollisionListScript PlayerSensor;
    public bool isOpen = false;
    public GameObject openState;
    public GameObject closeState;
    public PlayerManager playerManager;
    public ChestSoundList chestSoundList;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public bool PlayerTouched()//回傳是否被玩家碰到了
    {
        return (PlayerSensor.CollisionObjects.Count > 0 && PlayerSensor.CollisionObjects[0].GetComponent<PlayerScript>() != null);
        //首先確認有人碰觸，在確認碰觸的東西是不是玩家
    }

    public void OpenChest()
    {
        this.chestSoundList.playOpenSound();
        this.closeState.SetActive(false);
        this.openState.SetActive(true);
        this.isOpen = true;
    }
}
