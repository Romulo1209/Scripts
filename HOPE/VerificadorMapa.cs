using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificadorMapa : MonoBehaviour
{
    public List<GameObject> pos;
    public int id;
    public GameObject camMapa;
    public bool mudou;
    public float timer;
    void Start()
    {
        camMapa = GameObject.FindGameObjectWithTag("Mini");
    }

    void Update()
    {
        if (mudou == true) { Desativar(); }
        pos[id].GetComponent<SpriteRenderer>().enabled = true; 
    }

    void Desativar()
    {
        mudou = false;
        pos[0].GetComponent<SpriteRenderer>().enabled = false;
        pos[1].GetComponent<SpriteRenderer>().enabled = false;
        pos[2].GetComponent<SpriteRenderer>().enabled = false;
        pos[3].GetComponent<SpriteRenderer>().enabled = false;
        pos[4].GetComponent<SpriteRenderer>().enabled = false;
        pos[5].GetComponent<SpriteRenderer>().enabled = false;
        pos[6].GetComponent<SpriteRenderer>().enabled = false;
        pos[7].GetComponent<SpriteRenderer>().enabled = false;
        pos[8].GetComponent<SpriteRenderer>().enabled = false;
        
    }
}
