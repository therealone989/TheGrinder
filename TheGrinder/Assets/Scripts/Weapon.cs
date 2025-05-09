using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] float weaponRange;
    [SerializeField] int weaponDamage = 5;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weaponRange))
        {
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();

            if (hit.collider.tag == "Enemy")
            {
                enemyHealth?.takeDamage(weaponDamage);
            }

            if (hit.collider.tag == "Crit")
            {
                enemyHealth?.takeCritDamage(weaponDamage);
            }
        }
    }
}
