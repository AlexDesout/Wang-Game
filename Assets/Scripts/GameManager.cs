using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {

    }

    public void DemarrerPartie()
    {
        // Charger la scène de jeu lorsque le bouton "Commencer la partie" est cliqué
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void QuitterJeu()
    {
        // Quitter l'application lorsque le bouton "Quitter le jeu" est cliqué (fonctionne dans un build, pas dans l'éditeur Unity)
        Application.Quit();
    }
}
