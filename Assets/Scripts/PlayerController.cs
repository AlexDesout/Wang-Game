using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveValue;
    private bool isCrouching = false;
    private Vector3 startPosition;

    public float speed = 5f;
    public float jumpForce = 10f;
    public float wallJumpForce = 7f; // Ajout de la déclaration de wallJumpForce
    private bool isJumping = false;
    private bool canJumpFromGround = false;
    private bool canWallJump = false;
    private Vector3 wallJumpDirection;

    private LightIntensityController lightController; // Référence au contrôleur de luminosité

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wallJumpForce = 7f; // Initialisation de wallJumpForce
        startPosition = transform.position;
        lightController = FindObjectOfType<LightIntensityController>(); // Trouver le contrôleur de luminosité dans la scène
    }

    void FixedUpdate()
    {
        // Déplacement horizontal uniquement dans le plan X-Z
        float horizontalMovement = moveValue.x * speed;
        rb.velocity = new Vector3(horizontalMovement, rb.velocity.y, rb.velocity.z);

        // Saut
        if (isJumping)
        {
            if (canJumpFromGround)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJumping = false;
                canJumpFromGround = false;
            }
            else if (canWallJump)
            {
                rb.AddForce(Vector3.up * jumpForce + wallJumpDirection * wallJumpForce, ForceMode.Impulse);
                isJumping = false;
                canWallJump = false;
            }
        }
    }

    public void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
        // Vérifier si la flèche du bas est appuyée
        isCrouching = moveValue.y < -0.5f;
    }

    public void OnJump(InputValue value)
    {
        isJumping = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("sol") || collision.gameObject.CompareTag("Plateforme"))
        {
            canJumpFromGround = true;
            canWallJump = false; // Réinitialiser le saut depuis le mur lorsque sur le sol ou une plateforme
        }
        if (collision.gameObject.CompareTag("mur"))
        {
            // Déterminer la direction du wall jump en fonction de la position du mur par rapport au joueur
            Vector3 contactNormal = collision.contacts[0].normal;
            if (contactNormal.x != 0) // On touche un mur vertical
            {
                canWallJump = true;
                wallJumpDirection = contactNormal;
            }
        }

        // Détection des pics et gestion de la mort du joueur
        if (collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("coucou");
            Die(); // Méthode à implémenter pour gérer la mort du joueur
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plateforme"))
        {
            // Vérifier si le joueur est en dessous de la plateforme ou appuie sur la flèche du bas
            if (isCrouching || transform.position.y < collision.transform.position.y)
            {
                Collider platformCollider = collision.collider;
                StartCoroutine(EnableTriggerTemporarily(platformCollider));
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("sol") || collision.gameObject.CompareTag("Plateforme"))
        {
            canJumpFromGround = false;
        }
        if (collision.gameObject.CompareTag("mur"))
        {
            canWallJump = false;
        }
    }

    private IEnumerator EnableTriggerTemporarily(Collider platformCollider)
    {
        platformCollider.isTrigger = true;
        yield return new WaitForSeconds(0.5f); // Durée pendant laquelle le collider est un trigger
        platformCollider.isTrigger = false;
    }

    private void Die()
    {
        // Actions à effectuer lorsque le joueur meurt
        Debug.Log("Le joueur est mort !");

        // Téléportation du joueur à la position de départ
        transform.position = startPosition;

        // Réinitialisation d'autres paramètres si nécessaire
        rb.velocity = Vector3.zero; // Réinitialisation de la vitesse
        isJumping = false; // Arrêt du saut

        // Assombrir l'écran à chaque mort
        if (lightController != null)
        {
            lightController.DecreaseIntensity();
        }
    }
}
