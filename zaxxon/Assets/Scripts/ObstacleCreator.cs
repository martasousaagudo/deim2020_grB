﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{
    //---SCRIPT ASOCIADO AL EMPTY OBJECT QUE CREARÁ LOS OBSTÁCULOS--//

    //Variable que contendré el prefab con el obstáculo
    [SerializeField] GameObject Columna;

    //Variable que tiene la posición del objeto de referencia
    [SerializeField] Transform InitPos;

    //Variables para generar columnas de forma random
    private float randomNumber;
    Vector3 RandomPos;

    //Distancia a la que se crean las columnas iniciales
    [SerializeField] float distanciaInicial = 5;

    //Acceder a los componentes de la nave
    public GameObject Spaceship;
    private SpaceshipMove spaceshipMove;
    
    // Start is called before the first frame update
    void Start()
    {

        //Accedo al script de la nave porque es privada
        spaceshipMove = Spaceship.GetComponent<SpaceshipMove>();
        //Creo las columnas iniciales
        for(int n = 1; n <= 25; n++)
        {
            
            CrearColumna(-n * distanciaInicial);
        }
        
        //Lanzo la corrutina
        StartCoroutine("InstanciadorColumnas");

    }

    public void Update()
    {
        if (spaceshipMove.speed == 0)
        {
            StopCoroutine("InstanciadorColumnas");
        }
    }

    //Función que crea una columna en una posición Random
    void CrearColumna(float posZ = 0f)
    {
        randomNumber = Random.Range(0f, 7f);
        RandomPos = new Vector3(randomNumber, 0, posZ);
        //print(RandomPos);
        Vector3 FinalPos = InitPos.position + RandomPos;
        Instantiate(Columna, FinalPos, Quaternion.identity);
    }

    //Corrutina que se ejecuta cada segundo
    //NOTA: ahora el intervalo de la corrutina depende de la variable "speed" de la nave
    //Aplicamos la fórmula "espacioEntreColumnas / velocidad"
    IEnumerator InstanciadorColumnas()
    {
        //Bucle infinito (poner esto es lo mismo que while(true){}
        for (; ; )
        {
            CrearColumna();
            float interval = 4 / spaceshipMove.speed;
            yield return new WaitForSeconds(interval);
        }

    }


}