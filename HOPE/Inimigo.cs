using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inimigo : MonoBehaviour
{
    //Timer
    public float timer;
    private float timerPos;
    //Velocidade
    public float velocidadeAtual;
    public float velocidade1;
    public float velocidade2;
    public float velocidade3;

    public float tempoVelocidade1;
    public float tempoVelocidade2;
    //Transform Player
    private Transform player;
    //Bool para ativar script
    public bool ativado;
    public bool reset;
    
    public Rigidbody2D inimigo;
    public GameObject path;
    
    public List<Vector3> posPlayer;
    public List<Sprite> sprites;

    private int contadorPlayer;
    private Animator animator;
    private bool pathCol;

    public GameObject morte;
    public GameObject scenePosition;
    public AudioSource musica;

    void Start()
    {
        //Iniciar o script com o tempo predefinido em segundos
        timer = 1;
        //Pegar a posição do player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inimigo = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(reset == true) { Reset(); }
        if(ativado == true)
        {
            timer -= Time.deltaTime;
            //Ativar o Script apenas quando o ativado == true

            Posicao();
            if (timer <= 0)
            {
                IA();
                Animacoes();
            }
        }
    }

    void Reset()
    {
        ativado = false;
        timer = 2;
        posPlayer.Clear();
        ativado = true;
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        reset = false;
    }

    void Posicao()
    {
        timerPos += Time.deltaTime * 3;
        if(timerPos >= 1)
        {
            timerPos = 0;
            posPlayer.Add(player.position);
        }
    }

    void IA()
    {
        //Ativar sprite renderer do inimigo
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        //Fazer o inimigo andar em direção ao player
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(posPlayer[0].x, posPlayer[0].y, -6), velocidadeAtual);
        if(transform.position == new Vector3(posPlayer[0].x, posPlayer[0].y, -6))
        {
            posPlayer.RemoveAt(0);
        }
        if(timer < -(tempoVelocidade2)) { velocidadeAtual = velocidade3; }
        else if(timer < -(tempoVelocidade1)) { velocidadeAtual = velocidade2; }
        else if (timer < 0) { velocidadeAtual = velocidade1; }
    }

    void Animacoes()
    {
        if (transform.position == new Vector3(posPlayer[0].x, posPlayer[0].y))
        {
            animator.SetInteger("Parado", 1);
            animator.SetInteger("Horizontal", 2);
            animator.SetInteger("Vertical", 2);
            if (contadorPlayer == 1) { this.GetComponent<SpriteRenderer>().sprite = sprites[0]; }
            if (contadorPlayer == 2) { this.GetComponent<SpriteRenderer>().sprite = sprites[1]; }
            if (contadorPlayer == 3) { this.GetComponent<SpriteRenderer>().sprite = sprites[2]; }
        }
        else if (transform.position.x - posPlayer[0].x < 0)
        {
            animator.SetInteger("Horizontal", 1);
            animator.SetInteger("Vertical", 2);
            animator.SetInteger("Parado", 0);
            this.GetComponent<SpriteRenderer>().flipX = false;
            contadorPlayer = 1;
        }
        else if (transform.position.x - posPlayer[0].x > 0)
        {
            animator.SetInteger("Horizontal", 1);
            animator.SetInteger("Vertical", 2);
            animator.SetInteger("Parado", 0);
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.y - posPlayer[0].y < 0)
        {
            animator.SetInteger("Horizontal", 2);
            animator.SetInteger("Vertical", 1);
            animator.SetInteger("Parado", 0);
            contadorPlayer = 2;
        }
        else if (transform.position.x - posPlayer[0].y > 0)
        {
            animator.SetInteger("Horizontal", 2);
            animator.SetInteger("Vertical", -1);
            animator.SetInteger("Parado", 0);
            contadorPlayer = 3;
        }
        else if (transform.position.y - posPlayer[0].y < 0 && transform.position.x - posPlayer[0].x != 0)
        {
            animator.SetInteger("Horizontal", 2);
            animator.SetInteger("Vertical", 1);
            animator.SetInteger("Parado", 0);
        }
        else if (transform.position.y - posPlayer[0].y > 0 && transform.position.x - posPlayer[0].x != 0)
        {
            animator.SetInteger("Horizontal", 2);
            animator.SetInteger("Vertical", -1);
            animator.SetInteger("Parado", 0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            ativado = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ativado = false;
            morte.SetActive(true);
            musica.Stop();
        }
    }

    public void Reiniciar()
    {
        GameController.instance.Transicao(scenePosition.transform);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ativado = true;
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
        musica.Play();
        morte.SetActive(false);
    }

    public void Fechar()
    {
        Application.Quit();
    }
}
