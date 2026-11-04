using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    [SerializeField] GameObject pantallaVictoria;

    //InGame
    [Header("Variables In-Game")]
    [SerializeField] GameObject puerta1;
    [SerializeField] GameObject puertaFinal;

    private Vector2 posicionInicial;
    private int monedas;
    [SerializeField] private int maxMonedas = 15 ;
    [SerializeField] Slider barraVida;
    [SerializeField] int vidas;

    //VARIABLES DE ESTADO
    private bool vulnerable;
    private Movimiento movimientoJugador;
    
    [Header("Invulnerabilidad")]
    [SerializeField] float duracionInvulnerabilidad = 1.5f;
    [SerializeField] float intervaloParpadeo = 0.3f;
    private SpriteRenderer spriteRenderer;

    [Header("SFX")]
    [SerializeField] private AudioSource fuenteMusica;
    [SerializeField] private AudioSource fuenteSFX;

    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip danioSonido;
    [SerializeField] private AudioClip gameOverSonido;
    [SerializeField] private AudioClip puertaSonido;
    [SerializeField] private AudioClip monedaSonido;
    [SerializeField] private AudioClip pisarEnemigoSonido;
    [SerializeField] private AudioClip victoriaSonido;


    [Range(0f, 1f)] [SerializeField] private float volumenMusica = 0.4f;
    [Range(0f, 1f)] [SerializeField] private float volumenSFX = 1f;

    void Start()
    {
        Time.timeScale = 1.0f;
        
        //https://discussions.unity.com/t/how-can-you-enable-disable-textmeshpro-text-in-a-script/743447
        panelCuidado.enabled = false;
        monedas = 0;
        interfazJuego.SetActive(true);
        
        pantallaPausa.SetActive(false);
        pantallaVictoria.SetActive(false);

        panelVidas.text = "Vidas: " + vidas.ToString(); 
        
        //Slider de vida
        barraVida.maxValue = vidas;
        barraVida.value = vidas;
        
        //Asigno posicion 
        //https://discussions.unity.com/t/how-can-i-make-my-object-go-back-to-its-start-position/181539
        posicionInicial = this.transform.position;

        vulnerable = true;

        pantallaGameOver.SetActive(false);

        panelDobleSalto.text = "";
        
        movimientoJugador = GetComponent<Movimiento>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        //Audio
        fuenteMusica.clip = gameMusic;
        fuenteMusica.loop = true;
        fuenteMusica.volume = volumenMusica;
        fuenteMusica.Play();

        fuenteSFX.volume = volumenSFX;

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

        if (monedas == maxMonedas)
        {
            puertaFinal.SetActive(false);
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
            spriteRenderer.enabled = !spriteRenderer.enabled; //Iguala al estado contrario sin necesidad de variable auxiliar

            //Indica que tiene que tiene que pausar temporalmente aqui
            yield return new WaitForSeconds(intervaloParpadeo);
            tiempoTranscurrido += intervaloParpadeo;
        }

        // Vuelve todo a la normalidad
        spriteRenderer.enabled = true;
        vulnerable = true;
    }

    private void DamagePlayer()
    {
        //Verifico si esta en estado de invulnerabilidad. En caso que si este, ignoro el resto
        if (!vulnerable)
            return;

        vulnerable = false;

        vidas--;

        fuenteSFX.PlayOneShot(danioSonido);
        
        barraVida.value = vidas;


        if (vidas >= 0)
        {
            panelVidas.text = "Vidas: " + vidas.ToString();
            this.transform.position = posicionInicial;
            //CORRUTINA : PERMITO QUE UN MÉTODO PUEDA PARAR EN SU EJECUCIÓN Y RENAUDARSE MÁS TARDE
            StartCoroutine(InvulnerabilidadParpadeo());
        }
        else
        {
            fuenteMusica.Stop();
            pantallaGameOver.SetActive(true);
            fuenteSFX.PlayOneShot(gameOverSonido);
        }
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
            Debug.Log("Ha tocado pincho");
            DamagePlayer();
            
        }

        if (collision.gameObject.CompareTag("Enemigo"))
{
            Vector2 normal = collision.GetContact(0).normal;

        // Calculo la Normal. Si es horizontal, el jugandor toma daño. SI no, destuye el enemigo
        if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                DamagePlayer();
            }
        else
            {
                fuenteSFX.PlayOneShot(pisarEnemigoSonido);
                Destroy(collision.gameObject);  // Pisas al enemigo: lo eliminas
            }
        }

        /*
        if (collision.gameObject.CompareTag("KeyObject"))
        {
            collision.gameObject.SetActive(false);
            puerta1.SetActive(false);
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
            fuenteSFX.PlayOneShot(monedaSonido);
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

        if (collision.gameObject.CompareTag("TriggerPuertaFinal"))
        {
            panelCuidado.text = "Consigue más monedas para continuar.";
            panelCuidado.enabled = true;
        } 

        if (collision.gameObject.CompareTag("TriggerVictoria"))
        {
            GanarJuego();

            //panelCuidado.text = "¡Has ganado!";
            //panelCuidado.enabled = true;
        }

        //KEY
        if (collision.gameObject.CompareTag("KeyObject"))
        {
            fuenteSFX.PlayOneShot(puertaSonido);
            collision.gameObject.SetActive(false);
            puerta1.SetActive(false);
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
        {
            panelCuidado.enabled = false;
        }

        if (collision.gameObject.CompareTag("TriggerPuertaFinal"))
        {
            panelCuidado.enabled = false;
        }

        if (collision.gameObject.CompareTag("TriggerVictoria"))
        {
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

    private void GanarJuego()
    {
        Time.timeScale = 0f;
        pantallaVictoria.SetActive(true);
        fuenteSFX.PlayOneShot(victoriaSonido);
    }
}