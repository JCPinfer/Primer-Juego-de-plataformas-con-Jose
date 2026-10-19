using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaJuego : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI panelMonedas;
    [SerializeField] TextMeshProUGUI panelCuidado;
    private Vector2 posicionInicial;
    private int monedas;
    
    void Start()
    {
        //https://discussions.unity.com/t/how-can-you-enable-disable-textmeshpro-text-in-a-script/743447
        panelCuidado.enabled = false;
        monedas = 0;
        
        /*
        //Asigno posicion 
        //https://discussions.unity.com/t/how-can-i-make-my-object-go-back-to-its-start-position/181539
        posicionInicial = this.transform.position;
        */
    }
    void Update()
    {
        
    }

    #region Colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Moneda")
        {
            collision.gameObject.SetActive(false);
            monedas++;
            panelMonedas.text = "Monedas: " + monedas.ToString();
        }

        if (collision.gameObject.CompareTag("DamagePlayer"))
        {

            Debug.Log("Ha tocado pincho");
            //this.transform.position = posicionInicial ;
            SceneManager.LoadScene(Scene.GetActiveScene().buildIndex); //FIX
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AbismoTrigger"))
        {
            panelCuidado.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
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

    #region TO DO
    /*
    TODO:
    - Investigar sobre: Time.timeScale= 0f;
    */
    #endregion
}