using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string SlotName = "DefaultWeaponSlot";

    public string GetSlotName()
    {
        return SlotName;
    }
    public GameObject Owner
    {
        get;
        private set;
    }    

    public void Init(GameObject owner)
    {
        Owner = owner;
    }

    public void Equip() 
    {
        gameObject.SetActive(true);
    }

    public void UnEquip()
    {
        gameObject.SetActive(false);
    }
}
