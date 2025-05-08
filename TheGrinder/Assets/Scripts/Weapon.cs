using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] float weaponRange;

    private bool db = true;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weaponRange))
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }
}
