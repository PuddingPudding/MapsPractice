using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHintScript : MonoBehaviour {

    public GameObject WASD;
    public GameObject Mouse;
    public Text GameHintText;
    public float LifeTime; //字幕存活時間

    void Start()
    {
        WASD.SetActive(false);
        Mouse.SetActive(false);
    }

    public void SearchKey()
    {
        GameHintText.text = "快去尋找鑰匙把寶箱打開吧!";
        Invoke("TextDisappear", LifeTime);
    }

    public void StartText()
    {
        GameHintText.text = "歡迎來到暗黑地牢!";
        Invoke("TextDisappear", LifeTime);
    }

    public void OpenImage()
    {
        GameHintText.text = "運用WASD來移動吧!";
        WASD.SetActive(true);
        Mouse.SetActive(true);
        Invoke("TextDisappear", LifeTime + 3);
        Invoke("ImageDisappear", LifeTime + 3);
    }

    public void JumpText()
    {
        GameHintText.text = "按下空白鍵來跳過這道牆吧!";
        Invoke("TextDisappear", LifeTime + 1.5f);
    }

    public void ImageDisappear()
    {
        WASD.SetActive(false);
        Mouse.SetActive(false);
    }

    public void NotFoundKey()
    {
        GameHintText.text = "尚未取得鑰匙";
        Invoke("TextDisappear", LifeTime);
    }

    public void GetKey()
    {
        GameHintText.text = "取得鑰匙";
        Invoke("TextDisappear", LifeTime);
    }

    public void OpenChest()
    {
        GameHintText.text = "咑咑咑咑咑咑咑～!";
        Invoke("TextDisappear", LifeTime);
    }

    public void TextDisappear()
    {
        GameHintText.text = "";
    }
}
