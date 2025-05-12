using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
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

}
