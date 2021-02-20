using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject menu, highScores;

    private void Start()
    {
        highScores.SetActive(false);

    }

    public void goHighScore()
    {
        menu.SetActive(false);
       highScores.SetActive(true);
    }
    public void goMenu()
    {
        highScores.SetActive(false);
        menu.SetActive(true);

    }


    public void iniciarJuego()
    {

        SceneManager.LoadScene("GAME");


    }



}
