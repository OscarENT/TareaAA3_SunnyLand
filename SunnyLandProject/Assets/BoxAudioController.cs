using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxAudioController : MonoBehaviour
{

    //Creamos un evento para luego recibirlo en PlayerAudioController.cs
    [Header("Events")]
    [Space]
    public UnityEvent OnCollisionEvent;

    bool isPlaying = false;

    Rigidbody2D rb;
    AudioSource movementSource;
    AudioSource impactSource;
        
    void Start()
    {
	rb = GetComponent<Rigidbody2D>();
    impactSource = GetComponents<AudioSource>()[0];
    movementSource = GetComponents<AudioSource>()[1];
    }
    
    void FixedUpdate()
    {
        float v = rb.velocity.magnitude;
	if ( v > 1 && !isPlaying) {
        movementSource.pitch = 1.0f + Random.Range(-0.4f, 0.4f);
        movementSource.Play();
	    isPlaying = true;
	} else if ( v < 1 && isPlaying ) {	    
	    movementSource.Stop();
	    isPlaying = false;
	}
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Hacemos que el sonido tenga un pitch de 0.6 y 1.4 (siendo 1.0 el valor normal de pitch)
        impactSource.pitch = 1.0f + Random.Range(-0.4f, 0.4f);
        impactSource.Play();
        //Comprobamos si la caja ha chocado con el jugador, si es así invocamos el evento.
        if (coll.gameObject.tag == "Player")
        {
            OnCollisionEvent.Invoke();
        }
    }
}
