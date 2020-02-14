using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void Sair()
    {
        Application.Quit();
    }

    public void Recomeçar()
    {
        SceneManager.LoadScene(1);
    }
}
