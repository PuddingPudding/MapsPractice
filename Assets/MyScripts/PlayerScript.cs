using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public bool hasGoldKey = false;
    public GameHintScript gameHintScript; //控制字幕程式碼

    public float CurrentHP = 100f;
    public Text HPText;

    // Use this for initialization
    void Start()
    {
        HPText.text = "HP: " + CurrentHP;
    }

    public void Hit(int value)
    {
        CurrentHP -= value;
        HPText.text = "HP: " + CurrentHP;
    }

        // Update is called once per frame
        void Update()
    {

    }

    public void getKey(string keyType)
    {
        if (keyType.ToLower().Equals("gold"))
        {
            this.hasGoldKey = true;
            gameHintScript.GetKey();
        }
    }

    void textDisappear()
    {
        gameHintScript.TextDisappear();
    }
}