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
        playerHealth.Heal(healAmount);
    }

    private IEnumerator HealCooldown()
    {
        canHeal = false;
        yield return new WaitForSeconds(healCooldown);
        canHeal = true;
    }
}
