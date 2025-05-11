using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float gold;

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
