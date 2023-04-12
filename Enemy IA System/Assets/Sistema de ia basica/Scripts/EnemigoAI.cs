using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemigoAI : MonoBehaviour
{
    public Transform jugador;
    private NavMeshAgent agente;
    private float distanciaMinima = 20.0f; // establece una distancia para detectar al jugador

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        ObtenerNuevoDestino(); 
    }

    private void Update()
    {
        // verifica a que distancia esta el jugador
        float distanciaJugador = Vector3.Distance(transform.position, jugador.position);

      
        if (distanciaJugador < distanciaMinima)
        {
            // el Raycast no sirve para detectar al jugador
            RaycastHit hit;
            if (Physics.Raycast(transform.position, jugador.position - transform.position, out hit, distanciaMinima))
            {
                if (hit.collider.tag == "Player")
                {
                    // Si se detecta al jugador lo empezara a perseguir
                    agente.destination = jugador.position;
                }
            }
        }
        else
        {
            // Si no est� persiguiendo al jugador se movera solo de nuevo
            if (!agente.hasPath || agente.remainingDistance < 0.5f)
            {
                // Si ya no tiene un destino al cual llegar obtiene uno nuevo
                ObtenerNuevoDestino();
            }
        }
    }

    private void ObtenerNuevoDestino()
    {
        
        Vector3 destino = Random.insideUnitSphere * 20.0f + transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(destino, out hit, 20.0f, NavMesh.AllAreas);

        // Establecer la nueva posici�n como objetivo del NavMeshAgent
        agente.destination = hit.position;
    }
}


