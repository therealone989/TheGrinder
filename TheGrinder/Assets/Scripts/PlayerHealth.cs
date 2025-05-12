using System;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerHealth : MonoBehaviour
{

    public static event Action<float> OnPlayerHealed;

    public float maxHealth;
    public float currentHealth;

    public HealthBar healthBar;

    [Header("VFX")]
    public VisualEffect healVFX;

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
        currentHealth = MathF.Min(currentHealth + amount, maxHealth);

        healthBar.SetHealth(currentHealth);
        healthBar.UpdateHealthNumber(currentHealth);

        OnPlayerHealed?.Invoke(amount);
        Debug.Log("HEAL SFX");
        healVFX.Play();

    }

}
