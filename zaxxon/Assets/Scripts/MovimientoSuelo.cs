using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoSuelo : MonoBehaviour
{
    
    Renderer rend;
    //variable imagen
    private Image UIimage;
    public GameObject nave;
    public SpaceshipMove spaceshipMove;

    // Start is called before the first frame update
    void Start()
    {
        //coger componente renderer del objeto (plano)
        rend = GetComponent<Renderer>();
        //buscar imagen dentro de los G.O.
        UIimage = GameObject.Find("Plane").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //variable para cambbiar el offset del plano
        float offset = Time.time * spaceshipMove.speed / 4;
        //utilizando la variable mover la textura
        rend.material.mainTextureOffset = new Vector2(0, -offset);
    }
}
