using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour
{
    bool inside;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inside == true && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(6);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { inside = true; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { inside = false; }
    }
}
