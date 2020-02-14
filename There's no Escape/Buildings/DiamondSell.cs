using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSell : MonoBehaviour
{
    bool canInteract;
    float diamondPrice;
    float diamondSell;

    public Storage storage;
    public Center center;
    void Update()
    {
        Sell();
    }

    void Sell()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract == true)
        {
            if(storage.diamondA > 0)
            {
                if (diamondSell >= 0 && diamondSell < 10) { diamondPrice = Random.Range(20, 30); }
                else if (diamondSell > 10 && diamondSell < 20) { diamondPrice = Random.Range(15, 25); }
                else if (diamondSell > 20 && diamondSell < 30) { diamondPrice = Random.Range(10, 20); }
                else if (diamondSell > 40) { diamondPrice = Random.Range(5, 15); }

                storage.diamondA -= 1;
                center.moneyA += diamondPrice;
                diamondSell += 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;
        }
    }
}
