using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject stageSelects;
    public GameObject storySelects;

    // Use this for initialization
    void Start()
    {
        backToMainMenu(); //開場先呼叫跳回主選單(讓其他選單都先關掉，再開啟主選單)
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void backToMainMenu()
    {
        stageSelects.SetActive(false);
        storySelects.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void jumpToStageMenu()
    {
        stageSelects.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void jumpToStoryMenu()
    {
        storySelects.SetActive(true);
        mainMenu.SetActive(false);
    }
}
