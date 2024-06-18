using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LightIntensityController : MonoBehaviour
{
    public Light directionalLight; // Référence à la lumière directionnelle
    public float intensityDecreaseAmount = 0.1f; // Quantité de diminution de l'intensité par mort
    public GameObject panel_game_over; // Assignez ici votre panel Game Over via l'inspecteur
    public float delayBeforeTeleport = 2f; // Délai avant de téléporter vers le niveau 1
    private float currentIntensity;
    private bool gameOver = false;
    private bool movementAllowed = true; // Indique si le mouvement du joueur est autorisé

    void Start()
    {
        if (directionalLight != null)
        {
            currentIntensity = directionalLight.intensity;
        }

        if (panel_game_over != null)
        {
            panel_game_over.SetActive(false); // Assurez-vous que le panel "Game Over" est désactivé au démarrage
        }
    }

    public void DecreaseIntensity()
    {
        if (movementAllowed && directionalLight != null)
        {
            currentIntensity -= intensityDecreaseAmount;
            if (currentIntensity <= -1)
            {
                currentIntensity = -1;
                StartCoroutine(TeleportWithGameOver());
            }
            directionalLight.intensity = currentIntensity;
        }
    }

    private IEnumerator TeleportWithGameOver()
    {
        movementAllowed = false; // Désactiver le mouvement du joueur

        if (panel_game_over != null)
        {
            panel_game_over.SetActive(true); // Afficher le panel "Game Over"
            gameOver = true; // Marquer le jeu comme étant en état de "Game Over"
        }

        // Arrêter le mouvement du personnage (à adapter selon votre script de mouvement)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Arrêter la vitesse du Rigidbody
        }

        yield return new WaitForSeconds(delayBeforeTeleport); // Attendre un certain temps avant la téléportation

        TeleportToLevel1(); // Charger la scène "Lvl1"
    }

    private void TeleportToLevel1()
    {
        // Charger la scène "Lvl1"
        SceneManager.LoadScene("Lvl1");
    }
}
