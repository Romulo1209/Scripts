using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivadorSala : AlertaInteracao
{
    public Transform scenePosition;
    public bool semTransicao;
    private AudioSource portaAbrindo;
    private bool podeExectar;
    private Player player;
    public float timer;
    private bool parado;
    void Start()
    {
        portaAbrindo = this.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (podeExectar == true)
        {
            if (Input.GetKeyDown(KeyCode.E) || semTransicao == true)
            {
                GameController.instance.Transicao(scenePosition);
                portaAbrindo.Play();
                podeExectar = false;
                player.ativado = false;
                timer = 1;
                parado = true;
            }
        }
        if(parado == true)
        {
            timer -= Time.deltaTime * 3;
            if(timer <= 0) { player.ativado = true; parado = false; }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (semTransicao == false)
        {
            Interagir();
        }
        podeExectar = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Sair();
        podeExectar = false;
    }
}
