﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableItem : MonoBehaviour
{
    [System.Serializable] //將BreakEntry序列化，如此才能放入list中 (C#要求list只能裝序列化的資料)
    public class BreakingEntry
    {
        public GameObject breakNode;
        public float breakingHP;
    }
    public float CurrentHP;
    public List<BreakingEntry> BreakingSettings;
    public GameObject DestroyEffect;
    public void Hit(float hitValue)
    {
        if (CurrentHP > 0)
        {
            CurrentHP -= hitValue;
            if (CurrentHP <= 0)
            {
                DestroyEffect.gameObject.SetActive(true);
                this.transform.DOScale(new Vector3(0.0f, 0.0f, 0.0f), 0.01f).SetDelay(0.1f).OnComplete(() => {
                    Invoke("DisableParticleSystems", 10);
                });
            }
            else
            {
                foreach (BreakingEntry entry in BreakingSettings)
                {
                    if (CurrentHP < entry.breakingHP)
                    {
                        entry.breakNode.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
    public void DisableParticleSystems()
    {
        ParticleSystem[] particles = this.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in particles)
        {
            p.Stop();
        }
        Invoke("KillYourself", 5);
    }
    //自殺有時候可以解決問題
    public void KillYourself()
    {
        GameObject.Destroy(this.gameObject);
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
