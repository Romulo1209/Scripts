using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    //Valores
    public float dialogueSpeed;
    public string dialogue;
    string currentLetter;
    int cont;
    //Condições
    public bool OneWay;
    public bool Door;
    public bool Another;
    public bool Iki;
    public bool Tutorial;
    public bool active;
    bool canActive;
    bool finalize;
    bool choice;
    bool direct;
    bool inDia;
    bool diaStart;
    public bool jumpDia;
    float confirm;
    int contTutorial;
    //Objetos
    public Text dialogueText;
    public Animator door;
    public Animator cam;
    public GameObject anotherDia;
    public List<string> dialogs;
    public PlayerMovement player;
    public WorldTime worldTime;
    public GameObject obj1;
    public GameObject obj2;
    Animator anim;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        worldTime = GameObject.FindGameObjectWithTag("WorldTime").GetComponent<WorldTime>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(canActive == true && diaStart == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                diaStart = true;
                player.inDialogue = true;
                direct = true;
                cont = 0;
                active = true;
            }
        }

        if(diaStart == true)
        {
            DialogueFunc();
        }
    }

    void DialogueFunc()
    {
        //Dialogo Ativo
        if (active == true)
        {
            confirm = Input.GetAxis("Confirm");
            //Para o Player
            player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
            //Para o relógio
            worldTime.inDia = true;
            //Animação Falando
            if (inDia == false) { if (Iki == true) { anim.SetBool("Talk", false); } }
            else
            {
                if (Input.GetKeyDown(KeyCode.E)) { dialogueSpeed = 0.000001f; }
                //Rindo
                if (dialogs[cont] == "*Risadas* ") { anim.SetBool("Laugh", true); }
                if (dialogs[cont] == "*Escolha* ")
                {
                    dialogueSpeed = 0.000001f;
                    choice = true;
                    dialogue = "Y para ouvir" + "\n" + "N para pular ";
                }
                if (dialogs[cont] == "// ") { jumpDia = true; }
                //Volta animação de falar
                if (Iki == true) { anim.SetBool("Talk", true); }
            }
            //Finaliza Dialogo
            if (dialogs[cont] == "")
            {
                active = false;
                if (OneWay == true)
                {
                    if (Another == true) { anotherDia.SetActive(true); }
                    if (Door == true) { door.SetBool("Open", true); }
                }
            }
            //Anim
            if (jumpDia == true)
            {
                if (Iki == true) { anim.SetBool("Laugh", false); }
                cont += 1;
                dialogue = dialogs[cont];
                jumpDia = false;
                contTutorial += 1;
                return;
            }
            //Passa para proxima frase
            if (Input.GetKeyDown(KeyCode.E) && inDia == false && choice == false && jumpDia == false)
            {
                if (Iki == true) { anim.SetBool("Laugh", false); }
                dialogue = dialogs[cont];
                StartCoroutine(ShowText());
            }
            if (choice == true)
            {
                if (confirm > 0)
                {
                    obj1.SetActive(true);
                    active = false;
                    Destroy(gameObject);
                }

                if (confirm < 0)
                {
                    obj2.SetActive(true);
                    active = false;
                    Destroy(gameObject);
                }
            }
            if (Tutorial == true)
            {
                cam.enabled = true;
                if(contTutorial == 0) { cam.SetInteger("Tutorial", 0); }
                else if (contTutorial == 1) { cam.SetInteger("Tutorial", 1); }
                else if (contTutorial == 2) { cam.SetInteger("Tutorial", 2); }
                else if (contTutorial == 3) { cam.SetInteger("Tutorial", 3); }
                else if (contTutorial == 4) { cam.SetInteger("Tutorial", 4); }
                else if (contTutorial == 5) { cam.SetInteger("Tutorial", 5); }
                else if (contTutorial == 6) { cam.SetInteger("Tutorial", 6); }
                else if (contTutorial == 7) { cam.SetInteger("Tutorial", 7); }
                else if (contTutorial == 8) { cam.SetInteger("Tutorial", 8); }
                else if (contTutorial == 9) { cam.SetInteger("Tutorial", 9); }
                else if (contTutorial == 10) { cam.SetInteger("Tutorial", 10); }
            }
        }
        //Desliga tudo e finaliza o dialogo
        else
        {
            contTutorial = 0;
            cam.enabled = false;
            worldTime.inDia = false;
            if (Iki == true) { anim.SetBool("Laugh", false); anim.SetBool("Talk", false); }
            player.inDialogue = false;
            direct = false;
            active = false;
            diaStart = false;
            if(OneWay == true) { Destroy(gameObject); }
        }
    }
    //Colisoes
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") { canActive = true; }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { canActive = false; }
    }

    IEnumerator ShowText()
    {
        if (Input.GetKeyDown(KeyCode.E) || direct == true)
        {
            direct = false;
            dialogueSpeed = 0.05f;
            if (inDia == false)
            {
                for (int i = 0; i < dialogue.Length; i++)
                {
                    currentLetter = dialogue.Substring(0, i);
                    dialogueText.text = currentLetter;
                    yield return new WaitForSeconds(dialogueSpeed);
                    inDia = true;

                    if (i == (dialogue.Length - 1))
                    {
                        cont += 1;
                        inDia = false;
                    }
                }
            }
        }
    }
}