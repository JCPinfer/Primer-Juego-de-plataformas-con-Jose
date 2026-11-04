using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private AudioSource fuenteSFX;

    [SerializeField] private AudioClip menuMusic;
    void Start()
    {
        fuenteSFX.PlayOneShot(menuMusic);
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
