using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject Panel;
    private bool visible = false;

    // Start is called before the first frame update
    void Start()
    {
        // Assurez-vous que le panneau est désactivé au démarrage
        Panel.SetActive(visible);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            visible = !visible;
            Panel.SetActive(visible);
        }
    }
}
