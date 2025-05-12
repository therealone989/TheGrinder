using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour
{
    public KeyCode healKey = KeyCode.F;
    public float healAmount = 25f;
    public float healCooldown = 5f;
    private bool canHeal = true;

    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(healKey) && canHeal)
        {
            Heal();
            StartCoroutine(HealCooldown());
        }
    }

    private void Heal()
    {
        if (playerHealth == null) return;

        playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth + healAmount, playerHealth.maxHealth);
        Debug.Log("Healed for " + healAmount);
        playerHealth.healthBar.SetHealth(playerHealth.currentHealth);
        playerHealth.healthBar.UpdateHealthNumber(playerHealth.currentHealth);
    }

    private IEnumerator HealCooldown()
    {
        canHeal = false;
        yield return new WaitForSeconds(healCooldown);
        canHeal = true;
    }
}
