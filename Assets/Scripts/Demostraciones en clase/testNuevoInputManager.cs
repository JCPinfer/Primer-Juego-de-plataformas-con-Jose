using UnityEngine;
using UnityEngine.InputSystem;

public class testNuevoInputManager : MonoBehaviour
{
    [SerializeField] private InputActionProperty pulsacionesAction;
    [SerializeField] private InputActionProperty moveAction;

    //CLASES ESTANDAR DE UNITY
    private void OnEnable()
    {
        pulsacionesAction.action.Enable();
        moveAction.action.Enable();

        pulsacionesAction.action.performed += pulsacionTecla;
    }

    private void OnDisable()
    {
        pulsacionesAction.action.Disable();
        moveAction.action.Disable();

        pulsacionesAction.action.performed -= pulsacionTecla;
    }

    private void pulsacionTecla(InputAction.CallbackContext context)
    {
        Debug.Log("PULSACIÓN");
    }

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

        if ( moveAction.action.ReadValue<Vector2>() != Vector2.zero)
            Debug.Log( moveAction.action.ReadValue<Vector2>() );
    }
}
