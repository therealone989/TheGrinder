using UnityEngine;

public class BowController : MonoBehaviour
{
    private Animator animator;

    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;

    private GameObject currentArrow;

    private bool isDrawing = false;
    private bool hasDrawnFully = false;
    public float drawDuration = 1.5f;
    private float drawTimer = 0f;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip releaseClip;
    [SerializeField] private AudioClip loadClip;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            hasDrawnFully = false;
            drawTimer = 0f;
            animator.SetBool("isDrawing", true);

            // Ladegeräusch abspielen (Loop)
            if (loadClip != null && audioSource != null)
            {
                audioSource.clip = loadClip;
                audioSource.loop = false;
                audioSource.Play();
            }

            // Zeigepfeil zum Visuellen Spannen anzeigen
            currentArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            currentArrow.transform.SetParent(arrowSpawnPoint);
        }

        if (isDrawing)
        {
            drawTimer += Time.deltaTime;

            if (currentArrow != null)
            {
                currentArrow.transform.position = arrowSpawnPoint.position;
                currentArrow.transform.rotation = arrowSpawnPoint.rotation;
                Rigidbody currentArrowRb = currentArrow.GetComponent<Rigidbody>();
                //Destroy(currentArrowRb);
                currentArrow.GetComponent<Collider>().enabled = false;

                float drawBack = Mathf.Lerp(0f, -0.9f, Mathf.Clamp01(drawTimer / drawDuration));
                currentArrow.transform.Translate(0f, 0f, drawBack, Space.Self);
            }

            if (!hasDrawnFully && drawTimer >= drawDuration)
            {
                hasDrawnFully = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            isDrawing = false;
            animator.SetBool("isDrawing", false);
            animator.SetTrigger("Release");

            // Ladegeräusch stoppen
            if (audioSource != null && audioSource.clip == loadClip)
            {
                //audioSource.Stop();
                audioSource.clip = null;
                audioSource.loop = false;
            }

            // Pfeil visuell entfernen
            if (currentArrow != null)
            {
                Destroy(currentArrow);
            }

            if (hasDrawnFully)
            {
                ShootArrow();
            }
        }
    }

    private void ShootArrow()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.7f;
        Quaternion spawnRotation = Quaternion.LookRotation(Camera.main.transform.forward, Vector3.up);
        GameObject newArrow = Instantiate(arrowPrefab, spawnPosition, spawnRotation);

        if (releaseClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(releaseClip);
        }

        // BULLET DROP IMPLEMENTATION ?
        //Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        //if (rb != null)
        //{
        //    rb.isKinematic = false;
        //    rb.useGravity = true;
        //    Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        //    newArrow.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Vector3.up);
        //    rb.AddForce(shootDirection * arrowForce, ForceMode.Impulse);
        //}
    }
}
