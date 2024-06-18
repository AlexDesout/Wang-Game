using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportOnKey : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("key"))
        {
            // Charger la scène spécifiée
            SceneManager.LoadScene("SampleScene");
        }
    }
}
