using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;

public class Storage : MonoBehaviour
{
    public GameObject window;
    public bool active;
    bool canActive;
    public float Level = 1;
    public Text LevelT;

    public Text wood;
    public Text stone;
    public Text coal;
    public Text iron;
    public Text refined;
    public Text diamond;

    public Text woodR;
    public Text stoneR;
    public Text coalR;
    public Text ironR;
    public Text refinedR;
    public Text diamondR;

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

    float woodNeed;
    float stoneNeed;
    float coalNeed;
    float ironNeed;
    float refinedNeed;
    float diamondNeed;

    public float woodA;
    public float stoneA;
    public float coalA;
    public float ironA;
    public float refinedA;
    public float diamondA;

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

    float woodT;
    float stoneT;
    float coalT;
    float ironT;
    float refinedT;
    float diamondT;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canActive == true) { if (active == true) { active = false; } else { active = true; } }

        if (canActive == false) { active = false; }

        Hud();
        if (Level == 1) { Level1(); }
        else if (Level == 2) { Level2(); }
        else if (Level == 3) { Level3(); }
        else if (Level == 4) { Level4(); }

        if (active == true) { window.SetActive(true); }
        else { window.SetActive(false); }
    }

    void Hud()
    {
        LevelT.text = "Nivel " + Level.ToString();

        wood.text = woodA.ToString() + " / " + woodT.ToString();
        stone.text = stoneA.ToString() + " / " + stoneT.ToString();
        coal.text = coalA.ToString() + " / " + coalT.ToString();
        iron.text = ironA.ToString() + " / " + ironT.ToString();
        refined.text = refinedA.ToString() + " / " + refinedT.ToString();
        diamond.text = diamondA.ToString() + " / " + diamondT.ToString();

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
            woodR.text = "Max";
            stoneR.text = "Max";
            coalR.text = "Max";
            ironR.text = "Max";
            refinedR.text = "Max";
            diamondR.text = "Max";

            woodU.text = "Max";
            stoneU.text = "Max";
            coalU.text = "Max";
            ironU.text = "Max";
            refinedU.text = "Max";
            diamondU.text = "Max";
        }

        if(Level == 1) { Gem.sprite = Lv1; Light1.SetActive(true); Light2.SetActive(false); Light3.SetActive(false); Light4.SetActive(false); }
        else if(Level == 2) { Gem.sprite = Lv2; Light1.SetActive(false); Light2.SetActive(true); Light3.SetActive(false); Light4.SetActive(false); }
        else if(Level == 3) { Gem.sprite = Lv3; Light1.SetActive(false); Light2.SetActive(false); Light3.SetActive(true); Light4.SetActive(false); }
        else if(Level == 4) { Gem.sprite = Lv4; Light1.SetActive(false); Light2.SetActive(false); Light3.SetActive(false); Light4.SetActive(true); }
        
        if(woodA > woodT) { woodA = woodT; }
        if(stoneA > stoneT) { stoneA = stoneT; }
        if(coalA > coalT) { coalA = coalT; }
        if(ironA > ironT) { ironA = ironT; }
        if(refinedA > refinedT) { refinedA = refinedT; }
        if(diamondA > diamondT) { diamondA = diamondT; }

        if (woodA >= woodNeed) { color1.color = new Color(0,255,0, 0.3f); } else { color1.color = new Color(255, 0, 0, 0.3f); }
        if (stoneA >= stoneNeed) { color2.color = new Color(0,255,0, 0.3f); } else { color2.color = new Color(255, 0, 0, 0.3f); }
        if (coalA >= coalNeed) { color3.color = new Color(0,255,0, 0.3f); } else { color3.color = new Color(255, 0, 0, 0.3f); }
        if (ironA >= ironNeed) { color4.color = new Color(0,255,0, 0.3f); } else { color4.color = new Color(255, 0, 0, 0.3f); }
        if (refinedA >= refinedNeed) { color5.color = new Color(0,255,0, 0.3f); } else { color5.color = new Color(255, 0, 0, 0.3f); }
        if (diamondA >= diamondNeed) { color6.color = new Color(0,255,0, 0.3f); } else { color6.color = new Color(255, 0, 0, 0.3f); }
    }

    public void UpgradeSystem()
    {
        if(woodA >= woodNeed)
        {
            if(stoneA >= stoneNeed)
            {
                if(coalA >= coalNeed)
                {
                    if(ironA >= ironNeed)
                    {
                        if(refinedA >= refinedNeed)
                        {
                            if(diamondA >= diamondNeed)
                            {
                                woodA -= woodNeed;
                                stoneA -= stoneNeed;
                                coalA -= coalNeed;
                                ironA -= ironNeed;
                                refinedA -= refinedNeed;
                                diamondA -= diamondNeed;
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
        woodT = 10;
        stoneT = 10;
        coalT = 3;
        ironT = 1;
        refinedT = 1;
        diamondT = 1;

        woodR.text = "+ 3";
        stoneR.text = "+ 3";
        coalR.text = "+ 3";
        ironR.text = "+ 2";
        refinedR.text = "+ 0";
        diamondR.text = "+ 0";

        woodNeed = 3;
        stoneNeed = 2;
        coalNeed = 1;
        ironNeed = 0;
        refinedNeed = 0;
        diamondNeed = 0;
    }

    void Level2()
    {
        woodT = 13;
        stoneT = 13;
        coalT = 6;
        ironT = 3;
        refinedT = 1;
        diamondT = 1;

        woodR.text = "+ 4";
        stoneR.text = "+ 4";
        coalR.text = "+ 3";
        ironR.text = "+ 3";
        refinedR.text = "+ 2";
        diamondR.text = "+ 1";

        woodNeed = 5;
        stoneNeed = 4;
        coalNeed = 3;
        ironNeed = 2;
        refinedNeed = 1;
        diamondNeed = 0;
    }

    void Level3()
    {
        woodT = 17;
        stoneT = 17;
        coalT = 9;
        ironT = 6;
        refinedT = 3;
        diamondT = 2;

        woodR.text = "+ 3";
        stoneR.text = "+ 3";
        coalR.text = "+ 3";
        ironR.text = "+ 3";
        refinedR.text = "+ 3";
        diamondR.text = "+ 1";

        woodNeed = 8;
        stoneNeed = 6;
        coalNeed = 5;
        ironNeed = 4;
        refinedNeed = 3;
        diamondNeed = 1;
    }

    void Level4()
    {
        woodT = 20;
        stoneT = 20;
        coalT = 12;
        ironT = 9;
        refinedT = 6;
        diamondT = 3;

        woodNeed = 0;
        stoneNeed = 0;
        coalNeed = 0;
        ironNeed = 0;
        refinedNeed = 0;
        diamondNeed = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player") { canActive = true; }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { canActive = false; }
    }
}