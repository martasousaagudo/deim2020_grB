using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Importante importar esta librería para usar la UI

public class SpaceshipMove : MonoBehaviour
{
    //--SCRIPT PARA MOVER LA NAVE --//
    public AudioSource sonidoExplosion;
    public AudioClip clipExplosion;

    //Variable PÚBLICA que indica la velocidad a la que se desplaza
    //La nave NO se mueve, son los obtstáculos los que se desplazan
    public float speed;

    //Variable que determina cómo de rápido se mueve la nave con el joystick
    //De momento fija, ya veremos si aumenta con la velocidad o con powerUps
    private float moveSpeed = 3f;
    //Variable que determina si estoy en los márgenes
    private bool inMarginMoveX = true;
    private bool inMarginMoveY = true;

    //Capturo el texto del UI que indicará la distancia recorrida
    [SerializeField] Text TextDistance;

    //Llamar al panel Start 
    public GameObject panelRestart;
    public Text record;

    public float distancia = 0;

    //variable para la explosion
    public Transform explosionPrefab;

    //colisiones
    [SerializeField] MeshRenderer myMesh;
    [SerializeField] MeshRenderer myMesh2;
    [SerializeField] MeshRenderer myMesh3;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
        //Llamo a la corrutina que hace aumentar la velocidad
        StartCoroutine("Distancia");
        StartCoroutine("Velocidad");
        panelRestart.SetActive(false);
        gameObject.AddComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //Ejecutamos la función propia que permite mover la nave con el joystick
        MoverNave();

    }

    //Corrutina que hace cambiar el texto de distancia
    IEnumerator Distancia()
    {
        distancia = 0;
        //Bucle infinito que suma 10 en cada ciclo
        //El segundo parámetro está vacío, por eso es infinito
        for (int n = 0; ; n++)
        {
            distancia = (float) distancia + (n * speed);
            //Cambio el texto que aparece en pantalla
            TextDistance.text = "DISTANCIA: " + distancia;

            //Ejecuto cada ciclo esperando 1 segundo
            yield return new WaitForSeconds(1f);
        }

    }
    IEnumerator Velocidad()
    {
        //Bucle infinito que suma 10 en cada ciclo
        //El segundo parámetro está vacío, por eso es infinito
        for (; ; )
        {
            if (speed < 20)
            {
                speed += 0.1f;
            }
            yield return new WaitForSeconds(1f);

        }
       
    }



    void MoverNave()
    {


        /*
        //Ejemplos de Input que podemos usar más adelante
        if(Input.GetKey(KeyCode.Space))
        {
            print("Se está pulsando");
        }
        if(Input.GetButtonDown("Fire1"))
        {
            print("Se está disparando");
        }
        */
        //Variable float que obtiene el valor del eje horizontal y vertical
        float desplX = Input.GetAxis("Horizontal");
        float desplY = Input.GetAxis("Vertical");

        //Movemos la nave mediante el método transform.translate
        //Lo multiplicamos por deltaTime, el eje y la velocidad de movimiento la nave


        float myPosX = transform.position.x;
        float myPosY = transform.position.y;

        //Lanzamos el método que nos comprueba la restricción en X y en Y
        checkRestrX(myPosX, desplX);
        checkRestrY(myPosY, desplY);

        //Si estoy en los márgenes, me muevo
        if (inMarginMoveX)
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * desplX);
        }
        if (inMarginMoveY)
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed * desplY);
        }

    }

    void checkRestrX(float myPosX, float desplX)
    {
        if (myPosX < -4.5 && desplX < 0)
        {
            inMarginMoveX = false;
        }
        else if (myPosX < -4.5 && desplX > 0)
        {
            inMarginMoveX = true;
        }
        else if (myPosX > 4.5 && desplX > 0)
        {
            inMarginMoveX = false;
        }
        else if (myPosX > 4.5 && desplX < 0)
        {
            inMarginMoveX = true;
        }
    }

    void checkRestrY(float myPosY, float desplY)
    {
        //Retricción en Y
        if (myPosY < -0 && desplY < 0)
        {
            inMarginMoveY = false;
        }
        else if (myPosY < -0 && desplY > 0)
        {
            inMarginMoveY = true;
        }
        else if (myPosY > 4 && desplY > 0)
        {
            inMarginMoveY = false;
        }
        else if (myPosY > 4 && desplY < 0)
        {
            inMarginMoveY = true;
        }
    }
    //este metodo es para las colisiones

    private void OnCollisionEnter(Collision collision)
    {
        //cuando se choque el GameObject con obstaculo
        if (collision.gameObject.tag == "obstacle")
        {
            //que desaparezca la malla de la nave
            myMesh.enabled = false;
            myMesh2.enabled = false;
            myMesh3.enabled = false;

            //que se frene la nave
            speed = 0;
            //parar corrutinas
            StopCoroutine("Velocidad");
            StopCoroutine("Distancia");
            //que la nave no se pueda mover en ninguna direccion
            moveSpeed = 0;
            //que aparezca el panel al chocar
            MostrarPanel();
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(explosionPrefab, position, rotation);

            sonidoExplosion.PlayOneShot(clipExplosion);
            

        }
    }

    //Metodo para activar el panel restart

    private void MostrarPanel()
    {
        //Quita el texto de la distancia
        TextDistance.text = "";
        //variable record
        record.text = "RECORD:  " + distancia;

        //ACTIVAR PANEL
        panelRestart.SetActive(true);
    }
}