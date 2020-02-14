using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MonoBehaviour
{
    bool turn;
    bool right = true;
    bool onSight;
    bool bite;
    bool attack;
    public bool canMove = true;

    bool end;
    bool takingDamage;
    bool blink;
    float timeInv;
    float timeP;
    float maxLife = 2;
    float life;

    Animator anim;
    LayerMask layer;
    Rigidbody2D control;
    PlayerMovement player;
    void Start()
    {
        anim = GetComponent<Animator>();
        control = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        layer = LayerMask.GetMask("Corpos");
        life = maxLife;
    }

    void FixedUpdate()
    {
        if(life > 0)
        {
            Vision();
            TakingDamage();
            if (canMove == true)
            {
                if (bite == false)
                {
                    if (onSight == true) { Follow(); }
                    else { Patrol(); }
                }
                else
                {
                    Attack();
                }
            }
            else { control.velocity = new Vector3(0, control.velocity.y); anim.SetBool("Attack", false); }
        }
        else if (life <= 0)
        {
            if(end == false) { anim.SetBool("Death", true); anim.SetBool("Follow", false); anim.SetBool("Attack", false); control.velocity = new Vector3(0, control.velocity.y); }
        }
    }

    void Attack()
    {
        if(attack == true) { anim.SetBool("Attack", true); } else { anim.SetBool("Attack", false); }
    }

    void Vision()
    {
        RaycastHit2D Visao = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 5, layer);
        if (Visao.collider != null)
        {
            if (Visao.collider.tag == "Player")
            {
                Debug.DrawLine(transform.position, player.transform.position, Color.red);
                onSight = true;
            }
        }
        else { onSight = false; }
    }

    void TakingDamage()
    {
        if(life > 0)
        {
            if (timeInv >= 0) { timeInv -= Time.deltaTime; timeP -= Time.deltaTime; }

            if (takingDamage == true) { anim.SetBool("Damage", true); } else { anim.SetBool("Damage", false); }
            if (takingDamage == true && timeInv <= 0)
            {
                timeInv = 1;
            }
        }
        else
        {
            return;
        }
    }

    void Follow()
    {
        anim.SetBool("Patrol", false);
        anim.SetBool("Follow", true);

        RaycastHit2D distance = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 0.5f, layer);
        if(distance.collider != null) { if (distance.collider.tag == "Player") { bite = true; attack = true; } }

        if (player.transform.position.x - transform.position.x > 0) { right = true; } else { right = false; }

        if(right == true && bite == false)
        {
            control.velocity = new Vector3(6, control.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(right == false && bite == false)
        {
            control.velocity = new Vector3(-6, control.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if(bite == true) { control.velocity = new Vector3(0, control.velocity.y); }
    }

    void Patrol()
    {
        anim.SetBool("Patrol", true);
        anim.SetBool("Follow", false);

        if (right == true)
        {
            Debug.DrawLine(new Vector3(transform.position.x + 2, transform.position.y - 0.5f, transform.position.z), new Vector3(transform.position.x + 1.5f, transform.position.y - 0.5f, transform.position.z), Color.red);
            RaycastHit2D groundCheck = Physics2D.Raycast(new Vector3(transform.position.x + 2, transform.position.y - 0.5f, transform.position.z), -Vector2.right, 0.5f);
            if (groundCheck.collider == null) { turn = true; }
        }
        else
        {
            Debug.DrawLine(new Vector3(transform.position.x - 2, transform.position.y - 0.5f, transform.position.z), new Vector3(transform.position.x - 1.5f, transform.position.y - 0.5f, transform.position.z), Color.red);
            RaycastHit2D groundCheck = Physics2D.Raycast(new Vector3(transform.position.x - 2, transform.position.y - 0.5f, transform.position.z), Vector2.right, 0.5f);
            if (groundCheck.collider == null) { turn = true; }
        }
        
        if(right == true) { control.velocity = new Vector3(1.5f, control.velocity.y); transform.localScale = new Vector3(1f, 1f, 1); }
        else { control.velocity = new Vector3(-1.5f, control.velocity.y); transform.localScale = new Vector3(-1f, 1f, 1); }
        
        if(turn == true)
        {
            if(right == true) { right = false; } else { right = true; }
            if(right == true) { transform.localScale = new Vector3(1, 1, 1); }
            else { transform.localScale = new Vector3(-1, 1, 1); }
            turn = false;
        }
    }

    void AttackStart() { attack = false; }
    void AttackEnd() { bite = false; }

    void AttackJumpBack() { if (right == true) { control.velocity = new Vector2(-5, 5); } else { control.velocity = new Vector2(5, 5); } }

    void DamageStart() { canMove = false; takingDamage = false; bite = false; }
    void DamageEnd() { canMove = true; }

    void DeathStart() { anim.SetBool("Death", false); end = true; }
    void DeathEnd() { Destroy(this.gameObject); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && takingDamage == false && timeInv <= 0)
        {
            takingDamage = true;
            life -= 1;
        }

        if (collision.tag == "Kill")
        {
            life -= 999;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && takingDamage == false && timeInv <= 0)
        {
            takingDamage = true;
            life -= 1;
        }
    }
}