using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSeguir : MonoBehaviour
{
    public bool fora;
    private Transform playerPos;
    private Rigidbody2D camera;
    public float velocidadeCamera;
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        camera = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Seguir(); 
    }

    void Seguir()
    {
        camera.velocity = new Vector2((playerPos.position.x - transform.position.x) * velocidadeCamera, (playerPos.position.y - transform.position.y) * velocidadeCamera);
    }
}
