using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBoss : MonoBehaviour
{
    bool inside;

    public GameObject boss;
    
    void Update()
    {
        if(inside == true) { boss.SetActive(true); } else { boss.SetActive(false); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") { inside = true; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player") { inside = false; }
    }
}
