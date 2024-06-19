using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // Cette méthode peut être vide si vous n'avez rien à initialiser
    }

    public void DemarrerPartie()
    {
        // Charger la scène de jeu "Level1" lorsque le bouton "Commencer la partie" est cliqué
        SceneManager.LoadScene("Lvl1", LoadSceneMode.Single);
    }

    public void QuitterJeu()
    {
        // Quitter l'application lorsque le bouton "Quitter le jeu" est cliqué (fonctionne dans un build, pas dans l'éditeur Unity)
        Application.Quit();
        Debug.Log("Je quite le jeu");
    }
}
