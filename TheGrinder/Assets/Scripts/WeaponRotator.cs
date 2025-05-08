using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    public Transform cameraRotationSoruce;

    private void LateUpdate()
    {
        transform.rotation = cameraRotationSoruce.rotation;
    }
}
