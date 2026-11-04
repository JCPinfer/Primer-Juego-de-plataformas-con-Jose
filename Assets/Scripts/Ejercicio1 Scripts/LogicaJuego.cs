using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogicaJuego : MonoBehaviour
{
    //Interfaz
    [Header("Interfaz")]
    [SerializeField] GameObject pantallaGameOver;
    [SerializeField] TextMeshProUGUI panelMonedas;
    [SerializeField] TextMeshProUGUI panelCuidado;
    [SerializeField] TextMeshProUGUI panelVidas;
    [SerializeField] TextMeshProUGUI panelDobleSalto;
    
    [SerializeField] GameObject interfazJuego;
    [SerializeField] GameObject pantallaPausa;
    //InGame
    [Header("Variables In-Game")]
    [SerializeField] GameObject puerta;
    private Vector2 posicionInicial;
    private int monedas;
    [SerializeField] int vidas;

    //VARIABLES DE ESTADO
    private bool vulnerable;
    private Movimiento movimientoJugador;
    
    [Header("Invulnerabilidad")]
    [SerializeField] float duracionInvulnerabilidad = 1.5f;
    [SerializeField] float intervaloParpadeo = 0.3f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        Time.timeScale = 1.0f;
        
        //https://discussions.unity.com/t/how-can-you-enable-disable-textmeshpro-text-in-a-script/743447
        panelCuidado.enabled = false;
        monedas = 0;
        interfazJuego.SetActive(true);
        
        pantallaPausa.SetActive(false);

        panelVidas.text = "Vidas: " + vidas.ToString(); 
        
        //Asigno posicion 
        //https://discussions.unity.com/t/how-can-i-make-my-object-go-back-to-its-start-position/181539
        posicionInicial = this.transform.position;

        vulnerable = true;

        pantallaGameOver.SetActive(false);

        panelDobleSalto.text = "";
        
        movimientoJugador = GetComponent<Movimiento>();

        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    void Update()
    {
        //Lógica de pausar el juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            pantallaPausa.SetActive(true);
        }

        if(movimientoJugador.tieneDobleSalto == true)
        {
            panelDobleSalto.text = "Tienes doble salto";
        }
        else
        {
            //panelDobleSalto.text = movimientoJugador.temporizadorDobleSalto.ToString("F2");
            panelDobleSalto.text = "";
        }
    }

    private IEnumerator InvulnerabilidadParpadeo()
    {
        // DOCUMENTACIÓN OFICIAL SOBRE CORRUTINAS:
        // https://docs.unity3d.com/es/2018.4/Manual/Coroutines.html
        
        vulnerable = false;

        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionInvulnerabilidad)
        {
            // Alterna entre visible e invisible
            spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(intervaloParpadeo);
            tiempoTranscurrido += intervaloParpadeo;
        }

        // Vuelve todo a la normalidad
        spriteRenderer.enabled = true;
        vulnerable = true;
    }

    #region Colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if(collision.gameObject.tag == "Moneda")
        {
            collision.gameObject.SetActive(false);
            monedas++;
            panelMonedas.text = "Monedas: " + monedas.ToString();
        }*/

        if (collision.gameObject.CompareTag("DamagePlayer"))
        {
            //Verifico si esta en estado de invulnerabilidad. En caso que si este, ignoro el resto
            if(!vulnerable)
                return;  
            
            vulnerable = false;
            Debug.Log("Ha tocado pincho");

            vidas--;

            if(vidas>=0)
            {
                panelVidas.text = "Vidas: " + vidas.ToString(); 
                this.transform.position = posicionInicial;

                //CORRUTINA : PERMITO QUE UN MÉTODO PUEDA PARAR EN SU EJECUCIÓN Y RENAUDARSE MÁS TARDE
                StartCoroutine(InvulnerabilidadParpadeo());
                //vulnerable = true ;
            }
            else
                pantallaGameOver.SetActive(true);
            
        }
        /*
        if (collision.gameObject.CompareTag("KeyObject"))
        {
            collision.gameObject.SetActive(false);
            puerta.SetActive(false);
        }
        */
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //COIN LOGIC
        if(collision.gameObject.tag == "Moneda")
        {
            collision.gameObject.SetActive(false);
            monedas++;
            panelMonedas.text = "Monedas: " + monedas.ToString();
        }

        //TRIGGERS AVISO PANTALL
        if (collision.gameObject.CompareTag("AbismoTrigger"))
        {
            panelCuidado.text = "¡¡ABISMO!!";
            panelCuidado.enabled = true;
        }
        
        if (collision.gameObject.CompareTag("TriggerCubo"))
        {
            panelCuidado.enabled = true;
            panelCuidado.text = "¡Empuja el cubo!";
        }

        if (collision.gameObject.CompareTag("Trigger rebote"))
        {
            panelCuidado.text = "Las plataformas amarillas rebotan. Utilizalas para llegar más alto.";
            panelCuidado.enabled = true;
        }

        if (collision.gameObject.CompareTag("TriggerHielo"))
        {
            panelCuidado.text = "El hielo es resbaladizo, ¡Cuidado!";
            panelCuidado.enabled = true;
        }

        //KEY
        if (collision.gameObject.CompareTag("KeyObject"))
        {
            collision.gameObject.SetActive(false);
            puerta.SetActive(false);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AbismoTrigger"))
        {
            panelCuidado.enabled = false;
        }

        if (collision.gameObject.CompareTag("TriggerCubo"))
        {
            panelCuidado.enabled = false;
        }

         if (collision.gameObject.CompareTag("Trigger rebote"))
        {
            panelCuidado.enabled = false;
        }

        if (collision.gameObject.CompareTag("TriggerHielo"))
        {;
            panelCuidado.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("AbismoTrigger"))
        {
            panelCuidado.enabled = true;
        }*/
    }

    #endregion


    public void reiniciarPartida()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #region BOTONES

    #endregion

    public void ContinuarPartida()
    {
        pantallaPausa.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu Principal");
    }
}