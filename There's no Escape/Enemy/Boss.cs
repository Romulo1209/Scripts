using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    float damage;
    float life = 3;
    bool attack;
    bool right;
    public bool takingDamage;
    public bool damageEnd;

    public PlayerMovement player;
    public Animator doorAnim;
    LayerMask layer;
    Animator anim;
    Rigidbody2D control;
    void Start()
    {
        anim = GetComponent<Animator>();
        control = GetComponent<Rigidbody2D>();
        layer = LayerMask.GetMask("Corpos");
    }

    void FixedUpdate()
    {
        if(damageEnd == true)
        {
            if (life <= 0)
            {
                anim.SetBool("DamageLong", false);
                anim.SetBool("DamageShort", false);
                anim.SetBool("Death", true);
            }

            if (takingDamage == false)
            {
                if (attack == false) { Follow(); }
                else { control.velocity = new Vector3(0, control.velocity.y); anim.SetBool("Attack", true); anim.SetBool("Run", false); }
            }
            else
            {
                if (damage < 2) { anim.SetBool("DamageShort", true); control.velocity = new Vector3(0, control.velocity.y); }
                else { anim.SetBool("DamageLong", true); control.velocity = new Vector3(0, control.velocity.y); }
            }   
        }
        else
        {
            control.velocity = new Vector3(0, control.velocity.y);
        }
    }

    void Follow()
    {
        RaycastHit2D distance = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 1.5f, layer);
        if (distance.collider != null) { if (distance.collider.tag == "Player") { attack = true; } }

        if (player.transform.position.x - transform.position.x > 0) { right = true; } else { right = false; }

        if (right == true && attack == false)
        {
            anim.SetBool("Run", true);
            control.velocity = new Vector3(6, control.velocity.y);
            transform.localScale = new Vector3(0.4123046f, 0.4123046f, 1);
        }
        else if (right == false && attack == false)
        {
            anim.SetBool("Run", true);
            control.velocity = new Vector3(-6, control.velocity.y);
            transform.localScale = new Vector3(-0.4123046f, 0.4123046f, 1);
        }
    }

    void AttackEnd() { anim.SetBool("Attack", false); attack = false; }

    void ScriptStart() { damageEnd = true; }

    void TakingLongDamageStart() { anim.SetBool("DamageLong", false); anim.SetBool("Attack", false); takingDamage = false; attack = false; damageEnd = false; life -= 1; }
    void TakingLongDamageEnd() { damage = 0; damageEnd = true; }

    void TakingShortDamageStart() { anim.SetBool("DamageShort", false); anim.SetBool("Attack", false); takingDamage = false; attack = false; damageEnd = false; }
    void TakingShortDamageEnd() { damage += 1; damageEnd = true; }

    void DeathStart() { anim.SetBool("Death", false); damageEnd = false; }
    void DeathEnd() { doorAnim.SetBool("Open", true); }
}
