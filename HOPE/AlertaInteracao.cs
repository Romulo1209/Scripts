using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AlertaInteracao : MonoBehaviour
{
    Transform alerta;

    public void Interagir()
    {
        if(alerta == null)
        {
            GameObject obj = new GameObject("btn");
            alerta = obj.transform;
            alerta.position = transform.position;
            SpriteRenderer btn = obj.AddComponent<SpriteRenderer>();
            btn.sprite = Resources.Load<Sprite>("Sprites/BotaoE");
            btn.sortingOrder = 2;
        }
    }
    public void Sair()
    {
        if (alerta)
        {
            Destroy(alerta.gameObject); 
        }
    }
}
