using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    bool damage;
    public Boss boss;
    void Update()
    {
        if(damage == true)
        {
            boss.takingDamage = true;
            damage = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spike") { damage = true; }
    }
}
