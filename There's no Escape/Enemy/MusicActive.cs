using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicActive : MonoBehaviour
{
    bool inside;

    public AudioSource music;
    public AudioSource bossMusic;
    void Start()
    {
        
    }

    void Update()
    {
        if (inside == true) { music.enabled = false; bossMusic.enabled = true; } else { music.enabled = true; bossMusic.enabled = false; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { inside = true; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { inside = false; }
    }
}
