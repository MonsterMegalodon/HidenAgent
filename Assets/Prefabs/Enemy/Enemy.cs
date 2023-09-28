using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    HealthComponet healthComponet;
    private void Awake()
    {
        healthComponet = GetComponent<HealthComponet>();
        healthComponet.onTakenDamage += TookDamage;
        healthComponet.onHealthEmpty += StartDealth;
        healthComponet.onHealthChanged += HealthChanged;
    }

    private void HealthChanged(float currentHealth, float delta, float maxHealth)
    {
        
    }

    private void StartDealth(float delta, float maxHealth)
    {
        Debug.Log("Dead!");
    }

    private void TookDamage(float currentHealth, float delta, float maxHealth)
    {
        Debug.Log($"Took Damage {delta}, now health is: {currentHealth}/{maxHealth}");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
