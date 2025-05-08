using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int startHealth = 20;
    public int currentHealth;

    void Awake()
    {
        currentHealth = startHealth;
    }

    public void takeDamage(int weaponDamage)
    {
        currentHealth =- weaponDamage;
        Debug.Log("HEALTH: " + currentHealth);
    }


}
