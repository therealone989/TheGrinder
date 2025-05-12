using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public static event Action<float> OnPlayerHealed;

    public float maxHealth;
    public float currentHealth;

    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.UpdateHealthNumber(maxHealth);
    }

    public void takeBulletDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        healthBar.UpdateHealthNumber(currentHealth);
    }

    public void Heal(float amount)
    {
        float oldHealth = currentHealth;
        currentHealth = MathF.Min(currentHealth + amount, maxHealth);
        healthBar.SetHealth(currentHealth);
        healthBar.UpdateHealthNumber(currentHealth);

        float healedAmount = currentHealth - oldHealth;
        if(healedAmount > 0)
        {
            OnPlayerHealed?.Invoke(healedAmount);
        }
    }

}
