using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats Instance;

    public float gold;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddGold(float amount)
    {
        gold += amount;
    }

    public bool SpendGold(float amount)
    {
        if(gold >= amount)
        {
            gold -= amount;
            return true;
        }
        return false;
    }


}
