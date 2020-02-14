using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldTime : MonoBehaviour
{
    public float timeClock;
    public float timePeople;
    RectTransform Clock;
    public bool day;
    [SerializeField]
    Center center;

    public Sprite dayR;
    public Sprite nightR;

    public bool nextDay;
    public bool inDia;
    public float dayCount = 1;
    public Text dayCText;

    public PayDay payday;
    public Image clockBack;
    public Text timeText;
    void Start()
    {
        Clock = GetComponent<RectTransform>();
        day = true;
    }

    void FixedUpdate()
    {
        if(inDia == false) { Tempo(); }
    }

    void Tempo()
    {
        dayCText.text = "Dia " + dayCount;
        //Rélogio
        timeClock -= Time.deltaTime;
        timePeople -= Time.deltaTime;
        if (day == true) { Clock.Rotate(new Vector3(0, 0, -0.06f)); } //2min dia
        else { Clock.Rotate(new Vector3(0, 0, -0.12f)); } //1min noite

        if(nextDay == true) { dayCount += 1; nextDay = false; }

        if(day == true)
        {
            if (timeClock <= -120) { day = false; timeText.text = "Night"; timeClock = 0; }
        }
        else
        {
            if (timeClock <= -60) { day = true; timeText.text = "Day"; timeClock = 0; nextDay = true; payday.endDay = true; }
        }

        if(day == true)
        {
           clockBack.sprite = dayR;
        }
        else
        {
            clockBack.sprite = nightR;
        }

        if (timePeople <= -180)
        {
            if (center.peopleL + center.peopleW < center.peopleT) { center.peopleL += 1; }
            timePeople = 0;
        }
    }
}
