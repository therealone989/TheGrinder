using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public static event Action<float> OnEnemyKilled; // Gold als float übergeben


    public float startHealth;
    private float currentHealth;

    [SerializeField] private Image healthSprite;
    [SerializeField] Canvas canv;

    [SerializeField] float rewardGold = 15f;


    void Awake()
    {
        currentHealth = startHealth;
    }


    private void Update()
    {
        if(currentHealth <= 0)
        {
            Die();
        }

        canv.transform.rotation = Quaternion.LookRotation(canv.transform.position - Camera.main.transform.position);
    }

    public void takeDamage(float weaponDamage)
    {
        currentHealth -= weaponDamage;
        UpdateHealthBar(currentHealth, startHealth);
    }

    public void takeCritDamage(float weaponDamage)
    {
        currentHealth -= weaponDamage * 2;
        UpdateHealthBar(currentHealth, startHealth);
    }

    public void UpdateHealthBar(float currHealth, float maxHealth)
    {
        healthSprite.fillAmount = currHealth / maxHealth;
    }

    private void Die()
    {
        OnEnemyKilled?.Invoke(rewardGold);
        Destroy(gameObject);
    }


}
