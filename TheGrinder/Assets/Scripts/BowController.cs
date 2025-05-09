using UnityEngine;

public class BowController : MonoBehaviour
{
    private Animator animator;
    public Weapon weaponScript; // Raycast-Skript

    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;

    private GameObject currentArrow;

    private bool isDrawing = false;
    private bool hasDrawnFully = false;
    private float drawDuration = 1.5f;
    private float drawTimer = 0f;

    [Header("Arrow Settings")]
    public float maxArrowForce = 50f; // Maximale Kraft bei voller Spannung
    public float shortCooldown = 0.5f;  // Cooldown für schwachen Schuss
    private float currentCooldown = 0f; // Aktuellen Cooldown speichern

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Cooldown-Management
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && currentCooldown <= 0) // Nur wenn kein Cooldown aktiv ist
        {
            isDrawing = true;
            hasDrawnFully = false;
            drawTimer = 0f;
            animator.SetBool("isDrawing", true);

            // Pfeil Spawnen und an Sehne hängen
            currentArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            currentArrow.transform.SetParent(arrowSpawnPoint);
        }

        // Während gezogen wird
        if (isDrawing)
        {
            drawTimer += Time.deltaTime;

            if (currentArrow != null)
            {
                // Pfeilposition immer an SpawnPoint ausrichten
                currentArrow.transform.position = arrowSpawnPoint.position;
                currentArrow.transform.rotation = arrowSpawnPoint.rotation;

                // Rückbewegung (max -1 auf Z-Achse)
                float drawBack = Mathf.Lerp(0f, -1f, Mathf.Clamp01(drawTimer / drawDuration));
                currentArrow.transform.Translate(0f, 0f, drawBack, Space.Self);
            }

            // Sobald vollständig gespannt
            if (!hasDrawnFully && drawTimer >= drawDuration)
            {
                Debug.Log("TIMER VOLL");
                hasDrawnFully = true;
            }
        }

        // Loslassen oder Abbrechen
        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            isDrawing = false;
            animator.SetBool("isDrawing", false);

            // Animation für das Zurücksetzen des Bogens (auch bei schwachem Schuss)
            animator.SetTrigger("Release");

            // Schieß-Logik aufrufen (abgebrochen oder voll aufgeladen)
            HandleShooting(hasDrawnFully);

            // Cooldown aktivieren
            if (hasDrawnFully)
            {
                // Kein Cooldown, wenn vollständig aufgeladen
                currentCooldown = 0f; // Direkt wieder schießen können
            }
            else
            {
                // Cooldown für schwachen Schuss
                currentCooldown = shortCooldown;
            }
        }
    }

    // Diese Methode wird für den normalen und den abgebrochenen Schuss verwendet
    private void HandleShooting(bool isFullDraw)
    {
        // Zerstöre den Pfeil am Bogen sofort
        if (currentArrow != null)
        {
            Destroy(currentArrow);
        }

        // Spawnen des neuen Pfeils an der Kamera-Mitte
        GameObject newArrow = Instantiate(arrowPrefab, Camera.main.transform.position + Camera.main.transform.forward * 0.8f, Quaternion.identity);
        newArrow.transform.rotation = Quaternion.identity; // Nur wenn nötig

        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            // Ray von der Kamera (Viewport-Mitte)
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            Vector3 shootDirection;

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                shootDirection = (hit.point - Camera.main.transform.position).normalized;
            }
            else
            {
                shootDirection = Camera.main.transform.forward; // Richtung entlang der Kamera
            }


            // Setze Rotation mit Quaternion.LookRotation
            newArrow.transform.rotation = Quaternion.LookRotation(shootDirection, Vector3.up);


            // Berechne die Kraft basierend auf der Ziehdauer
            float forceMultiplier = Mathf.Clamp01(drawTimer / drawDuration);
            float arrowForce = maxArrowForce * forceMultiplier;

            // Wenn der Schuss abgebrochen wurde, verwende eine kleinere Kraft
            if (!isFullDraw)
            {
                arrowForce *= 0.5f; // Zum Beispiel nur 30% der vollen Kraft
            }

            // Pfeil abschießen
            rb.AddForce(shootDirection * arrowForce, ForceMode.Impulse);
        }

        // Zerstöre den Pfeil nach einer kurzen Zeit
        Destroy(newArrow, 5f);
    }
}
