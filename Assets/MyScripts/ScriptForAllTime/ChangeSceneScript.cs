using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneScript : MonoBehaviour
{

    public void ChangeToScene(string SceneName)
    {
        Application.LoadLevel(SceneName);
    }

    public void ChangeToScene(int SceneID)
    {
        Application.LoadLevel(SceneID);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
