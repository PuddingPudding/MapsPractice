using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionMinion : MonoBehaviour
{
    private Color[] originColor;
    Component[] rendererList;

    // Use this for initialization
    void Start()
    {
        rendererList = this.transform.GetComponentsInChildren<Renderer>();
        originColor = new Color[rendererList.Length];
        int i = 0;
        foreach (Renderer renderer in rendererList)
        {
            foreach (Material material in renderer.materials)
            {
                originColor[i] = renderer.material.color; //在開始時先將原本材質的顏色記下
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showFake()
    {
        foreach (Renderer renderer in rendererList)
        {
            foreach (Material material in renderer.materials)
            {
                renderer.material.color = Color.gray;
            }
        }
    }

    public void disguise()
    {
        int i = 0;
        foreach (Renderer renderer in rendererList)
        {
            foreach (Material material in renderer.materials)
            {
                renderer.material.color = originColor[i];
            }
        }
    }
}
