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

    //Inicializamos las siguientes variables de manera p�blica para que puedan ser accedidas externamente a este script:
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip crouchSound;
	
    //m�todo que se ejecuta s�lo una vez al principio
    void Start()
    {
        //obtenemos el rigidbody (es decir, el personaje)
	    rb = GetComponent<Rigidbody2D>();
        //obtenemos los componentes de audio correspondientes al �ndice 0, 1 y 2.
        runningSource = GetComponents<AudioSource>()[0];
	    jumpSource = GetComponents<AudioSource>()[1];
	    cherrySource = GetComponents<AudioSource>()[2];
        ouchSource = GetComponents<AudioSource>()[3];
    }
    
    //m�todo que se ejecuta continuamente
    void FixedUpdate()
    {	
        //leemos la velocidad en la variable v
	    float v = rb.velocity.magnitude;
        /* Si v es mayora a 1, isPlaying es false y isJumping es false,
        * (es decir, si el personaje no est� saltando, el sonido no estaba ya sonando y velocidad es mayor a 1)*/
	    if ( v > 1 && !isPlaying && !isJumping) {
            //Reproducimos el sonido runningSource y establecemos el boolean isPlaying a true para indicar que el sonido est� sonando
	        runningSource.Play();
	        isPlaying = true;
        // Si no se cumple la anterior condici�n, v es menor a 1 y el sonido ya estaba sonando (isPlaying == true)
	    } else if ( v < 1 && isPlaying ) {	    
            //Detenemos la reproducci�n del sonido y establecemos el boolean isPlaying a false para indicar que el sonido no est� sonando
	        runningSource.Stop();
	        isPlaying = false;
	    }
	    // Si el jugador est� saltando (isJumping == true)
	    if (isJumping) {
            /* Detenemos la reproducci�n del sonido y establecemos el boolean isPlaying a false para indicar que el sonido no est� sonando
            * Dado que en el else anterior hacemos las mismas acciones que en este if, podr�amos simplemente haber puesto:
            "else if ( (v < 1 && isPlaying) || isJumping )", de manera que no har�a falta tener otro if entero. */
            runningSource.Stop();
	        isPlaying = false;
	    }
    }
    
    //m�todo void que se ejecutar� cuando el personaje aterrice
    public void OnLanding() {
        //Establecemos el boolean isJumping a false para indicar que el personaje ya no est� saltando
        isJumping = false;
        /* Cambiamos el sonido de salto a crouchSound. Usamos el m�todo clip en lugar de asignar el valor directamente para que
         * el nuevo sonido no se establezca antes de que el anterior sonido acabe de sonar (si estaba sonando).
         * Lo mismo ocurre las siguientes veces que aparece el AudioSource.clip en los m�todos OnCroughing, OnJump y OnCherryCollect*/
        jumpSource.clip = landSound;

        //Obtenemos un n�mero aleatorio entre el 0 y el 2
	    int randomNumber = Random.Range(0, 2);
        // Si el n�mero aleatorio es 1
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
        //Establecemos el boolean isJumping a true para indicar que el personaje est� saltando
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
        // Si el sonido no se est� reproduciendo
        if (!ouchSource.isPlaying)
        {
            //Reproducimos el sonido
            ouchSource.Play();
        }
    }
}
