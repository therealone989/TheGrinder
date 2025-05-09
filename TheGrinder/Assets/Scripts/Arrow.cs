using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int baseDamage = 10;
    public float critMultiplier = 2f;

    private Rigidbody rb;
    private Collider col;
    private bool hasHit = false;
    public bool isCancelled = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        // Pfeil trifft anderen Pfeil ignorieren
        if (collision.collider.GetComponent<Arrow>() != null)
            return;

        hasHit = true;

        if (isCancelled) return;

        // Rigidbody entfernen
        Destroy(rb);
        col.enabled = false;

        // Gegner prüfen
        var enemyHealth = collision.collider.GetComponentInParent<EnemyHealth>();
        if (enemyHealth != null)
        {
            if (collision.collider.CompareTag("Crit"))
            {
                enemyHealth.takeCritDamage(baseDamage);
            }
            else if (collision.collider.CompareTag("Enemy"))
            {
                enemyHealth.takeDamage(baseDamage);
            }

            // Pfeil an Gegner heften
            transform.SetParent(collision.transform);

            Destroy(gameObject, 10f);
        }
        else
        {
            // Kein Gegner keine Parentiong, einfach sterben
            Destroy(gameObject, 5f);
        }
    }

}
