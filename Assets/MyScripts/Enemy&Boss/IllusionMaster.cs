using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionMaster : MonoBehaviour
{
    public IllusionMinion[] minions;
    public bool illusionswitch = false; //表示幻覺狀態是否開啟，true表示為顯現，false為平常幻覺狀態

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            if(illusionswitch == false)
            {
                foreach (IllusionMinion minion in minions)
                {
                    minion.showFake();
                }
                illusionswitch = true;
            }
            else
            {
                foreach (IllusionMinion minion in minions)
                {
                    minion.disguise();
                }
                illusionswitch = false;
            }            
        }
    }
}
