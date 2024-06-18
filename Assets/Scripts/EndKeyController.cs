using UnityEngine;

public class EndKeyController : MonoBehaviour
{
    public GameObject panelGameOver; // Référence à votre panel Game Over dans l'inspecteur

    void Start()
    {
        // Assurez-vous que le panel Game Over est désactivé au démarrage
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
        }

        // Ajoute un SphereCollider pour détecter la proximité du joueur
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = 2.0f; // Ajustez le rayon selon vos besoins
    }

    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si le joueur (tag "Player") entre en collision avec le prefab End Key
        if (collision.gameObject.CompareTag("Player"))
        {
            // Activer le panel Game Over
            if (panelGameOver != null)
            {
                panelGameOver.SetActive(true);
            }
        }
    }
}
