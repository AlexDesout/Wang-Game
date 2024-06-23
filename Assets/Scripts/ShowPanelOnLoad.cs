using UnityEngine;
using System.Collections;

public class ShowPanelOnLoad : MonoBehaviour
{
    public GameObject panel; // Référence à votre panel dans l'inspecteur
    public float displayTime = 5f; // Durée pendant laquelle le panel sera affiché

    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(true); // Afficher le panel au début
            StartCoroutine(HidePanelAfterTime()); // Lancer la coroutine pour masquer le panel après un certain temps
        }
    }

    private IEnumerator HidePanelAfterTime()
    {
        yield return new WaitForSeconds(displayTime); // Attendre la durée spécifiée
        if (panel != null)
        {
            panel.SetActive(false); // Masquer le panel après le délai
        }
    }
}
