using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherBehavior : MonoBehaviour
{
    [SerializeField] private GameObject ProjectileOrigin;
    [SerializeField] private GameObject Rocket;
    [SerializeField] private float FireRate;
    float timeOfNextShot;

    void Shoot()
    {
        if (Input.GetButton("Fire1") && Time.time > timeOfNextShot)
        {
            GameObject projectile = Instantiate(Rocket, ProjectileOrigin.transform.position, ProjectileOrigin.transform.rotation);
            timeOfNextShot = Time.time + FireRate / 1000;
        }
    }

    void Start()
    {
        timeOfNextShot = FireRate / 1000; //Divided by 1k to get a milisecond value
    }

    void Update()
    {
        Shoot();
    }
}
