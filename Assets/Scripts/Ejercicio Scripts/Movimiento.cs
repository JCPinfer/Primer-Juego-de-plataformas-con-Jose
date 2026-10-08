using UnityEngine;

public class Movimiento : MonoBehaviour
{
    // Variables de movimiento del jugador
    [SerializeField] float velocidad = 5.0f;
    [SerializeField] float movimientox = 0f;
    [SerializeField] float movimientoy = 0f;
    private Rigidbody2D rb;
    private bool tocandoSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movimientox = Input.GetAxis("Horizontal");
        movimientoy = Input.GetAxis("Vertical");
        
        if (!tocandoSuelo)
            movimientoy = 0f;
        
        rb.AddForce(new Vector2(movimientox * velocidad, movimientoy * velocidad), ForceMode2D.Impulse);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataformas")
        {
            tocandoSuelo = true;
            Debug.Log(tocandoSuelo);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataformas") 
        {
            tocandoSuelo = false;
            Debug.Log(tocandoSuelo);
        }
    }


}
