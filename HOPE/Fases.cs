using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fases : MonoBehaviour
{
    public int fase;
    public bool proximo;
    public bool interagir;
    public bool interagindo;
    public int tocar;
    public PosPortas portas;
    public Transform scenePosition;
    public Transform final;
    private AudioSource som;
    public float timer;
    public Image fade;
    public AudioSource musica;
    public GameObject reset;
    public Inimigo inimigo;

    public GameObject fase2;
    public GameObject fase3;
    public GameObject fase4;
    public GameObject fase5;

    void Start()
    {
        fase = 0;
        som = this.GetComponent<AudioSource>();
        inimigo = GameObject.FindGameObjectWithTag("Inimigo").GetComponent<Inimigo>();
    }
    void Update()
    {
        if (interagir == true && Input.GetKeyDown(KeyCode.E)) { interagindo = true; }
        if (interagindo == true) { Animacao(); }

    }

    void Animacao()
    {
        timer += Time.deltaTime;
        fade.color = new Color(0, 0, 0, timer);
        musica.Stop();
        inimigo.ativado = false;
        inimigo.GetComponent<SpriteRenderer>().enabled = false;
        inimigo.GetComponent<BoxCollider2D>().enabled = false;
        if (timer >= 1 && tocar == 0)
        {
            tocar = 1;
        }
        if(tocar == 1)
        {
            som.Play();
            tocar = 2;
        }
        if (!som.isPlaying && tocar == 2)
        {
            ProximaFase();
        }

    }

    void ProximaFase()
    {
        fase += 1;
        portas.gerar = true;
        proximo = false;
        interagindo = false;
        tocar = 0;
        fade.color = new Color(0, 0, 0, 0);
        if (fase == 1) { fase2.SetActive(true); }
        else if (fase == 2) { fase3.SetActive(true); }
        else if (fase == 3) { fase4.SetActive(true); }
        else if (fase == 4) { fase5.SetActive(true); }
        if (fase < 4) { GameController.instance.Transicao(scenePosition); } 
        else if(fase >= 4) { GameController.instance.Transicao(final); }
        musica.Play();
        reset.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interagir = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interagir = false;
        }
    }
}
