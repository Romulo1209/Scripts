using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificadorPlayer : MonoBehaviour
{
    public int id;
    public VerificadorMapa mapa;
    public bool seguir;
    private Inimigo inimigo;
    public Fases fases;

    void Start()
    {
        mapa = GameObject.FindGameObjectWithTag("Path").GetComponent<VerificadorMapa>();
        inimigo = GameObject.FindGameObjectWithTag("Inimigo").GetComponent<Inimigo>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        mapa.id = id;
        mapa.mudou = true;
        if(seguir == true && fases.fase >= 2) { inimigo.reset = true; }
    }
}
