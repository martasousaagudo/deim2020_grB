﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RESTART : MonoBehaviour
{
 //metodo para cargar una escena
 public void RestartScene()
    {
        SceneManager.LoadScene("GAME");
        
    }

    public void goMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
