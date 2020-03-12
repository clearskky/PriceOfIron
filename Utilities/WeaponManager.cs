using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    int WeaponIndex;

    void EquipWeapon(int _weaponIndex)
    {
        foreach(Transform child in transform)
        {
            if (child.GetSiblingIndex() != _weaponIndex)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }
    void SwapWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponIndex = 0;
            EquipWeapon(WeaponIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponIndex = 1;
            EquipWeapon(WeaponIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            WeaponIndex = 2;
            EquipWeapon(WeaponIndex);
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            WeaponIndex += 1;
            if (WeaponIndex > 2)
            {
                WeaponIndex = 0;
            }
            EquipWeapon(WeaponIndex);
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            WeaponIndex -= 1;
            if (WeaponIndex < 0)
            {
                WeaponIndex = 2;
            }
            EquipWeapon(WeaponIndex);
        }
    }
    void Start()
    {
        WeaponIndex = 0;
        EquipWeapon(WeaponIndex);
    }

    void Update()
    {
        SwapWeapon();
    }
}
