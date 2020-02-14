using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Verificador de teclas no eixo X e Y
    public float movX;
    public float movY;
    //Verificador de diagonal
    private bool naDiagonal;
    //Velocidades
    public float velocidadeReal;
    public float velocidadeDiagonal;
    public float velocidade;
    //Rigidbody do Player
    private Rigidbody2D player;
    private Animator animator;
    //Posição do Player
    private int contadorPlayer;
    //Sprite das Posições do Player
    public List <Sprite> sprites;

    public bool ativado;
    public bool pausado;
    public bool noMenu;
    public GameObject pause;
    void Start()
    {
        //Setando rigidbody do Player
        player = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausado == false && noMenu == false) { pausado = true; } else if (Input.GetKeyDown(KeyCode.Escape) && pausado == true && noMenu == false) { pausado = false; Time.timeScale = 1; }
        if (pausado == true) { Time.timeScale = 0; pause.SetActive(true); if (Input.GetKeyDown(KeyCode.Escape)){ Time.timeScale = 1; } } else { pause.SetActive(false); }
    }

    void FixedUpdate()
    {
        
        Animacoes();
        if (ativado == true && noMenu == false)
        {
            //Movimento sempre ativo
            Movimento();
        }
        else
        {
            player.velocity = new Vector2(0, 0);
        }
    }

    void Animacoes()
    {
 
        if(player.velocity.x == 0 && player.velocity.y == 0){
            animator.SetBool("Andando", false);
        }
        else if (movX > 0){
            animator.SetBool("Andando", true);
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (movX < 0){
            animator.SetBool("Andando", true);
            this.GetComponent<SpriteRenderer>().flipX = true;           
        }
        else if (movY != 0)
        {
            animator.SetBool("Andando", true);
        }
    }

    void Movimento()
    {
        //Verificador de quais teclas estão sendo pressionadas
        movX = Input.GetAxis("Horizontal");
        movY = Input.GetAxis("Vertical");

        //Aplica velocidade no player dependendo da tecla pressionada
        player.velocity = new Vector2(velocidadeReal * movX, velocidadeReal * movY);
        //Verificando se o player esta se movendo na diagonal
        if(movX != 0 && movY != 0){
            naDiagonal = true;
        }
        else { naDiagonal = false; }
        //Aplicando mudança de velocidade se o player estiver na diagonal ou não
        if(naDiagonal == true){
            velocidadeReal = velocidade - velocidadeDiagonal;
        }
        else { velocidadeReal = velocidade; }
        //Zerar velocidade caso certa tecla não esteja sendo pressionada
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            player.velocity = new Vector2(velocidade * movX, 0);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            player.velocity = new Vector2(0, velocidade * movY);
        }
    } 

    public void Resume()
    {
        Time.timeScale = 1;
        pausado = false;
    }
}
