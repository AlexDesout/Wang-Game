using UnityEngine;
using UnityEngine.SceneManagement;

public class LightIntensityController : MonoBehaviour
{
    public Light directionalLight; // Référence à la lumière directionnelle
    public float intensityDecreaseAmount = 0.1f; // Quantité de diminution de l'intensité par mort
    private float currentIntensity;

    void Start()
    {
        if (directionalLight != null)
        {
            currentIntensity = directionalLight.intensity;
        }
    }

    public void DecreaseIntensity()
    {
        if (directionalLight != null)
        {
            currentIntensity -= intensityDecreaseAmount;
            if (currentIntensity <= -1)
            {
                currentIntensity = -1;
                ResetGame();
            }
            directionalLight.intensity = currentIntensity;
        }
    }

    private void ResetGame()
    {
        // Réinitialiser la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
