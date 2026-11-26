using TMPro;
using UnityEngine;

public class uicontroller : MonoBehaviour
{
    //[SerializeField] private Movimiento player; // PlayerController seria más adecuado, en mi caso de momento Movimiento es un "Controller" de player
    [SerializeField] private TextMeshProUGUI textoPrueba;
    void Start()
    {
        textoPrueba.text = ("");
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        Movimiento.OnColisionConEnemigo += ActualizarUI;

    }

    private void OnDisable()
    {
        
    }

    private void ActualizarUI()
    {
        textoPrueba.text = ("Tocaste al enemigo");
        Debug.Log("Recibo evento del player");
    }
}
