using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    Animator anim;
    bool active;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(active == true) { anim.SetBool("Active", true); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") { active = true; }
    }
}
