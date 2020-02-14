using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public bool onTeleport;
    bool FadeIn;
    public bool elevator;
    public bool door;
    bool elevatorActive;

    float timer;

    Animator anim;
    public Transform TeleportPos;
    Animator fade;
    PlayerMovement player;
    void Start()
    {
        if(elevator == true) { anim = GetComponent<Animator>(); }
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
    }

    void Update()
    {
        if (onTeleport == true && elevator == true && door == false) { anim.SetBool("Open", true); }
        else if(onTeleport == false && elevator == true && door == false) { anim.SetBool("Open", false); }

        if(onTeleport == true && elevator == false) { TeleportPlayer(); }
        if(onTeleport == true && elevator == true) { if (Input.GetKeyDown(KeyCode.E)) { elevatorActive = true; } }
        if(elevatorActive == true) { TeleportPlayer(); }
    }

    void TeleportPlayer()
    {
        if(onTeleport == true)
        {
            player.GetComponent<PlayerMovement>().anim.SetBool("Jump", false);
            if (timer > 0) { timer -= Time.deltaTime; }
            if (timer <= 0) { if (FadeIn == true) { FadeIn = false; timer = 1; } else { FadeIn = true; timer = 1; } }

            if (FadeIn == false) { fade.SetBool("FadeIn", true); fade.SetBool("End", false); player.canMove = false; player.teleporting = true; if (elevator == false) { player.GetComponent<BoxCollider2D>().enabled = false; } }
            else { fade.SetBool("FadeIn", false); fade.SetBool("End", true); onTeleport = false; elevatorActive = false; FadeIn = false; player.transform.position = new Vector3(TeleportPos.position.x, TeleportPos.position.y, 0); player.canMove = true; player.teleporting = false; if(elevator == false) { player.GetComponent<BoxCollider2D>().enabled = true; } }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            onTeleport = true;
            timer = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && elevator == true)
        {
            onTeleport = false;
        }
    }
}
