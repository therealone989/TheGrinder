using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Damage Settings")]
    public float baseDamage = 10f;
    public float critMultiplier = 2f;

    [Header("Movement")]
    public float speed = 30f;
    public float lifetime = 5f;

    [Header("Behavior")]
    public bool isSticky = false;
    public bool destroyOnImpact = true;

    private Rigidbody rb;
    private Collider col;
    private bool hasHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private int currScene;

    private void Start()
    {
        rb.linearVelocity = transform.forward * speed;
        currScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        hasHit = true;

        if(collision.collider.tag == "Random")
        {
            SceneManager.LoadScene(currScene +1);
        }

        // Schaden zufügen, wenn Enemy getroffen
        var enemyHealth = collision.collider.GetComponentInParent<EnemyHealth>();
        if (enemyHealth != null)
        {
            if (collision.collider.CompareTag("Crit"))
                enemyHealth.takeCritDamage(baseDamage);
            else
                enemyHealth.takeDamage(baseDamage);
        }

        // Sticky Verhalten
        if (isSticky)
        {
            rb.isKinematic = true;
            col.enabled = false;
            Destroy(rb);
            
            if (collision.collider.tag == "Enemy" || collision.collider.tag == "Crit")
            {
                transform.SetParent(collision.transform);
            }

            Destroy(gameObject, lifetime);
            return;
        }

        // Zerstöre das Projektil sofort bei Aufprall
        if (destroyOnImpact)
        {
            Destroy(gameObject);
        }
    }

}
