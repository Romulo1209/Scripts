using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeEffect : MonoBehaviour
{
    private Image fundo;
    CanvasGroup hud;
     // Start is called before the first frame update
    void Awake()
    {
        hud = FindObjectOfType(typeof(CanvasGroup)) as CanvasGroup; 
        fundo = GameObject.Find("FundoFade").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //função que starta o jogo
    public void Inicio()
    {
        StartCoroutine(_start(1)); 
    }
    public void TransicaoDeCenario(Transform posicao)
    {
        StartCoroutine(_start(posicao));
    }
    public void TransicaoDeCenario(int posicao)
    {
        StartCoroutine(_start(posicao));
    }

    IEnumerator _start(Transform _posicaoFinal)
    {
        float i = 0;
        while (fundo.color.a < 1)
        {
            i += Time.deltaTime*0.7f;
            fundo.color = Vector4.MoveTowards(fundo.color,new Vector4(fundo.color.r, fundo.color.g, fundo.color.b,1),i);
            yield return true;
        }
        Transform pos = GameObject.Find("Player").transform;
        pos.position = _posicaoFinal.position;
        Camera.main.transform.position = new Vector3(_posicaoFinal.position.x,_posicaoFinal.position.y,-10);
        i = 0;
        while (fundo.color.a > 0)
        {
            i += Time.deltaTime * 0.7f;
            fundo.color = Vector4.MoveTowards(fundo.color, new Vector4(fundo.color.r, fundo.color.g, fundo.color.b, 0), i);
            yield return true;
        }
      }
    IEnumerator _start(int scene)
    {
        float i = 0;
        while (fundo.color.a < 1)
        {
            i += Time.deltaTime * 0.7f;
            fundo.color = Color.Lerp(fundo.color, new Color(fundo.color.r, fundo.color.g, fundo.color.b, 1), i);
            yield return true;
        }
        SceneManager.LoadScene(scene); 
        i = 0;
        while (fundo.color.a > 0)
        {
            i += Time.deltaTime * 0.7f;
            fundo.color = Color.Lerp(fundo.color, new Color(fundo.color.r, fundo.color.g, fundo.color.b, 0), i);
            yield return true;
        }
        hud.alpha = 1;
        hud.blocksRaycasts = true;
    }
}
