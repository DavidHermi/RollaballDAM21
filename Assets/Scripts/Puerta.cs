namespace DefaultNamespace;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour {
    public Animator laPuerta ;

    private void OnTriggerEnter(Collider other)
    {

        lapuerta.play("abrir");
    }
    private void OnTriggerexit(Collider other)
    {

        lapuerta.play("salir");
    }
   
}

