using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float velocidad = 5.0f; // Protege la variable para que no sea modificada desde fuera
    [SerializeField] float movimientox = 0f;
    [SerializeField] float movimientoy = 0f;
    [SerializeField] TextMeshProUGUI panelPuntuacion;
    [SerializeField] TextMeshProUGUI panelTiempo;
    private Rigidbody2D rb;
    private bool tocandoSuelo = false;
    public int puntuacion = 0;
    private float tiempoTotal = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Coge el RigidBody asociado al script SI EXISTE y se lo asocia a la variable 'rb'
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //En cada ciclo del juego veo si hay que mover el player
        movimientox = Input.GetAxis("Horizontal");
        movimientoy = Input.GetAxis("Vertical");

        if (!tocandoSuelo)
            movimientoy = 0f;

        //rb.AddForce( new Vector2(movimientox * velocidad, 0));
        //rb.AddForce( new Vector2(0, movimientoy * velocidad), ForceMode2D.Impulse);

        //ESTO EQUIVALE A LO ANTERIOR, PERO EN UNA SOLA LINEA
        rb.AddForce(new Vector2(movimientox * 1/5, movimientoy * velocidad), ForceMode2D.Impulse);

        // Aqui, controlo el tiempo pasado
        tiempoTotal = tiempoTotal + Time.deltaTime;
        panelTiempo.text = tiempoTotal.ToString("F1");

        //Debug.Log(tiempoTotal);
    }

    /*
     * MÉTODO PARA DETECTAR COLISIONES
     * Parameter: colision
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bolita") {

            puntuacion += 1;
            //panelPuntuacion.text = "El panel furula :)";
            panelPuntuacion.text = "Puntuación : " + puntuacion.ToString();

            collision.gameObject.SetActive(false); // Lo unico que hace es DESACTIVAR con lo interactua, no lo borra de memoria como tal
            //Si quisiera que se borre en memoria, hacer un Destroy
            //Destroy(collision.gameObject);
            
            Debug.Log("Puntuación: " + puntuacion);
        }

        if (collision.gameObject.tag == "Plataformas")
        {
            tocandoSuelo = true;
            Debug.Log(tocandoSuelo);
        }

        //Debug.Log("Ha colisionado el player");
    }

    /*
     * MÉTODO PARA DETECTAR FIN DE COLISION
     * Parameter: colision
     */
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataformas")
        {
            tocandoSuelo = false;
            Debug.Log(tocandoSuelo);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Moneda"))
            Debug.Log("Detecta choque con trigger");
        else if (collision.gameObject.CompareTag("ParedInvisible"))
            Debug.Log("Detecta choque con pared");

        if (collision.gameObject.CompareTag("Plataformas"))
        {
            Debug.Log("He detectado choque con el suelo");
            tocandoSuelo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Moneda"))
            Debug.Log("Detecta que salgo del trigger");

    }

    //Este salta continuamente mientras no se salga del trigger
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Moneda"))
            Debug.Log("Detecta dentro del trigger");

    }

}
