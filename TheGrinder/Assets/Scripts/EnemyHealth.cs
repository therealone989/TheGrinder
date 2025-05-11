using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public float startHealth;
    private float currentHealth;

    [SerializeField] private Image healthSprite;
    [SerializeField] Canvas canv;

    public void UpdateHealthBar(float currHealth, float maxHealth)
    {
        healthSprite.fillAmount = currHealth / maxHealth;
    }

    void Awake()
    {
        currentHealth = startHealth;
    }

    public void takeDamage(float weaponDamage)
    {
        currentHealth -= weaponDamage;
        UpdateHealthBar(currentHealth, startHealth);
        Debug.Log("HEALTH: " + currentHealth);
    }

    public void takeCritDamage(float weaponDamage)
    {
        currentHealth -= weaponDamage * 2;
        UpdateHealthBar(currentHealth, startHealth);
        Debug.Log("CRIT AHHH");
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        canv.transform.rotation = Quaternion.LookRotation(canv.transform.position - Camera.main.transform.position);
    }


}
