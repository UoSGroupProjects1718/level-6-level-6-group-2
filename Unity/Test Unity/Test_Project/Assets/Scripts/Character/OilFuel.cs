using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OilFuel : MonoBehaviour {

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public Slider Oil;

    

    // Use this for initialization
    void Start() {

        MaxHealth = 100f;
        CurrentHealth = MaxHealth;

        Oil.value = CalculateHealth();

    }

    // Update is called once per frame
    void Update() {
 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damage")
        {
            DealDamage(10);
            Debug.Log("Hit");
        }
    }

    void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        Oil.value = CalculateHealth();
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
