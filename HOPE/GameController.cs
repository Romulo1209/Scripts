//script para centralizar comandos do jogo
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class GameController : Singleton<GameController>
{
    private FadeEffect fade;
     // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType(typeof(FadeEffect)) as FadeEffect;
     } 

    public void IniciarJogo()
    {
         fade.Inicio(); 
     }
    public void Transicao(int refe)
    {
        fade.TransicaoDeCenario(refe);
    }
    public void Transicao(Transform refe)
    {
        fade.TransicaoDeCenario(refe);
    }




}
