using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    [SerializeField] private GameObject ProjectileOrigin;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float FireRate;
    AudioSource audioSource;
    MuzzleFlash muzzleFlash;
    float timeOfNextShot;
    void Shoot()
    {
        if (Input.GetButton("Fire1") && Time.time > timeOfNextShot)
        {
            audioSource.Play();
            GameObject projectile = Instantiate(Bullet, ProjectileOrigin.transform.position, ProjectileOrigin.transform.rotation);
            timeOfNextShot = Time.time + FireRate / 1000;
            muzzleFlash.Activate();
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        muzzleFlash = GetComponent<MuzzleFlash>();
        timeOfNextShot = FireRate / 1000; //Divided by 1k to get a milisecond value
    }

    void Update()
    {
        Shoot();
    }
}
