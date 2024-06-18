using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 10f; // Vitesse de rotation normale
    public float rotationSpeedNearPlayer = 20f; // Vitesse de rotation lorsque le joueur est à proximité
    public float detectionRadius = 2.0f; // Rayon de détection pour le joueur
    private bool playerNearby = false; // Indicateur si le joueur est à proximité

    void Start()
    {
        // Ajouter un SphereCollider pour la détection du joueur
        SphereCollider detectionCollider = gameObject.AddComponent<SphereCollider>();
        detectionCollider.isTrigger = true;
        detectionCollider.radius = detectionRadius;
    }

    void Update()
    {
        // Sélectionner la vitesse de rotation en fonction de la proximité du joueur
        float currentRotationSpeed = playerNearby ? rotationSpeedNearPlayer : rotationSpeed;

        // Rotation autour de l'axe Y
        transform.Rotate(Vector3.up * currentRotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur est à proximité, augmenter la vitesse de rotation
            playerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur n'est plus à proximité, réinitialiser la vitesse de rotation
            playerNearby = false;
        }
    }
}
