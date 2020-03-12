using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float ProjectileSpeed;
    Rigidbody rb;
    float Lifetime;
    void DetonateOnTime()
    {
        Lifetime += Time.deltaTime;
        if (Lifetime >= 4f)
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * ProjectileSpeed;
    }
    void Update()
    {
        DetonateOnTime();
    }
}
