using UnityEngine;

public class Movimiento : MonoBehaviour
{
    
    // Variables de movimiento del jugador
    [Header("Configuración de Movimiento")]
    [SerializeField] float velocidadx = 5.0f;
    [SerializeField] float velocidady = 6.0f;
    [SerializeField] float movimientox = 0f;
    [SerializeField] float movimientoy = 0f;

    private Rigidbody2D rb;
    private bool tocandoSuelo;
    
    #region Start & Update  
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
        
        //Eje X
        //Utilizo linearVelocity en vez de AddForce para movimiento más preciso
        //EN AMBOS, INCLUYO LA VELOCIDAD LINEAL DEL EJE OPUESTO PARA QUE NO SE PIERDA DURANTE EL MOVIMIENTO
        // Ej: Si salto, mantener la velocidad de movimiento
        rb.linearVelocity = new Vector2(movimientox * velocidadx, rb.linearVelocity.y);
        
        //Eje Y
        // rb.AddForce(new Vector2(0, movimientoy * velocidady), ForceMode2D.Impulse); //ANNTIGUO
        if ( movimientoy!= 0 && tocandoSuelo )
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocidady);

            //LinearVelocity permite ser mas preciso EN ESTE CASO SOLAMENT, pero es recomendable utilizar rb.AddForce para fisicas más realistas
            //https://docs.unity3d.com/6000.1/Documentation/ScriptReference/Rigidbody-linearVelocity.html
        }
    }
    #endregion

    #region Colisiones y salto
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
    #endregion
}
