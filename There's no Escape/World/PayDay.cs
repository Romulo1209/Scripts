using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PayDay : MonoBehaviour
{
    bool onPayday;

    float daysFree = 2;
    float money;
    float extraMoney;
    public bool endDay;
    bool payed;

    Animator anim;

    bool day;
    public Transform PosDay;
    public Transform PosNight;

    public Text moneyNeedText;

    public GameObject paydayHud;
    public WorldTime time;
    public Center center;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Hud();
        PaySystem();
        OnMove();
    }

    void OnMove()
    {
        if(time.day == true) { day = true; } else { day = false; }
        if(day == false && payed == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(PosNight.transform.position.x, transform.position.y, transform.position.z), 0.06f);
            transform.localScale = new Vector3(-0.3512389f, 0.3512389f, 0.3512389f);
            if(transform.position == new Vector3(PosNight.transform.position.x, transform.position.y, transform.position.z))
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Wait", true);
            }
            else
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Wait", false);
            }
        }
        else if(payed == true || day == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(PosDay.transform.position.x, transform.position.y, transform.position.z), 0.06f);
            transform.localScale = new Vector3(0.3512389f, 0.3512389f, 0.3512389f);
            if (transform.position == new Vector3(PosNight.transform.position.x, transform.position.y, transform.position.z))
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Wait", false);
            }
            else
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Wait", false);
            }
        }
    }

    void PaySystem()
    {
        money = time.dayCount + extraMoney;
        moneyNeedText.text = money.ToString();

        if (Input.GetKeyDown(KeyCode.E) && center.moneyA >= money && onPayday == true && payed == false)
        {
            payed = true;
            center.moneyA -= money;
        }        

        if(endDay == true)
        {
            if(payed == false)
            {
                extraMoney += (time.dayCount - 1) + center.peopleL + center.peopleW;
                daysFree -= 1;
            }
            if(payed == true)
            {
                payed = false;
                extraMoney = 0;
                daysFree = 3;
            }
            endDay = false;
        }

        if (daysFree <= 0)
        {
            SceneManager.LoadScene(5);
            //GAMEOVER
        }
    }

    void Hud()
    {
        if (onPayday == false || payed == true) { paydayHud.SetActive(false); }
        else if (onPayday == true) { paydayHud.SetActive(true); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" )
        {
            onPayday = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            onPayday = false;
        }
    }
}
