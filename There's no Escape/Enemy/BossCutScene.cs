using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutScene : MonoBehaviour
{
    bool start;
    float timer = 4;

    public Animator boss;
    public Animator camAnim;
    public PlayerMovement player;
    public GameObject bossAfterDeath;
    BoxCollider2D box;
    Animator anim;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (start == true)
        {
            if(timer < 2f) { boss.enabled = true; }
            player.canMove = false;
            player.player.velocity = new Vector2(0, player.player.velocity.y);
            anim.SetBool("Open", false);
            timer -= Time.deltaTime;
            camAnim.enabled = true;
            camAnim.SetBool("Boss", true);
            if(timer <= 0) { camAnim.SetBool("Boss", false); camAnim.enabled = false; player.canMove = true; start = false; box.enabled = false; bossAfterDeath.SetActive(true); }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            start = true;
        }
    }
}
