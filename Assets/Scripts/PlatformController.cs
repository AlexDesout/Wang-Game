using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public bool isActive = false; // État actif ou inactif de la plateforme
    public float moveSpeed = 1.0f; // Vitesse de déplacement constante de la plateforme
    public float moveDistance = 5.0f; // Distance de déplacement de la plateforme
    public bool moveOnXAxis = true; // Indique si la plateforme se déplace sur l'axe X

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        initialPosition = transform.position;

        // Calculer la position cible en fonction de l'axe de déplacement
        if (moveOnXAxis)
            targetPosition = initialPosition + Vector3.right * moveDistance;
        else
            targetPosition = initialPosition + Vector3.up * moveDistance;
    }

    void Update()
    {
        if (isActive)
        {
            // Déplacer la plateforme en utilisant Lerp pour une vitesse constante
            float t = Mathf.PingPong(Time.time * moveSpeed / moveDistance, 1.0f);
            Vector3 newPosition = Vector3.Lerp(initialPosition, targetPosition, t);

            // Déplacer la plateforme uniquement si elle est active
            transform.position = newPosition;
        }
    }

    // Méthode pour activer le mouvement de la plateforme
    public void ActivateMovement()
    {
        isActive = true;
    }

    // Méthode pour désactiver le mouvement de la plateforme
    public void DeactivateMovement()
    {
        isActive = false;
    }
}