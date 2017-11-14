using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStageEvent : MonoBehaviour {

    public GameObject Door1;
    public GameObject Player;
    public GameObject translucentEnemy;
    public PlayerMovement playerMovement;

    private bool EventStartflag = true;
    private bool Eventoff = false;

    // Use this for initialization
    void Start()
    {
        
    }

	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            TranslucentEvent();
        }
    }

    void TranslucentEvent()
    {
        if (EventStartflag == true)
        {
            playerMovement.enabled = false;
            Component[] rendererList = translucentEnemy.transform.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in rendererList)
            {
                foreach (Material material in renderer.materials)
                {
                    DOTween.To(() => renderer.material.color, (x) => renderer.material.color = x, new Color(1, 1, 1, 0.1f), 2.0f).OnComplete(() =>
                    {
                        translucentEnemy.GetComponent<TranslucentScript>().enabled = true;
                        Door1.transform.DOShakePosition(5.0f,0.1f,10,90);//(持續時間,搖晃方向,震動幅度,隨機性0~180)
                        Door1.transform.DOMoveY(Door1.transform.position.y - 5.5f, 5).OnComplete(() =>
                        {
                            playerMovement.enabled = true;
                            translucentEnemy.GetComponent<EnemyScript>().FollowTarget = Player;
                        });
                    });
                }
            }
        }

        EventStartflag = false;
    }
}
