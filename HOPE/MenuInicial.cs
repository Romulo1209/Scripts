using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour
{
    public GameObject menu;
    public GameObject scenePosition;
    public void Btn_Sair()
    {
        Application.Quit();
    }
    public void Btn_Credito()
    {

    }
    public void Btn_Joga()
    {
        GameController.instance.Transicao(scenePosition.transform);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ativado = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().noMenu = false;
        menu.SetActive(false);
    }
}
