using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float ProjectileSpeed;
    Rigidbody rb;
    float Lifetime;

    void DestroyOnTime()
    {
        Lifetime += Time.deltaTime;
        if (Lifetime >= 2.5f)
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (!enemy.isTriggered)
            {
                enemy.SeekPlayer();
            }
            enemy.TakeDamage(25);
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * ProjectileSpeed;
    }
    void Update()
    {
        DestroyOnTime();
    }
}
