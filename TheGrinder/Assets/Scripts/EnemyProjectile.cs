using System.Collections;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] float speed = 30f;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] float damage = 10f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }
    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {

        playerHealth = other.GetComponent<PlayerHealth>();

        if(playerHealth != null)
        {
            // Health abziehen
            playerHealth.takeBulletDamage(damage);

        } 
        Destroy(gameObject);

    }
}
