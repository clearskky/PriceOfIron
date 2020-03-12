using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVitality : MonoBehaviour, IDamagable
{
    public float MaxHealth = 250f;
    private float CurrentHealth;
    Image HealthBar;
    
    void Start()
    {
        HealthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;
        UpdateHealthBar();
        if (CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void UpdateHealthBar()
    {
        HealthBar.fillAmount = (1.0f * CurrentHealth) / (MaxHealth * 2.0f);
    }
    void Update()
    {
        
    }
}
