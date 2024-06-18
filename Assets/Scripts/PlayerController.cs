using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveValue;
    private bool isCrouching = false;
    private Vector3 startPosition;
    private GameObject carriedSphere = null; // Référence à la sphère transportée par le joueur

    public float speed = 5f;
    public float jumpForce = 10f;
    private bool isJumping = false;
    private bool canJumpFromGround = false;
    private bool canWallJump = false;
    private Vector3 wallJumpDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
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
                rb.AddForce(Vector3.up * jumpForce + wallJumpDirection * jumpForce, ForceMode.Impulse);
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

    public void OnGrab(InputValue value)
    {
        if (carriedSphere != null)
        {
            // Si une sphère est déjà transportée, lâcher la sphère
            carriedSphere.GetComponent<BallColorChangeAndJump>().SetBeingCarried(false);
            carriedSphere = null;
        }
        else
        {
            // Détecter les sphères à proximité
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f); // Rayon de détection de 2 unités
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Ball"))
                {
                    carriedSphere = hitCollider.gameObject;
                    carriedSphere.GetComponent<BallColorChangeAndJump>().SetBeingCarried(true); // Marquer la balle comme tenue par le joueur
                    carriedSphere.transform.SetParent(transform); // Attacher la sphère au joueur
                    carriedSphere.transform.localPosition = new Vector3(0, 2f, 0); // Placer la sphère au-dessus du joueur
                    break;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("sol"))
        {
            canJumpFromGround = true;
            canWallJump = false; // Réinitialiser le saut depuis le mur lorsque sur le sol
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
            Die(); // Méthode à implémenter pour gérer la mort du joueur
        }
    }

    // void OnCollisionStay(Collision collision)
    // {
    //     // Vérifier si le joueur est sur une plateforme et peut sauter en dessous
    //     if (collision.gameObject.CompareTag("Plateforme"))
    //     {
    //         if (isCrouching || transform.position.y < collision.transform.position.y)
    //         {
    //             Collider platformCollider = collision.collider;
    //             StartCoroutine(EnableTriggerTemporarily(platformCollider));
    //         }
    //     }
    // }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("sol"))
        {
            canJumpFromGround = false;
        }
        if (collision.gameObject.CompareTag("mur"))
        {
            canWallJump = false;
        }
    }

    // private IEnumerator EnableTriggerTemporarily(Collider platformCollider)
    // {
    //     platformCollider.isTrigger = true;
    //     yield return new WaitForSeconds(0.5f); // Durée pendant laquelle le collider est un trigger
    //     platformCollider.isTrigger = false;
    // }

    private void Die()
    {
        // Actions à effectuer lorsque le joueur meurt
        Debug.Log("Le joueur est mort !");

        // Téléportation du joueur à la position de départ
        transform.position = startPosition;

        // Réinitialisation d'autres paramètres si nécessaire
        rb.velocity = Vector3.zero; // Réinitialisation de la vitesse
        isJumping = false; // Arrêt du saut
        // Autres actions nécessaires à la gestion de la mort du joueur
    }
}