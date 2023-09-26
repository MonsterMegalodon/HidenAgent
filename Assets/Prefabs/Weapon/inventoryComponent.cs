using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryComponent : MonoBehaviour
{

    [SerializeField] Weapon[] initialWeaponPrefabs;
    [SerializeField] Transform[] weaponSlots;
    [SerializeField] Transform defaultWeaponSlot;

    private List<Weapon> weapons = new List<Weapon>();

    int currentWeaponIndex = -1; // negative value means something do not exit.

    private void Awake()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        foreach (Weapon weaponPrefab in initialWeaponPrefabs)
        {
            Transform weaponslot = defaultWeaponSlot;
            foreach (Transform slot in weaponSlots) 
            {
                if (slot.name == weaponPrefab.GetSlotName())
                {
                    weaponslot = slot;
                    break;

                }
            }
            Weapon newWeapon = Instantiate<Weapon>(weaponPrefab, weaponslot);
            newWeapon.Init(gameObject);

            weapons.Add(newWeapon);
            newWeapon.UnEquip();
        }

        NextWeapon();
    }

    public void NextWeapon()
    {
        int nextIndex = currentWeaponIndex + 1;
        if (nextIndex >= weapons.Count)
        {
            nextIndex = 0;
        }

        EquipWeapon(nextIndex);
    }

    private void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weapons.Count)
        {
            return; // index out of range
        }

        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].UnEquip(); // unequip current weapon if holding any
        }

        currentWeaponIndex = weaponIndex;
        weapons[currentWeaponIndex].Equip(); // enquip the new weapon.
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
