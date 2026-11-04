using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Ejercicio1");
    }
    public void Salir()
    {
        Application.Quit();
    }

}
