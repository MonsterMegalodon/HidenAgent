using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponet : MonoBehaviour
{
    public delegate void OnHealthChanged(float currentHealth, float delta, float maxHealth);
    public delegate void OnHealthEmpty(float delta, float maxHealth);
    public delegate void OnTakenDamage(float currentHealth, float delta, float maxHealth);

    public event OnHealthChanged onHealthChanged;
    public event OnHealthEmpty onHealthEmpty;
    public event OnTakenDamage onTakenDamage;

    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;

    public void ChangeHealth(float amt)
    { 
        //using early return.
        if(amt == 0 || currentHealth == 0)
        {
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth + amt, 0, maxHealth);
        
        onHealthChanged?.Invoke(currentHealth, amt, maxHealth);
        if(amt < 0)
        {
            onTakenDamage?.Invoke(currentHealth, amt, maxHealth);
        }

        if(currentHealth == 0)
        {
            onHealthEmpty?.Invoke(amt, maxHealth);
        }
    }
}
