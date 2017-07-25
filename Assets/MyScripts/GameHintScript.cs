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

    public void EnemyDestroy()
    {
        GameHintText.text = "敵人被你擊倒了!快去開寶箱吧!";
        Invoke("TextDisappear", LifeTime);
    }

    public void EnemyAppear()
    {
        GameHintText.text = "門被關起來了! 快擊敗敵人離開這鬼地方";
        Invoke("TextDisappear", LifeTime);
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
        GameHintText.text = "按下G鍵來撿取寶物吧!";
    }

    public void GetCoins()
    {
        GameHintText.text = "快把寶物帶回去吧!";
        Invoke("TextDisappear", LifeTime);
    }

    public void NVGHint()
    {
        GameHintText.text = "已獲得道具-夜視鏡!";
        Invoke("NVGHowToUse", LifeTime);
    }
    private void NVGHowToUse()
    {
        GameHintText.text = "按下N鍵來進行開關";
        Invoke("TextDisappear", LifeTime);
    }

    public void NVGHowToFight()
    {
        GameHintText.text = "眼前出現了一個看不見的敵人，或許你的夜視鏡幫上一點忙";
        Invoke("TextDisappear", LifeTime);
    }

    public void TutorialFinish()
    {
        GameHintText.text = "做得好，恭喜你通過了考驗，你將被傳送至下個區域";
        Invoke("TextDisappear", LifeTime);
    }

    public void TextDisappear()
    {
        GameHintText.text = "";
    }
}
