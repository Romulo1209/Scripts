using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerCycle : MonoBehaviour
{
    float timeWalk;
    float timeStay;

    public bool walk;
    public bool turnS;

    Rigidbody2D obj;
    Animator anim;
    void Start()
    {
        obj = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        Debug.DrawLine(new Vector2(transform.position.x - 1, transform.position.y - 1), new Vector2(transform.position.x + 1, transform.position.y - 1), Color.red);
        if(walk == true) { Walk(); }
        else { Stop(); }
    }

    void Walk()
    {
        timeWalk -= Time.deltaTime;
        if(timeWalk <= 0) { walk = false; Turn(); }
        if (turnS == false) { obj.velocity = new Vector2(-2, obj.velocity.y); }
        else { obj.velocity = new Vector2(2, obj.velocity.y); }
        anim.SetBool("Walk", true);

        if(turnS == true)
        {
            RaycastHit2D wallCheck = Physics2D.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y - 1 , transform.position.z), Vector2.right, 0.5f);
            if (wallCheck.collider == null) { Turn(); walk = false; }
        }
        else
        {
            RaycastHit2D wallCheck = Physics2D.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y - 1, transform.position.z), -Vector2.right, 0.5f);
            if (wallCheck.collider == null) { Turn(); walk = false; }
        }
        
    }

    void Stop()
    {
        timeStay -= Time.deltaTime;
        if (timeStay <= 0) { walk = true; }
        obj.velocity = new Vector2(0, obj.velocity.y);
        anim.SetBool("Walk", false);

    }

    void Turn()
    {
        if(turnS == true) { transform.eulerAngles = new Vector3(0, 180, 0); turnS = false; RandomTime(); return; }
        else { transform.eulerAngles = new Vector3(0, 0, 0); turnS = true; RandomTime(); return; }
    }

    void RandomTime()
    {
        timeWalk = Random.Range(1, 5);
        timeStay = Random.Range(1, 5);
    }
}