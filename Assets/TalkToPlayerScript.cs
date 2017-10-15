using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkToPlayerScript : MonoBehaviour
{

    public string Message;
    public string buttonImage;
    public string textImage;
    public GameObject PointUI;
    public Text ButtonImage;
    public Text TextImage;
    public Text GameObjectMessage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerScript>())
        {
            ButtonImage.text = buttonImage;
            TextImage.text = textImage;
            PointUI.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other) //??我們原本沒有加參數，該method卻依然在玩家碰到時被呼叫了
    {
        Debug.Log("碰到了!");
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObjectMessage.text = Message;
        }
    }

    void OnTriggerExit(Collider other)
    {
        PointUI.SetActive(false);
        GameObjectMessage.text = null;
    }

    private void OnDisable()
    {
        //以下這一段拿掉會有大量錯誤產生，但並非致命錯誤，因為其餘talkToPlayer物件試圖於關閉時去關閉PointUI
        if (PointUI != null)
        {
            PointUI.SetActive(false);
            GameObjectMessage.text = null;
        }
        
    }
}
