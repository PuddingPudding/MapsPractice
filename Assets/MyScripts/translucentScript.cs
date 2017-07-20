using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translucentScript : MonoBehaviour
{

    public float TranslucentValue = 0.3f; //透明度 可在外面調整

    // Use this for initialization
    void Start()
    {
        //開始就調整透明度
        Component[] rendererList = this.transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in rendererList)
        {
            foreach (Material material in renderer.materials)
            {
                renderer.material.color = new Color(1, 1, 1, TranslucentValue);
            }
        }
    }
    //void Update()
    //{
    //    Component[] rendererList = this.transform.GetComponentsInChildren<Renderer>();
    //    foreach (Renderer renderer in rendererList)
    //    {
    //        foreach (Material material in renderer.materials)
    //        {
    //            renderer.material.color = new Color(0.2f, 0.2f, 0.8f, TranslucentValue);
    //        }
    //    }
    //}

    public void BecomeTranslucent() //變回原本透明度(隱形)
    {
        Component[] rendererList = this.transform.GetComponentsInChildren<Renderer>();        
        foreach (Renderer renderer in rendererList)
        {
            foreach (Material material in renderer.materials)
            {
                renderer.material.color = new Color(1, 1, 1, TranslucentValue);
            }
        }
    }

    public void BecomeCanSee() //變成可以看的見
    {
        Component[] rendererList = this.transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in rendererList)
        {
            foreach (Material material in renderer.materials)
            {
                renderer.material.color = new Color(0.2f, 0.2f, 0.8f, 1);
            }
        }
    }
}
