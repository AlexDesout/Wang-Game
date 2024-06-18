using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 10f; // Vitesse de rotation

    void Update()
    {
        // Rotation autour de l'axe Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
