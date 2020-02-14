using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usaveis : MonoBehaviour
{
    public bool usou;
    public bool semItem;
    public bool Timer;
    public int id;

    private float timerBuff;
    private Player player;
    private Inimigo inimigo;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        inimigo = GameObject.FindGameObjectWithTag("Inimigo").GetComponent<Inimigo>();
    }

    void Update()
    {
        if(usou == true)
        {
            id = Random.Range(0, 4);
            Buff();
            semItem = false;
            usou = false;
        }
        if(Timer == true)
        {
            Contagem();
        }
    }

    void Contagem()
    {
        timerBuff -= Time.deltaTime;
        if(timerBuff <= 0)
        {
            if (id == 0)//+Velocidade Player 3s
            {
                player.velocidade /= 2;
                Timer = false;
                semItem = true;
            }
            if (id == 1)//+Velocidade Inimigo 3s
            {
                inimigo.velocidadeAtual /= 2;
                Timer = false;
                semItem = true;
            }
            if (id == 2)//-Velocidade Player 3s
            {
                player.velocidade *= 2;
                Timer = false;
                semItem = true;
            }
            if (id == 3)//-Velocidade Inimigo 3s
            {
                inimigo.velocidadeAtual *= 2;
                Timer = false;
                semItem = true;
            }
            if (id == 4)//Inimigo Parar 3s
            {
                inimigo.enabled = true;
                Timer = false;
                semItem = true;
            }
        } 
    }

    void Buff()
    {
        if (id == 0)//+Velocidade Player 3s
        {
            player.velocidade *= 2;
            timerBuff = 3;
            Timer = true;
            Debug.Log("+velP");
        }
        if (id == 1)//+Velocidade Inimigo 3s
        {
            inimigo.velocidadeAtual *= 2;
            timerBuff = 3;
            Timer = true;
            Debug.Log("+velI");
        }
        if (id == 2)//-Velocidade Player 3s
        {
            player.velocidade /= 2;
            timerBuff = 3;
            Timer = true;
            Debug.Log("-velP");
        }
        if (id == 3)//-Velocidade Inimigo 3s
        {
            inimigo.velocidadeAtual /= 2;
            timerBuff = 3;
            Timer = true;
            Debug.Log("-velI");
        }
        if (id == 4)//Inimigo Parar 3s
        {
            inimigo.enabled = false;
            timerBuff = 3;
            Timer = true;
            Debug.Log("Desativado");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Random" && semItem == true)
        {
            usou = true;
            Destroy(col.gameObject);
        }
    }
}
