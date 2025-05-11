using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    [Header("Attack Settings")]
    public float attackRange = 5f;
    public float fireCooldown = 2f;
    private float lastFireTime;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform shootPoint;

    public float enemySpacing;

    [Header("Vision Settings")]
    public float viewAngle = 120f;
    public float viewDistance = 10f;
    public LayerMask obstacleMask; // z.B. "Obstacles"

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(0, 100);
    }

    private void Update()
    {
        if (target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (CanSeeTarget())
        {
            if (distanceToTarget <= attackRange)
            {
                TryShoot();
                agent.SetDestination(transform.position); // stehen bleiben
            }
            else
            {
                agent.SetDestination(target.position); // verfolgen
            }
        }
        else
        {
            // Kein Sichtkontakt – versuche Position zu wechseln
            agent.SetDestination(target.position);
        }

        HandleNearbyEnemies(); // nur leichte Korrektur

        // Nur Y-Rotation
        Vector3 lookDir = target.position - transform.position;
        lookDir.y = 0;
        if (lookDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }


    public void HandleNearbyEnemies()
    {
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, enemySpacing);

        Vector3 separation = Vector3.zero;
        int count = 0;

        foreach (Collider col in nearbyEnemies)
        {
            if (col.gameObject != this.gameObject && col.CompareTag("Enemy"))
            {
                Vector3 diff = transform.position - col.transform.position;
                separation += diff.normalized / diff.magnitude;
                count++;
            }
        }

        if (count > 0)
        {
            separation /= count;
            agent.velocity += separation;
        }
    }

    private void TryShoot()
    {
        if (Time.time < lastFireTime + fireCooldown) return;

        lastFireTime = Time.time;

        if (projectilePrefab != null && shootPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            projectile.transform.LookAt(target.transform);
        }
    }

    private bool CanSeeTarget()
    {
        if (target == null) return false;

        Vector3 dirToTarget = (target.position - transform.position).normalized;
        float angleToTarget = Vector3.Angle(transform.forward, dirToTarget);

        if (angleToTarget < viewAngle / 2f)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= viewDistance)
            {
                if (!Physics.Raycast(transform.position + Vector3.up * 1.5f, dirToTarget, out RaycastHit hit, viewDistance, obstacleMask))
                {
                    return true; // keine Wand dazwischen
                }

                if (hit.transform == target)
                {
                    return true; // direkter Blickkontakt zum Spieler
                }
            }
        }

        return false;
    }

}
