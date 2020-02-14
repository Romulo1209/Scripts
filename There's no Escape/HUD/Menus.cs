using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public GameObject inicial;
    public GameObject opcoes;
    public GameObject creditos;

    public AudioSource audioMenu;
    public void Iniciar()
    {
        SceneManager.LoadScene(3);
    }

    public void Opções()
    {
        opcoes.SetActive(true);
        inicial.SetActive(false);
    }

    public void Creditos()
    {
        creditos.SetActive(true);
        inicial.SetActive(false);
    }

    public void Voltar()
    {
        creditos.SetActive(false);
        opcoes.SetActive(false);
        inicial.SetActive(true);
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Click()
    {
        audioMenu.Play();
    }
}
