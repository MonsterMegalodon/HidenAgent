using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ValueGuage healthBarPrefab;
    [SerializeField] Transform healthBarAttachTransform;
    HealthComponet healthComponet;

    ValueGuage healthBar;

    private void Awake()
    {
        healthComponet = GetComponent<HealthComponet>();
        healthComponet.onTakenDamage += TookDamage;
        healthComponet.onHealthEmpty += StartDealth;
        healthComponet.onHealthChanged += HealthChanged;

        healthBar = Instantiate(healthBarPrefab, FindObjectOfType<Canvas>().transform);
        UIAttachComponent attachmentComp = healthBar.AddComponent<UIAttachComponent>();
        attachmentComp.SetupAttachment(healthBarAttachTransform);
    }

    private void HealthChanged(float currentHealth, float delta, float maxHealth)
    {
        healthBar.SetValue(currentHealth, maxHealth);
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
