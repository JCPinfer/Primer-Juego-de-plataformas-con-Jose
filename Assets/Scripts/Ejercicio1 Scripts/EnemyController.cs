using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // [SerializeField] private Movimiento player; // PlayerController seria más adecuado, en mi caso de momento Movimiento es un "Controller" de player
    // EN CASO DE ELEMENTO NO ESTATICO
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        Movimiento.OnColisionConEnemigo += tratarChoqueConPlayer;

    }

    private void OnDisable()
    {
        
    }

    private void tratarChoqueConPlayer()
    {
        Debug.Log("Recibo evento del player");
    }
}
