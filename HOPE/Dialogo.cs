using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogo : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Player player;
    public bool interagir;
    public bool repetivel;
    public bool apertar;
    public bool ativador;
    public GameObject caixaDialogo;
    public Text texto;
    public Image imagem;
    public List<string> textos;
    public List<Sprite> foto;

    public GameObject ativar;
    private AudioSource som;
    public int linha;

    public bool destruir;
    public bool pararTempo;
    public GameObject destruido;
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        som = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interagir == true && apertar == true) { Dialogar(); }
        if (apertar == false && interagir == true) { Dialogar(); }
    }

    void Dialogar()
    {
        player.ativado = false;
        caixaDialogo.SetActive(true);
        texto.text = textos[linha];
        imagem.sprite = foto[linha];
        if (pararTempo == true) { Time.timeScale = 0; }
        if (Input.GetKeyDown(KeyCode.E)) { som.Play(); }

        if (texto.text == "")
        {
            player.ativado = true;
            caixaDialogo.SetActive(false);
            linha = 0;
            if (ativador == true) { ativar.SetActive(true); }
            if (repetivel == false) { Destroy(this); sprite.enabled = false; }
            if (destruir == true) { Destroy(destruido); }
            if (pararTempo == true) { Time.timeScale = 1; }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            linha += 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sprite.enabled = true;
            interagir = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sprite.enabled = false;
            interagir = false;
        }
    }
}
