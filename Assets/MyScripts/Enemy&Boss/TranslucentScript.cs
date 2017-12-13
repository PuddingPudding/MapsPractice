using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslucentScript : MonoBehaviour
{
    public float TranslucentValue = 0.3f; //透明度 可在外面調整
    private List<Color> originColorList = new List<Color>();

    // Use this for initialization
    void Awake() //改Awake是因為無論隱形敵人一剛開始是否存在，我們都希望originColorList被建立
    {
        //開始就調整透明度
        Component[] rendererList = this.transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in rendererList)
        {
            foreach (Material material in renderer.materials)
            {
                originColorList.Add(renderer.material.color); //先把敵人原本真正的顏色記下
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
                //renderer.material.color = new Color(0.2f, 0.2f, 0.8f, 1);
                renderer.material.color = Color.red;
            }
        }
    }

    public void BecomeNormal() //變成普通狀態
    {
        Component[] rendererList = this.transform.GetComponentsInChildren<Renderer>();
        int i = 0;
        foreach (Renderer renderer in rendererList)
        {
            foreach (Material material in renderer.materials)
            {
                renderer.material.color = originColorList[i];
                i++;
            }
        }
    }
}
