using UnityEngine;

public class Movimiento : MonoBehaviour
{
    
    // Variables de movimiento del jugador
    [Header("Configuración de Movimiento")]
    [SerializeField] float velocidadx = 5.0f;
    [SerializeField] float velocidady = 6.0f;
    [SerializeField] float movimientox = 0f;
    [SerializeField] float movimientoy = 0f;

    //DOBLE SALTO
    [Header("Doble Salto")]
    public bool tieneDobleSalto;
    [SerializeField] float tiempoDobleSalto;
    public float temporizadorDobleSalto;

    [Header("SFX")]
    [SerializeField] private AudioClip saltoSonido;
    [SerializeField] private AudioSource fuenteSalto;

    private Rigidbody2D rb;
    private bool tocandoSuelo;
    
    #region Start & Update  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tieneDobleSalto = true;
        temporizadorDobleSalto = 0f;
    }

    void Update()
    {
        // RAW devuelve inmediatamente a 0 al soltar, en vez de bajar gradualmente como el GetAxis simples.
        // Se esta utilizando UNICAMENTE para la fricción, se corregirá en la implementación del nuevo input
        movimientox = Input.GetAxisRaw("Horizontal");
        movimientoy = Input.GetAxis("Vertical");
        
        if (!tocandoSuelo)
            movimientoy = 0f;

        //Eje X
        //Utilizo linearVelocity en vez de AddForce para movimiento más preciso
        //EN AMBOS, INCLUYO LA VELOCIDAD LINEAL DEL EJE OPUESTO PARA QUE NO SE PIERDA DURANTE EL MOVIMIENTO
        // Ej: Si salto, mantener la velocidad de movimiento
        
        // Si no hay input, no tocamos la velocidad X para que la fricción pueda actuar
        if (movimientox != 0)
        {
            rb.linearVelocity = new Vector2(movimientox * velocidadx, rb.linearVelocity.y);
        }

        //Eje Y
        //Pongo el primero para que pueda saltar solamente con W. Solución guarra de momento

        //Caso tenga doble salto, permito que el jugador realize UNO solo. Este bool se resetea 
        
        //Timer de doble salto
        if (!tocandoSuelo && temporizadorDobleSalto > 0f)
        {
            temporizadorDobleSalto -= Time.deltaTime;
        }
        
        
        if ( Input.GetKeyDown(KeyCode.W) &&  movimientoy!= 0 && tocandoSuelo  )
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocidady);
            fuenteSalto.PlayOneShot(saltoSonido);

            //LinearVelocity permite ser mas preciso EN ESTE CASO SOLAMENT, pero es recomendable utilizar rb.AddForce para fisicas más realistas
            //https://docs.unity3d.com/6000.1/Documentation/ScriptReference/Rigidbody-linearVelocity.html

            tieneDobleSalto = true;

        }
        else if ( Input.GetKeyDown(KeyCode.W) && tieneDobleSalto && temporizadorDobleSalto<=0 )
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocidady);
            fuenteSalto.PlayOneShot(saltoSonido);
            tieneDobleSalto = false;
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

            temporizadorDobleSalto = tiempoDobleSalto;
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
