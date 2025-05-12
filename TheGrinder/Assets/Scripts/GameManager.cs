using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerStats playerStats;

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += HandleEnemyKill;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= HandleEnemyKill;
    }

    private void Start()
    {
        if (playerStats == null)
        {
            playerStats = FindFirstObjectByType<PlayerStats>();
            if (playerStats != null)
                Debug.Log("PlayerStats neu gefunden.");
            else
                Debug.LogWarning("PlayerStats konnte nicht gefunden werden!");
        }
    }

    private void Awake()
    {
        // Singleton-Check
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Duplikate verhindern
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Erhalte über Szenenwechsel hinweg

        Debug.Log("Playermoney: " + playerStats.gold);
    }

    private void HandleEnemyKill(float gold)
    {
        if (playerStats != null)
        {
            playerStats.AddGold(gold);
            Debug.Log($"Gold erhalten: {gold} - Neuer Stand: {playerStats.gold}");
        }
        else
        {
            Debug.LogWarning("PlayerStats ist null kein Gold erhalten!");
        }
    }

}
