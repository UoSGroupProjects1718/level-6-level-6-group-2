using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour {

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public Slider healthbar;

    // Use this for initialization
    void Start() {

        MaxHealth = 50f;
        CurrentHealth = MaxHealth;

        healthbar.value = CalculateHealth();

    }

    // Update is called once per frame
    void Update() {
               
    }

    void OnTriggerEnter(Collider other)
    {
        DealDamage(10);
        Debug.Log("Hit");
    }

    void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        healthbar.value = CalculateHealth();
        if (CurrentHealth <= 0)
            Die();
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    void Die()
    {
        CurrentHealth = 0;
        Debug.Log("You Dead");

    }
}
