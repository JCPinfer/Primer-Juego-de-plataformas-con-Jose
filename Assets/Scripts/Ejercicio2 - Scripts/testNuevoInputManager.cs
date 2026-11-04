using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class testNuevoInputManager : MonoBehaviour
{
    [Header("Acciones")]
    [SerializeField] private InputActionProperty saltoAction;
    [SerializeField] private InputActionProperty moveAction;
    [SerializeField] private InputActionProperty disparoAction;
    [SerializeField] private InputActionProperty acelerarAction;
    
    [Header("Paneles")]
    [SerializeField] TextMeshProUGUI panelEntrada;
    [SerializeField] TextMeshProUGUI panelAccion;
    [SerializeField] TextMeshProUGUI panelVelocidad;
    [SerializeField] TextMeshProUGUI panelAtaque;

    [Header("Movimiento Player")]
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] float velocidadx = 5.0f;
    [SerializeField] float velocidady = 6.0f;
    [SerializeField] float movimientox = 0f;
    [SerializeField] float movimientoy = 0f;

    //VARIABLES ACELERACIÓN
    private float valorAcelerar = 0f ;
    private float maximoAcelerar = 1.0f;

    void Start()
    {
        panelEntrada.text = "";
        panelAccion.text = "";
        panelVelocidad.text = "";

        rb = player.GetComponent<Rigidbody2D>();
    }

    //CLASES ESTANDAR DE UNITY
    private void OnEnable()
    {
        saltoAction.action.Enable();
        moveAction.action.Enable();
        disparoAction.action.Enable();
        acelerarAction.action.Enable();

        saltoAction.action.performed += pulsacionSalto;
        moveAction.action.performed += pulsacionMove;
        disparoAction.action.performed += pulsacionDisparo;
        acelerarAction.action.performed += pulsacionAcelerar;
    }

    private void OnDisable()
    {
        saltoAction.action.Disable();
        moveAction.action.Disable();
        disparoAction.action.Disable();
        acelerarAction.action.Disable();

        saltoAction.action.performed -= pulsacionSalto;
        moveAction.action.performed -= pulsacionMove;
        disparoAction.action.performed -= pulsacionDisparo;
        acelerarAction.action.performed -= pulsacionAcelerar;
    }

    #region Acciones
    private void pulsacionSalto(InputAction.CallbackContext context)
    {
        if(context.control.device is Gamepad)
        {
            panelEntrada.text = "Gamepad";
        }
        
        else if(context.control.device is Keyboard)
        {
            panelEntrada.text = "Teclado y Ratón";
        }
        
        panelAccion.text = "Salto";

        // Mantiene la velocidad X y cambia solo la Y
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocidady);   
    }

    private void pulsacionMove(InputAction.CallbackContext context)
    {
        if(context.control.device is Gamepad)
        {
            panelEntrada.text = "Gamepad";
        }
        
        else if(context.control.device is Keyboard)
        {
            panelEntrada.text = "Teclado y Ratón";
        }

        panelAccion.text = "Move";

        // Lee el Vector2 de la acción y mantiene la velocidad Y actual
        movimientox = context.ReadValue<Vector2>().x;
        rb.linearVelocity = new Vector2(movimientox * velocidadx, rb.linearVelocity.y);
    }
    
    private void pulsacionDisparo(InputAction.CallbackContext context)
    {
        if(context.control.device is Gamepad)
        {
            panelEntrada.text = "Gamepad";
        }
        
        else if(context.control.device is Mouse)
        {
            panelEntrada.text = "Teclado y Ratón";
        }

        panelAccion.text = "Disparo";
    }

    private void pulsacionAcelerar(InputAction.CallbackContext context)
    {
        if(context.control.device is Gamepad)
        {
            panelEntrada.text = "Gamepad";
        }
        
        else if(context.control.device is Mouse)
        {
            panelEntrada.text = "Teclado y Ratón";
        }

        panelAccion.text = "Acelerar";
    }

    private void pulsacionAtaqueCargado(InputAction.CallbackContext context)
    {
        if(context.control.device is Gamepad)
        {
            panelEntrada.text = "Gamepad";
        }
        
        else if(context.control.device is Mouse)
        {
            panelEntrada.text = "Teclado y Ratón";
        }

        panelAccion.text = "Cargando superataque...";

        panelAccion.text = "¡SUPERATAQUE EJECUTADO!";
    }

    #endregion
    #region Update
    // Update is called once per frame
    void Update()
    {   
       // Acelerar

        
    }
    #endregion
}
