using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerBehavior : MonoBehaviour
{
    float timeSinceLastHit;
    float rearmTime;
    AudioSource audioSource;
    void Awake()
    {
        audioSource = transform.GetComponent<AudioSource>();
        rearmTime = 1f;
        timeSinceLastHit = 1.1f;
    }
    void LateUpdate()
    {
        timeSinceLastHit += Time.deltaTime;
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Mecha") && timeSinceLastHit > rearmTime)
        {
            timeSinceLastHit = 0;
            audioSource.Play();
            Transform Player = collision.transform;
            PlayerVitality playerVitality = Player.GetComponent<PlayerVitality>();
            playerVitality.TakeDamage(35);
        }
    }
}
