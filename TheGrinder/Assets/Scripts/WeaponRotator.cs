using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    public Transform cameraRotationSource;

    void LateUpdate()
    {
        Vector3 camEuler = cameraRotationSource.eulerAngles;

        // Nur Y-Rotation folgen
        transform.rotation = Quaternion.Euler(camEuler.x, camEuler.y,0f);
    }
}
