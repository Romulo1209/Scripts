using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //Valores
    public float velocity;
    public float jumpForce;
    public float maxLife = 3;
    public float staminaMax = 30;
    public float life = 3;
    float jumps = 1;
    float staminaA = 30;
    float timeInv;
    float timeP;
    //Spawnpoint
    public Transform spawn;
    //Inputs
    float movX;
    float movY;
    float run;
    float jump;
    float attack;
    //Sprites
    public SpriteRenderer head;
    public SpriteRenderer torso;
    public SpriteRenderer foot1;
    public SpriteRenderer foot2;
    public SpriteRenderer weapon1;
    public SpriteRenderer weapon2;
    public SpriteRenderer espada1;
    public SpriteRenderer espada2;
    public Sprite swordSprite;
    //Condições
    public bool canMove = true;
    public bool inDialogue;
    public bool teleporting;
    bool running;
    bool attackMode;
    bool attacking;
    bool takingDamage;
    bool onkick;
    bool kick;
    bool dead;
    bool end;   
    bool onGround;
    bool onMove;
    bool materialHide;
    bool turn;
    bool canUse;
    int swordState;
    //Vida
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image heart4;
    public Image heart5;
    public Image heart6;
    //Componentes
    public GameObject Exc;
    public Slider staminaHud;
    public GameObject dialogueObj;
    public Center center;
    public Animator fade;
    public Rigidbody2D player;
    public Animator anim;
    public Animator materialAnim;
    void Start()
    {
        //Inicio
        player = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Animações sempre rodando
        Animation();
        //Se não estiver morto o o script roda
        if (end == false)
        {
            //Cano tome dano a função inicia
            TakingDamage();
            //Se pode interagir exclamação aparece
            if (canUse == true) { Exc.SetActive(true); } else { Exc.SetActive(false); }
            //Se estiver em dialogo você não pode se mover
            if (inDialogue == true) { dialogueObj.SetActive(true); }
            else
            {
                if (canMove == true) { Movement(); }
                dialogueObj.SetActive(false);
            }
            //Iniciar ataque
            if (attacking == false) { attack = Input.GetAxis("Attack"); } else { attack = 0; }
            //Caso esteja teleportando não pode se mexer
            if(teleporting == true) { player.velocity = new Vector3(0, player.velocity.y); }
        }
        //Se estiver morto para de se mexer
        else { player.velocity = new Vector3(0, player.velocity.y); }
    }
    //Animações
    void Animation()
    {
        //Animação quando está levando chute
        if(takingDamage == true || kick == true) { anim.SetBool("TakingDamage", true); }
        else { anim.SetBool("TakingDamage", false); }
        //Animação de morte
        if(dead == true) { anim.SetBool("Death", true); }
        else { anim.SetBool("Death", false); }
        //Seta qual espada aparacer dependendo do modo que você esta
        if (attackMode == true) { espada2.sprite = swordSprite; espada1.sprite = null; }
        if (attackMode == false) { espada2.sprite = null; espada1.sprite = swordSprite; }
        /*if (attack > 0 && movX == 0 && attackMode == true && attacking == false )
        {
            animação de ataque fraco DESATIVADO
            anim.SetBool("Attack", true);
        }
        */
        //Ataque forte
        else if(attack < 0 && movX == 0 && attackMode == true && attacking == false && staminaA - 10 > 0) { anim.SetBool("StrongAttack", true); staminaA -= 10; }
        //Limita o ataque em certas situações
        else if(movX != 0 && attackMode == true && attacking == true) {
            anim.SetBool("Attack", false);
            anim.SetBool("StrongAttack", false);
            canMove = true;
            attacking = false;
        }
        else if(attacking == false) {
            anim.SetBool("Attack", false);
            anim.SetBool("StrongAttack", false);
        }
        //No ar
        if (onGround == false) { anim.SetBool("Jump", false); anim.SetBool("Air", true); }
        //Finaliza o pulo
        if (onGround == true) { anim.SetBool("Air", false); }
        //Animação de andar + animação de puxar e guardar espada em diferentes situações
        if(player.velocity.x != 0 && onGround == true) {
            anim.SetBool("Walk", true);
            anim.SetBool("LookDown", false);
            anim.SetBool("LookUp", false);
            if (swordState == 1) {
                if (attackMode == false) {
                    anim.SetBool("Pick", true);
                    anim.SetBool("Clear", true);
                }
                else {
                    anim.SetBool("Pick", false);
                    anim.SetBool("Clear", false);
                }
            }
            if (swordState == 2) {
                if (attackMode == false) {
                    anim.SetBool("Pick", false);
                    anim.SetBool("Clear", true);

                }
                else {
                    anim.SetBool("Pick", true);
                    anim.SetBool("Clear", false);
                }
            }
        }
        //Animação de ficar parado + animação de puxar e guardar espada em diferentes situações
        else if (player.velocity.x == 0 && onGround == true) {
            anim.SetBool("Walk", false);
            if (swordState == 1) {
                if(attackMode == false) {
                    anim.SetBool("Pick", true);
                    anim.SetBool("Clear", true);
                }
                else {
                    anim.SetBool("Pick", false);
                    anim.SetBool("Clear", false);
                } 
            }
            if(swordState == 2) {
                if(attackMode == false) {
                    anim.SetBool("Pick", false);
                    anim.SetBool("Clear", true);

                }
                else {
                    anim.SetBool("Pick", true);
                    anim.SetBool("Clear", false);
                }                
            }
        }
        //Animação de correr + animação de puxar e guardar espada em diferentes situações
        if (running == true && player.velocity.x != 0) {
            anim.SetBool("Walk", false);
            anim.SetBool("Running", true);
            if (swordState == 1) {
                if (attackMode == false) {
                    anim.SetBool("Pick", true);
                    anim.SetBool("Clear", true);
                }
                else {
                    anim.SetBool("Pick", false);
                    anim.SetBool("Clear", false);
                }
            }
            if (swordState == 2) {
                if (attackMode == false) {
                    anim.SetBool("Pick", false);
                    anim.SetBool("Clear", true);

                }
                else {
                    anim.SetBool("Pick", true);
                    anim.SetBool("Clear", false);
                }
            }
        }
        //Finaliza a condição de correr e finaliza a animação
        else { anim.SetBool("Running", false); }
        //Animação de mostar/esconder os materiais
        if (materialHide == true) { materialAnim.SetBool("Fall", false); }
        else { materialAnim.SetBool("Fall", true); }
        //Animações de olhar para cima ou baixo
        if (movY > 0 && movX == 0) { anim.SetBool("LookUp", true); }
        if (movY < 0 && movX == 0) { anim.SetBool("LookDown", true); }
        if (movY == 0 && movX == 0) { anim.SetBool("LookDown", false); anim.SetBool("LookUp", false); }
    }
    //Movimentação do player
    void Movement()
    {
        //HUD da estamina
        staminaHud.maxValue = staminaMax;
        staminaHud.value = staminaA;
        //Caso a stamina atual seja maior que a maxima ela vira a maxima
        if(staminaA > staminaMax) { staminaA = staminaMax; }
        //Regenera stamina parado
        if(player.velocity.x == 0 && onMove == false) { staminaA += Time.deltaTime * 15; }
        //Regenra stamina quando está caminhando
        if(player.velocity.x != 0 && onMove == true && running == false) { staminaA += Time.deltaTime * 5; }
        //Ativa e desativa a barra de materiais
        if (Input.GetKeyDown(KeyCode.Tab)) { if (materialHide == true) { materialHide = false; } else { materialHide = true; } }
        //Guarda ou puxa a espada
        if (Input.GetKeyDown(KeyCode.F) && movY == 0) { if(attackMode == false) { swordState = 1; } else { swordState = 2; } }
        //Inputs vertical e horizontal
        movX = Input.GetAxis("Horizontal");
        movY = Input.GetAxis("Vertical");
        //Inputs correr e pular
        run = Input.GetAxis("Run");
        jump = Input.GetAxis("Jump");
        //Raycast parar checar o chão e definir se está no chão ou não
        Debug.DrawLine(new Vector3(transform.position.x - 0.1f, transform.position.y -1.2f, transform.position.z), new Vector3(transform.position.x + 0.1f, transform.position.y - 1.2f, transform.position.z), Color.red);
        RaycastHit2D groundCheck = Physics2D.Raycast(new Vector3(transform.position.x - 0.1f, transform.position.y - 1.2f, transform.position.z), Vector2.right, 0.2f);
        if (groundCheck.collider != null) { if (groundCheck.collider.tag == "Ground") { onGround = true; } else { onGround = false; } }
        if (groundCheck.collider == null) { onGround = false; }
        //Verifica se está no chão e se pode pular e depois executa o pulo
        if(onGround == true && jump > 0 && jumps > 0 && staminaA - 10 > 0) { player.velocity = new Vector3(player.velocity.x, jumpForce); anim.SetBool("Jump", true); staminaA -= 10; jumps -= 1; }
        //Seta o estado do player correndo ou não
        if (run != 0) { running = true; } else { running = false; }
        //Aplica a velocidade no player dependo das circunstâncias
        if (movX != 0 && movY == 0){
            if(running == false) {
                player.velocity = new Vector3(velocity * movX, player.velocity.y);
                onMove = true;
            }
            else if(running == true && staminaA > 0) {
                player.velocity = new Vector3((velocity * 1.4f) * movX, player.velocity.y);
                staminaA -= 0.3f;
                onMove = true;
            }
            else if(running == true && staminaA <= 0) {  player.velocity = new Vector3(velocity * movX, player.velocity.y); }            
            // ------------------------------------------------------------------------------
            if (movX > 0) {
                transform.localScale = new Vector3(0.4f, 0.4f, 1);
            }
            else {
                transform.localScale = new Vector3(-0.4f, 0.4f, 1);
            }
        }
        //Para o player caso nenhum botão de movimento esteja pressionado
        if (movX == 0) { player.velocity = new Vector3(0, player.velocity.y); onMove = false; }
    }
    //Função de tomar dano
    void TakingDamage()
    {
        //Se o tempo de invencibilidade for maior que 0 o timer inicia se for menor para de piscar
        if(timeInv >= 0) { timeInv -= Time.deltaTime; timeP -= Time.deltaTime; }
        else {
            turn = true;
            head.enabled = true;
            torso.enabled = true;
            foot1.enabled = true;
            foot2.enabled = true;
            weapon1.enabled = true;
            weapon2.enabled = true;
        }
        //Caso vida atual seja maior que vida máxima vida atual vai ser igual a máxima
        if(life > maxLife) { life = maxLife; }
        //Se tomar dano o tempo de invencibilidade é setado para 2 segundos
        if(takingDamage == true) { timeInv = 2; }
        //Reseta o timer piscando e decide entre todas as partes ativas ou não
        if(timeP < 0) { timeP = 0.15f; if(turn == true) { turn = false; } else { turn = true; } }
        //Sprites renderes ativos
        if (turn == true) {
            head.enabled = true;
            torso.enabled = true;
            foot1.enabled = true;
            foot2.enabled = true;
            weapon1.enabled = true;
            weapon2.enabled = true;
        }
        //Sprites renderes desativos
        else {
            head.enabled = false;
            torso.enabled = false;
            foot1.enabled = false;
            foot2.enabled = false;
            weapon1.enabled = false;
            weapon2.enabled = false;
        }
        //Toma dano do chute - não perde vida
        if(onkick == true) { player.velocity = new Vector3(-10, player.velocity.y); }
        //Corações da HUD
        if (life == 6) { heart1.enabled = true; heart2.enabled = true; heart3.enabled = true; heart4.enabled = true; heart5.enabled = true; heart6.enabled = true; }
        else if (life == 5) { heart1.enabled = true; heart2.enabled = true; heart3.enabled = true; heart4.enabled = true; heart5.enabled = true; heart6.enabled = false; }
        else if (life == 4) { heart1.enabled = true; heart2.enabled = true; heart3.enabled = true; heart4.enabled = true; heart5.enabled = false; heart6.enabled = false; }
        else if (life == 3) { heart1.enabled = true; heart2.enabled = true; heart3.enabled = true; heart4.enabled = false; heart5.enabled = false; heart6.enabled = false; }
        else if (life == 2) { heart1.enabled = true; heart2.enabled = true; heart3.enabled = false; heart4.enabled = false; heart5.enabled = false; heart6.enabled = false; }
        else if (life == 1) { heart1.enabled = true; heart2.enabled = false; heart3.enabled = false; heart4.enabled = false; heart5.enabled = false; heart6.enabled = false; }
        else if (life <= 0) { heart1.enabled = false; heart2.enabled = false; heart3.enabled = false; heart4.enabled = false; heart5.enabled = false; heart6.enabled = false; dead = true; }
    }

    //Estados das animações -----------------------------------------------------------
    public void SwordPickStart() { canMove = false; if (attackMode == true) { attackMode = false; } else { attackMode = true; } }
    public void SwordPickEnd() { canMove = true; swordState = 0; }

    void StopMove() { canMove = false; player.velocity = new Vector3(0, player.velocity.y); }
    void StartMove() { canMove = true; jumps = 1; }

    void AttackStart() { canMove = false; attacking = true; }
    void AttackEnd() { canMove = true; anim.SetBool("Attack", false); anim.SetBool("StrongAttack", false); attacking = false; }

    void TakingDamageStart() { if (takingDamage == true) { life -= 1; } takingDamage = false; if (kick == true) { onkick = true; } kick = false; canMove = false; player.velocity = new Vector3(0, player.velocity.y); }
    void TakingDamageEnd() { takingDamage = false; onkick = false; canMove = true; anim.SetBool("Attack", false); anim.SetBool("StrongAttack", false); }

    void DeathEnd() { end = true; dead = false; attackMode = true; head.enabled = true;
        torso.enabled = true;
        foot1.enabled = true;
        foot2.enabled = true;
        weapon1.enabled = true;
        weapon2.enabled = true;
        fade.SetBool("FadeIn", true); fade.SetBool("End", false);
    }

    void Death() {
        transform.position = spawn.position;
        end = false;
        life = maxLife;
    }

    void DeathFade() {
        center.moneyA = 0;
        fade.SetBool("FadeIn", false); fade.SetBool("End", true);
    }
    //---------------------------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D collision) {
        //Colisão com arma 
        if (collision.tag == "Enemy" && takingDamage == false && timeInv <= 0) { takingDamage = true; }
        //Interagir
        if (collision.tag == "Interagir") { canUse = true; }
        //Chute
        if (collision.tag == "Kick" && takingDamage == false && timeInv <= 0) { kick = true; }
        //Morte
        if (collision.tag == "Kill") { dead = true; }
    }
    private void OnTriggerStay2D(Collider2D collision) {
        //Colisão com arma Ficar
        if (collision.tag == "Enemy" && takingDamage == false && timeInv <= 0) { takingDamage = true; }
        //Interagir Ficar
        if (collision.tag == "Interagir") { canUse = true; }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        //Interagir Sair
        if (collision.tag == "Interagir") { canUse = false; }
    }
}