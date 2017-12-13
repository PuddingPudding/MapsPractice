using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdAreaEventSystem : MonoBehaviour
{

    public CollisionListScript StartArea;
    public CollisionListScript StartArea2;
    public List<StrongEnemyScript> Enemy;
    public List<StrongEnemyScript> Enemy2;
    public GameObject BossDoor;
    public Image endScreen;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (StartArea.CollisionObjects.Count > 0) //穿過觸發點開始倒數
        {
            for (int i = 0; i < Enemy.Count; i++)
            {
                Enemy[i].CountingStart = true;
            }
        }

        if (StartArea2.CollisionObjects.Count > 0) //穿過觸發點開始倒數
        {
            for (int j = 0; j < Enemy2.Count; j++)
            {
                Enemy2[j].CountingStart = true;
            }
        }

        if (Enemy2[0].CurrentHP <= 0 && Enemy2[1].CurrentHP <= 0) //判斷最後兩隻是否死亡
        {
            BossDoor.GetComponent<TalkToPlayerScript>().Message = "";
            if(BossDoor.GetComponent<CollisionListScript>().CollisionObjects.Count > 0 && Input.GetKeyDown(KeyCode.F) )
            {
                DOTween.To(() => endScreen.color, (x) => endScreen.color = x, Color.black, 3f).OnComplete(() =>
                {
                    Application.LoadLevel("fourth");
                });
            }
        }
    }
}
