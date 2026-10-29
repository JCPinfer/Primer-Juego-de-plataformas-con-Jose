using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class testNuevoInputManager : MonoBehaviour
{
    [Header("Acciones")]
    [SerializeField] private InputActionProperty saltoAction;
    [SerializeField] private InputActionProperty moveAction;
    [SerializeField] private InputActionProperty disparoAction;
    
    [Header("Paneles")]
    [SerializeField] TextMeshProUGUI panelEntrada;
    [SerializeField] TextMeshProUGUI panelAccion;

    [Header("Movimiento Player")]
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] float velocidadx = 5.0f;
    [SerializeField] float velocidady = 6.0f;
    [SerializeField] float movimientox = 0f;
    [SerializeField] float movimientoy = 0f;

    void Start()
    {
        panelEntrada.text = "";
        panelAccion.text = "";

        rb = player.GetComponent<Rigidbody2D>();
    }

    //CLASES ESTANDAR DE UNITY
    private void OnEnable()
    {
        saltoAction.action.Enable();
        moveAction.action.Enable();
        disparoAction.action.Enable();

        saltoAction.action.performed += pulsacionSalto;
        moveAction.action.performed += pulsacionMove;
        disparoAction.action.performed += pulsacionDisparo;
    }

    private void OnDisable()
    {
        saltoAction.action.Disable();
        moveAction.action.Disable();
        disparoAction.action.Disable();

        saltoAction.action.performed -= pulsacionSalto;
        moveAction.action.performed -= pulsacionMove;
        disparoAction.action.performed -= pulsacionDisparo;
    }

    #region Acciones
    private void pulsacionSalto(InputAction.CallbackContext context)
    {
        if(context.control.device is Gamepad)
        {
            Debug.Log("Gamepad - Salto");
            panelEntrada.text = "Gamepad";
        }
        
        else if(context.control.device is Keyboard)
        {
            Debug.Log("Teclado - Salto");
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
            Debug.Log("Gamepad - Movimiento");
            panelEntrada.text = "Gamepad";
        }
        
        else if(context.control.device is Keyboard)
        {
            Debug.Log("Teclado - Movimiento");
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
            Debug.Log("Gamepad - Disparo");
            panelEntrada.text = "Gamepad";
        }
        
        else if(context.control.device is Mouse)
        {
            Debug.Log("Teclado - Disparo");
            panelEntrada.text = "Teclado y Ratón";
        }

        panelAccion.text = "Disparo";
    }

    #endregion
    #region Update
    // Update is called once per frame
    void Update()
    {   
        //Se podría poner solamente el nombre del boton virtual en el InputManager
        //El cual se puede asignar manualmente en la config del mismo
        //SISTEMA VIEJO:
        
        /*
        if (Input.GetKeyDown(KeyCode.W)) 
            Debug.Log("Pulsado tecla: W" );
        */

        /*
        if ( moveAction.action.ReadValue<Vector2>() != Vector2.zero)
            Debug.Log( moveAction.action.ReadValue<Vector2>() );
        */
    }
    #endregion
}
