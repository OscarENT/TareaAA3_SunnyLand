using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    //Inicializamos las siguientes variables:
    bool isJumping = false;
    bool isPlaying = false;

    Rigidbody2D rb;
    AudioSource runningSource;
    AudioSource jumpSource;
    AudioSource cherrySource;
    AudioSource ouchSource;

    //Inicializamos las siguientes variables de manera pública para que puedan ser accedidas externamente a este script:
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip crouchSound;
	
    //método que se ejecuta sólo una vez al principio
    void Start()
    {
        //obtenemos el rigidbody (es decir, el personaje)
	    rb = GetComponent<Rigidbody2D>();
        //obtenemos los componentes de audio correspondientes al índice 0, 1 y 2.
        runningSource = GetComponents<AudioSource>()[0];
	    jumpSource = GetComponents<AudioSource>()[1];
	    cherrySource = GetComponents<AudioSource>()[2];
        ouchSource = GetComponents<AudioSource>()[3];
    }
    
    //método que se ejecuta continuamente
    void FixedUpdate()
    {	
        //leemos la velocidad en la variable v
	    float v = rb.velocity.magnitude;
        /* Si v es mayora a 1, isPlaying es false y isJumping es false,
        * (es decir, si el personaje no está saltando, el sonido no estaba ya sonando y velocidad es mayor a 1)*/
	    if ( v > 1 && !isPlaying && !isJumping) {
            //Reproducimos el sonido runningSource y establecemos el boolean isPlaying a true para indicar que el sonido está sonando
	        runningSource.Play();
	        isPlaying = true;
        // Si no se cumple la anterior condición, v es menor a 1 y el sonido ya estaba sonando (isPlaying == true)
	    } else if ( v < 1 && isPlaying ) {	    
            //Detenemos la reproducción del sonido y establecemos el boolean isPlaying a false para indicar que el sonido no está sonando
	        runningSource.Stop();
	        isPlaying = false;
	    }
	    // Si el jugador está saltando (isJumping == true)
	    if (isJumping) {
            /* Detenemos la reproducción del sonido y establecemos el boolean isPlaying a false para indicar que el sonido no está sonando
            * Dado que en el else anterior hacemos las mismas acciones que en este if, podríamos simplemente haber puesto:
            "else if ( (v < 1 && isPlaying) || isJumping )", de manera que no haría falta tener otro if entero. */
            runningSource.Stop();
	        isPlaying = false;
	    }
    }
    
    //método void que se ejecutará cuando el personaje aterrice
    public void OnLanding() {
        //Establecemos el boolean isJumping a false para indicar que el personaje ya no está saltando
        isJumping = false;
        /* Cambiamos el sonido de salto a crouchSound. Usamos el método clip en lugar de asignar el valor directamente para que
         * el nuevo sonido no se establezca antes de que el anterior sonido acabe de sonar (si estaba sonando).
         * Lo mismo ocurre las siguientes veces que aparece el AudioSource.clip en los métodos OnCroughing, OnJump y OnCherryCollect*/
        jumpSource.clip = landSound;

        //Obtenemos un número aleatorio entre el 0 y el 2
	    int randomNumber = Random.Range(0, 2);
        // Si el número aleatorio es 1
	    if (randomNumber == 1) {
            // Hacemos que el sonido tenga un pitch de 0.6 y 1.4 (siendo 1.0 el valor normal de pitch)
            jumpSource.pitch = 1.0f + Random.Range(-0.4f, 0.4f);
    	} else {
            // Dejamos el pitch del sonido en el valor normal
	        jumpSource.pitch = 1.0f;
	    }
        //Reproducimos el sonido de salto
    	jumpSource.Play();
        //Imprimimos en la consola el siguiente texto:
        print("the fox has landed");	
        }
    
    public void OnCrouching()
    {
        // Cambiamos el sonido de salto a crouchSound.
        jumpSource.clip = crouchSound;
        //Reproducimos el sonido
	    jumpSource.Play();
        //Imprimimos en la consola el siguiente texto:
        print("the fox is crouching");
    }
 
    public void OnJump()
    {
        //Establecemos el boolean isJumping a true para indicar que el personaje está saltando
        isJumping = true;
        // Cambiamos el sonido de salto a jumpSound.
	    jumpSource.clip = jumpSound;
        // Hacemos que el sonido tenga un pitch de 0.7 y 1.3 (siendo 1.0 el valor normal de pitch)
	    jumpSource.pitch = 1.0f + Random.Range(-0.3f, 0.3f);
        //Reproducimos el sonido
        jumpSource.Play();
        //Imprimimos en la consola el siguiente texto:
        print("the fox has jumped");
    }
    
    public void OnCherryCollect()
    {
        //Reproducimos el sonido
        cherrySource.Play();
        //Imprimimos en la consola el siguiente texto:
        print("the fox has collected a cherry");
    }
    //Event fired when Player hits the box
    public void BoxCollisionEvent()
    {
        // Si el sonido no se está reproduciendo
        if (!ouchSource.isPlaying)
        {
            //Reproducimos el sonido
            ouchSource.Play();
        }
    }
}
