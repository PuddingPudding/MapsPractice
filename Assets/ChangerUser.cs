using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangerUser : MonoBehaviour
{
    //public GameObject arrowCandidate; //未來名稱請做改變
    public GameObject bow;
    public GameObject arrowOnBow; //置於弓上的箭，用來在射箭後暫時關閉，製作出射出的效果
    private Vector3 arrowLocalPosition;//用於記住最起始的位子，之後復原才可使用
    private Vector3 arrowLocalRotation;//用於記住最起始的旋轉角度，之後復原才可使用
    public float arrowLifeTime = 3f;
    public GameObject changerArrow;

    //干擾器的使用，是把干擾器瞬間拉到弓的位置在發射，同時把自己弓上的那把箭隱藏起來，以做到好像射出去的效果
    //為什麼要這麼做? 因為干擾器要跟ChangeSystem溝通，他不能夠以prefab的形式被複製出來，否則會無法進行參照
    //為什麼無法參照? prefab(遊戲原型)只能參照自己身上的東西，ChangeSystem則是獨立出來於每一關的東西

    // Use this for initialization
    void Start()
    {
        arrowLocalPosition = arrowOnBow.transform.localPosition;
        arrowLocalRotation = arrowOnBow.transform.localEulerAngles;
        this.changerArrowCoolDown();
    }

    // Update is called once per frame
    void Update()
    {
        if(changerArrow)
        if (Input.GetMouseButtonUp(0) && arrowOnBow.active)
        {
            this.changerArrowShoot();
        }
    }

    public void changerArrowCoolDown()
    {
        changerArrow.transform.position = Vector3.zero;
        changerArrow.transform.localEulerAngles = Vector3.zero;
        changerArrow.GetComponent<Collider>().enabled = false;
        changerArrow.GetComponent<ChangerArrowScript>().enabled = false;
        arrowOnBow.SetActive(true);
    }

    public void changerArrowShoot()
    {
        changerArrow.transform.position  = arrowOnBow.transform.position;
        changerArrow.transform.rotation = arrowOnBow.transform.rotation;
        changerArrow.GetComponent<Collider>().enabled = true;
        changerArrow.GetComponent<ChangerArrowScript>().enabled = true;
        changerArrow.GetComponent<ChangerArrowScript>().InitAndShoot(bow.transform.forward);
        arrowOnBow.SetActive(false);
        Invoke("changerArrowCoolDown", arrowLifeTime);
    }
}
