using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int startHealth = 20;
    private int currentHealth;

    void Awake()
    {
        currentHealth = startHealth;
    }

    public void takeDamage(int weaponDamage)
    {
        currentHealth -= weaponDamage;
        Debug.Log("HEALTH: " + currentHealth);
    }

    public void takeCritDamage(int weaponDamage)
    {
        currentHealth -= weaponDamage * 2;
        Debug.Log("CRIT AHHH");
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


}
