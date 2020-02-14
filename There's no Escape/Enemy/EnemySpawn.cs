using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<Transform> enemiesPos;
    public List<GameObject> enemies;
    public List<GameObject> enemiesDestroy;

    public bool clear;
    bool playerDetect;
    bool open;
    bool spawn;

    public GameObject door;
    void Update()
    {
        if (clear == true) { door.GetComponent<Animator>().SetBool("Open", true); }
        else
        {
            if (playerDetect == false) { PlayerOff(); }
            if (playerDetect == true && clear == false) { PlayerOn(); }
            door.GetComponent<Animator>().SetBool("Open", false);
        }
    }

    void PlayerOn()
    {
        if(spawn == false)
        {
            for (int i = 0; i < enemiesPos.Count; i++)
            {
                int rand = Random.Range(0, enemies.Count);
                enemiesDestroy.Add(Instantiate(enemies[rand], enemiesPos[i].position, Quaternion.identity)); 
            }
            spawn = true;
        }
    }

    void PlayerOff()
    {
        for (int i = 0; i < enemiesDestroy.Count; i++)
        {
            Destroy(enemiesDestroy[i]);
            enemiesDestroy.RemoveAt(i);
            spawn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { playerDetect = true; }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Monster") { clear = false; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { playerDetect = false; }
        if (playerDetect == true) { if (collision.tag == "Monster") { clear = true; } }
    }
}
