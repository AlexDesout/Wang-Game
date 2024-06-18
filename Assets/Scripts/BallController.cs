using UnityEngine;

public class BallColorChangeAndJump : MonoBehaviour
{
    public Color originalColor = Color.white; // Couleur originale de la balle
    public Color grabbedColor = Color.green; // Couleur lorsque la balle est saisie par le joueur
    private Renderer ballRenderer;
    private Rigidbody rb;
    private bool isBeingCarried = false; // Indique si la balle est actuellement tenue par le joueur

    public float jumpForce = 2.0f; // Force du saut de la balle

    void Start()
    {
        // Récupère le Renderer de la balle pour pouvoir changer sa couleur
        ballRenderer = GetComponent<Renderer>();
        // Initialise la couleur à la couleur originale
        ballRenderer.material.color = originalColor;

        // Configure le Rigidbody pour utiliser la gravité
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;
        rb.isKinematic = false;

        // Ajoute un deuxième collider pour la détection du joueur
        SphereCollider detectionCollider = gameObject.AddComponent<SphereCollider>();
        detectionCollider.isTrigger = true;
        detectionCollider.radius = 2.0f; // Ajustez le rayon selon vos besoins
    }

    void Update()
    {
        // Vérifie si la balle est tenue par le joueur
        if (isBeingCarried)
        {
            // Met à jour la couleur de la balle à chaque frame si elle est tenue par le joueur
            ChangeColor(grabbedColor);

            // Exemple : Met en pause la physique de la balle lorsqu'elle est tenue par le joueur
            rb.isKinematic = true;
        }
        else
        {
            // Réactive la physique de la balle si elle n'est pas tenue par le joueur
            rb.isKinematic = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui entre dans le trigger est le joueur (tagué "Player")
        if (other.CompareTag("Player"))
        {
            // Vérifie si le joueur tient la balle
            if (!isBeingCarried)
            {
                // Change la couleur de la balle en vert si le joueur ne la tient pas encore
                ChangeColor(grabbedColor);
                Jump();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Vérifie si l'objet qui sort du trigger est le joueur (tagué "Player")
        if (other.CompareTag("Player"))
        {
            // Restaure la couleur originale de la balle lorsque le joueur cesse de la tenir
            ChangeColor(originalColor);
        }
    }

    public void SetBeingCarried(bool carried)
    {
        isBeingCarried = carried;
    }

    private void ChangeColor(Color color)
    {
        if (ballRenderer != null)
        {
            ballRenderer.material.color = color;
        }
    }

     private void Jump()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}