using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Center : MonoBehaviour
{
    public GameObject window;
    public bool active;
    bool regen;
    float regentime;
    bool canActive;
    public float Level = 1;
    public Text levelT;

    public Text peopleTT;
    public Text peopleF;
    public Text moneyTT;
    public Text moneyHud;

    public Text peopleU;
    public Text moneyU;

    public Text woodU;
    public Text stoneU;
    public Text coalU;
    public Text ironU;
    public Text refinedU;
    public Text diamondU;

    public Image color1;
    public Image color2;
    public Image color3;
    public Image color4;
    public Image color5;
    public Image color6;

    public float peopleT;
    public float peopleW;
    public float peopleL = 1;

    public float moneyT;
    public float moneyA;

    float woodNeed;
    float stoneNeed;
    float coalNeed;
    float ironNeed;
    float refinedNeed;
    float diamondNeed;

    public Sprite Lv1;
    public Sprite Lv2;
    public Sprite Lv3;
    public Sprite Lv4;
    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;
    public GameObject Light4;
    public SpriteRenderer Gem;
    public SpriteRenderer Work1;
    public SpriteRenderer Work2;
    public SpriteRenderer Work3;

    public Storage storage;
    public PlayerMovement player;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canActive == true) { if (active == true) { active = false; } else { active = true; }  }

        if (canActive == false) { active = false; }

        if(moneyA > moneyT) { moneyA = moneyT; }

        Hud();
        if (Level == 1) { Level1(); }
        else if(Level == 2) { Level2(); }
        else if(Level == 3) { Level3(); }
        else if(Level == 4) { Level4(); }

        if (regen == true) { regentime += Time.deltaTime; if (regentime > 10) { player.life += 1; regentime = 0; } }

        if (active == true) { window.SetActive(true); }
        else { window.SetActive(false); }
    }

    void Hud()
    {
        levelT.text = "Nivel " + Level.ToString();

        peopleTT.text = (peopleL + peopleW).ToString() + " / " + peopleT.ToString();
        peopleF.text = "Pessoas desocupadas   " + peopleL.ToString();
        moneyTT.text = moneyA.ToString() + " / " + moneyT.ToString();
        moneyHud.text = moneyA.ToString() + " / " + moneyT.ToString();

        if(Level < 4)
        {
            woodU.text = woodNeed.ToString();
            stoneU.text = stoneNeed.ToString();
            coalU.text = coalNeed.ToString();
            ironU.text = ironNeed.ToString();
            refinedU.text = refinedNeed.ToString();
            diamondU.text = diamondNeed.ToString();
        }
        else
        {
            peopleU.text = "Max";
            moneyU.text = "Max";

            woodU.text = "Max";
            stoneU.text = "Max";
            coalU.text = "Max";
            ironU.text = "Max";
            refinedU.text = "Max";
            diamondU.text = "Max";
        }

        if (Level == 1) { Gem.sprite = Lv1; Light1.SetActive(true); Light2.SetActive(false); Light3.SetActive(false); Light4.SetActive(false); }
        else if (Level == 2) { Gem.sprite = Lv2; Light1.SetActive(false); Light2.SetActive(true); Light3.SetActive(false); Light4.SetActive(false); }
        else if (Level == 3) { Gem.sprite = Lv3; Light1.SetActive(false); Light2.SetActive(false); Light3.SetActive(true); Light4.SetActive(false); }
        else if (Level == 4) { Gem.sprite = Lv4; Light1.SetActive(false); Light2.SetActive(false); Light3.SetActive(false); Light4.SetActive(true); }

        if (storage.woodA >= woodNeed) { color1.color = new Color(0, 255, 0, 0.3f); } else { color1.color = new Color(255, 0, 0, 0.3f); }
        if(storage.stoneA >= stoneNeed) { color2.color = new Color(0, 255, 0, 0.3f); } else { color2.color = new Color(255, 0, 0, 0.3f); }
        if(storage.coalA >= coalNeed) { color3.color = new Color(0, 255, 0, 0.3f); } else { color3.color = new Color(255, 0, 0, 0.3f); }
        if(storage.ironA >= ironNeed) { color4.color = new Color(0, 255, 0, 0.3f); } else { color4.color = new Color(255, 0, 0, 0.3f); }
        if(storage.refinedA >= refinedNeed) { color5.color = new Color(0, 255, 0, 0.3f); } else { color5.color = new Color(255, 0, 0, 0.3f); }
        if(storage.diamondA >= diamondNeed) { color6.color = new Color(0, 255, 0, 0.3f); } else { color6.color = new Color(255, 0, 0, 0.3f); }
    }

    public void Upgrade()
    {
        if (storage.woodA >= woodNeed)
        {
            if (storage.stoneA >= stoneNeed)
            {
                if (storage.coalA >= coalNeed)
                {
                    if (storage.ironA >= ironNeed)
                    {
                        if (storage.refinedA >= refinedNeed)
                        {
                            if (storage.diamondA >= diamondNeed)
                            {
                                storage.woodA -= woodNeed;
                                storage.stoneA -= stoneNeed;
                                storage.coalA -= coalNeed;
                                storage.ironA -= ironNeed;
                                storage.refinedA -= refinedNeed;
                                storage.diamondA -= diamondNeed;
                                Level += 1;
                            }
                        }
                    }
                }
            }
        }
    }
    public void CloseWindow() { active = false; }

    void Level1()
    {
        peopleT = 3;
        moneyT = 50;

        peopleU.text = "+ 2";
        moneyU.text = "+ 50";

        woodNeed = 5;
        stoneNeed = 5;
        coalNeed = 3;
        ironNeed = 3;
        refinedNeed = 1;
        diamondNeed = 0;
    }

    void Level2()
    {
        player.maxLife = 4;
        player.staminaMax = 50;

        peopleT = 5;
        moneyT = 100;

        peopleU.text = "+ 4";
        moneyU.text = "+ 50";

        woodNeed = 10;
        stoneNeed = 10;
        coalNeed = 8;
        ironNeed = 8;
        refinedNeed = 5;
        diamondNeed = 1;
    }

    void Level3()
    {
        player.maxLife = 5;
        player.staminaMax = 80;


        peopleT = 7;
        moneyT = 150;

        peopleU.text = "+ 3";
        moneyU.text = "+ 50";

        woodNeed = 15;
        stoneNeed = 15;
        coalNeed = 10;
        ironNeed = 9;
        refinedNeed = 6;
        diamondNeed = 3;
    }

    void Level4()
    {
        player.maxLife = 6;
        player.staminaMax = 120;


        peopleT = 9;
        moneyT = 200;

        woodNeed = 0;
        stoneNeed = 0;
        coalNeed = 0;
        ironNeed = 0;
        refinedNeed = 0;
        diamondNeed = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") { canActive = true; regen = true; }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { canActive = false; regen = false; }
    }
}