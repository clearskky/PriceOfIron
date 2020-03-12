using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBehavior : MonoBehaviour
{
    [SerializeField] private float ProjectileSpeed;
    [SerializeField] private GameObject ProjectileOrigin;
    [SerializeField] private GameObject Pellet;
    float timeOfNextShot;
    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(Pellet, ProjectileOrigin.transform.position, ProjectileOrigin.transform.rotation);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            GameObject projectile = Instantiate(Pellet, ProjectileOrigin.transform.position, ProjectileOrigin.transform.rotation);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        Shoot();
    }
}
