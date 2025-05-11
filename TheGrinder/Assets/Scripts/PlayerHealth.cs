using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;


    public void takeBulletDamage(float damage)
    {
        currentHealth -= damage;
    }

}
