using System.Collections;
using UnityEngine;

public class corrutinaScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Las corrutinas se comienzan con StartCoroutine
        StartCoroutine( GeneraSecuencia() );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GeneraSecuencia()
    {
        for(int i = 0; i<3; i++)
        {
            Debug.Log($"Entro en corrutina, ejecución numero {i+1}");
            
            //Paro 3 segundos
            yield return new WaitForSeconds(3f);

            Debug.Log($"Salgo de la corrutina, ejecución numero {i+1}");
        }
    }
}
