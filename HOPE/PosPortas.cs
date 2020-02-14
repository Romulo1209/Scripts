using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosPortas : MonoBehaviour
{
    public bool gerar;
    public bool rand;
    public int contagem;
    public int random;
    public int numeracao = 19;
    public List<AtivadorSala> Salas;
    public List<GameObject> Pos;
    public List<AtivadorSala> SalasR;
    public List<GameObject> PosR;

    void Update()
    {
        if(numeracao == -1) { rand = false; }
        if (gerar == true)
        {
            SalasR.Add(Salas[0]);
            SalasR.Add(Salas[1]);
            SalasR.Add(Salas[2]);
            SalasR.Add(Salas[3]);
            SalasR.Add(Salas[4]);
            SalasR.Add(Salas[5]);
            SalasR.Add(Salas[6]);
            SalasR.Add(Salas[7]);
            SalasR.Add(Salas[8]);
            SalasR.Add(Salas[9]);
            SalasR.Add(Salas[10]);
            SalasR.Add(Salas[11]);
            SalasR.Add(Salas[12]);
            SalasR.Add(Salas[13]);
            SalasR.Add(Salas[14]);
            SalasR.Add(Salas[15]);

            PosR.Add(Pos[0]);
            PosR.Add(Pos[1]);
            PosR.Add(Pos[2]);
            PosR.Add(Pos[3]);
            PosR.Add(Pos[4]);
            PosR.Add(Pos[5]);
            PosR.Add(Pos[6]);
            PosR.Add(Pos[7]);
            PosR.Add(Pos[8]);
            PosR.Add(Pos[9]);
            PosR.Add(Pos[10]);
            PosR.Add(Pos[11]);
            PosR.Add(Pos[12]);
            PosR.Add(Pos[13]);
            PosR.Add(Pos[14]);
            PosR.Add(Pos[15]);

            numeracao = 15;
            rand = true;
            gerar = false;
        }
        if (numeracao != -1 && rand == true)
        {
            random = Random.Range(0, PosR.Capacity);
            if (contagem == 0 && SalasR[0].name == "Porta1" && PosR[random].name == "TP1") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta2" && PosR[random].name == "TP2") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta3" && PosR[random].name == "TP3") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta4" && PosR[random].name == "TP4") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta5" && PosR[random].name == "TP5") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta6" && PosR[random].name == "TP6") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta7" && PosR[random].name == "TP7") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta8" && PosR[random].name == "TP8") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta9" && PosR[random].name == "TP9") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta10" && PosR[random].name == "TP10") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta11" && PosR[random].name == "TP11") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta12" && PosR[random].name == "TP12") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta13" && PosR[random].name == "TP13") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta14" && PosR[random].name == "TP14") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta15" && PosR[random].name == "TP15") { Debug.Log("pinto"); return; }
            else if (contagem == 0 && SalasR[0].name == "Porta16" && PosR[random].name == "TP16") { Debug.Log("pinto"); return; }

            if (contagem == 0 && SalasR[0].name == "Porta13" && PosR[random].name == "TP9" || SalasR[contagem].name == "Porta13" && PosR[random].name == "TP10") { return; }
            if (contagem == 0 && SalasR[0].name == "Porta11" && PosR[random].name == "TP9" || SalasR[contagem].name == "Porta11" && PosR[random].name == "TP10") { return; }
            if (contagem == 0 && SalasR[0].name == "Porta7" && PosR[random].name == "TP6" || SalasR[contagem].name == "Porta7" && PosR[random].name == "TP8") { return; }
            if (contagem == 0 && SalasR[0].name == "Porta15" && PosR[random].name == "TP6" || SalasR[contagem].name == "Porta15" && PosR[random].name == "TP7") { return; }
            if (contagem == 0 && SalasR[0].name == "Porta1" && PosR[random].name == "TP2" || SalasR[contagem].name == "Porta1" && PosR[random].name == "TP3") { return; }
            SalasR[contagem].scenePosition = PosR[random].GetComponent<Transform>();
            SalasR.RemoveAt(contagem);
            PosR.RemoveAt(random);
            numeracao -= 1;
            PosR.Capacity -= 1;
        }
    }
}
