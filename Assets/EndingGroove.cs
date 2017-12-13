using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndingGroove : MonoBehaviour
{
    public Image endScreen;
    public AudioSource BossBGM;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

     public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BossHeartScript>() != null)
        {
            DOTween.To(() => endScreen.color, (x) => endScreen.color = x, Color.black, 3f).OnComplete(() =>
            {
                Application.LoadLevel("Menu");
            });
            BossBGM.DOFade(0, 3f);
        }
    }
}
