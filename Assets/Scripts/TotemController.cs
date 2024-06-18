using UnityEngine;

public class TotemController : MonoBehaviour
{
    public Color activatedColor = Color.blue; // Couleur lorsque le totem est activé par une balle
    public Color deactivatedColor = Color.white; // Couleur par défaut du totem
    public float detectionRadius = 2.0f; // Rayon de détection pour les balles
    public PlatformController associatedPlatform; // Référence à la plateforme associée

    private Renderer totemRenderer;
    private SphereCollider detectionCollider;

    void Start()
    {
        // Récupère le Renderer du totem pour pouvoir changer sa couleur
        totemRenderer = GetComponent<Renderer>();
        // Initialise la couleur à la couleur par défaut
        totemRenderer.material.color = deactivatedColor;

        // Ajoute un SphereCollider pour la détection des balles
        detectionCollider = gameObject.AddComponent<SphereCollider>();
        detectionCollider.isTrigger = true;
        detectionCollider.radius = detectionRadius;
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui entre dans le trigger est une balle (taguée "Ball")
        if (other.CompareTag("Ball"))
        {
            // Change la couleur du totem lorsqu'une balle est à proximité
            totemRenderer.material.color = activatedColor;

            // Activer la plateforme associée si elle est définie
            if (associatedPlatform != null)
            {
                associatedPlatform.ActivateMovement();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Vérifie si l'objet qui sort du trigger est une balle (taguée "Ball")
        if (other.CompareTag("Ball"))
        {
            // Rétablit la couleur par défaut du totem lorsque la balle s'éloigne
            totemRenderer.material.color = deactivatedColor;

            if (associatedPlatform != null)
            {
                associatedPlatform.DeactivateMovement();
            }

        }
    }
}