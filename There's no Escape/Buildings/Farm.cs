using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour
{
    public GameObject window;
    public bool active;
    bool canActive;
    public float Level = 0;
    public Text levelT;

    public Text moneyC;

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

    public Animator moneyGain;

    float moneyCan;

    public Text peopleSlotsWork;
    public Text peopleFree;

    float peopleWorking;
    float peopleCanWork;

    float woodNeed;
    float stoneNeed;
    float coalNeed;
    float ironNeed;
    float refinedNeed;
    float diamondNeed;

    float timer;
    public Slider cont;

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
    public Center center;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canActive == true) { if (active == true) { active = false; } else { active = true; } }

        if (canActive == false) { active = false; }

        if (Level > 0 && peopleWorking > 0) { Farming(); }
        Hud();

        if (Level == 0) { Build(); }
        else if (Level == 1) { Level1(); }
        else if (Level == 2) { Level2(); }
        else if (Level == 3) { Level3(); }
        else if (Level == 4) { Level4(); }

        if (active == true) { window.SetActive(true); }
        else { window.SetActive(false); }
    }

    void Hud()
    {
        levelT.text = "Nivel " + Level.ToString();

        peopleSlotsWork.text = peopleWorking.ToString() + " / " + peopleCanWork.ToString();
        peopleFree.text = "Pessoas desocupadas   " + center.peopleL.ToString();

        if (Level < 4)
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
            woodU.text = "Max";
            stoneU.text = "Max";
            coalU.text = "Max";
            ironU.text = "Max";
            refinedU.text = "Max";
            diamondU.text = "Max";
        }

        if(Level == 0) { Gem.sprite = null; Light1.SetActive(false); Light2.SetActive(false); Light3.SetActive(false); Light4.SetActive(false); Gem.gameObject.SetActive(false); }
        else if (Level == 1) { Gem.gameObject.SetActive(true); Gem.sprite = Lv1; Light1.SetActive(true); Light2.SetActive(false); Light3.SetActive(false); Light4.SetActive(false); }
        else if (Level == 2) { Gem.gameObject.SetActive(true); Gem.sprite = Lv2; Light1.SetActive(false); Light2.SetActive(true); Light3.SetActive(false); Light4.SetActive(false); }
        else if (Level == 3) { Gem.gameObject.SetActive(true); Gem.sprite = Lv3; Light1.SetActive(false); Light2.SetActive(false); Light3.SetActive(true); Light4.SetActive(false); }
        else if (Level == 4) { Gem.gameObject.SetActive(true); Gem.sprite = Lv4; Light1.SetActive(false); Light2.SetActive(false); Light3.SetActive(false); Light4.SetActive(true); }

        if (peopleWorking == 0) { Work1.color = new Color(0, 0, 0, 1); Work2.color = new Color(0, 0, 0, 1); Work3.color = new Color(0, 0, 0, 1); }
        else if(peopleWorking == 1) { Work1.color = new Color(255, 255, 255, 1); Work2.color = new Color(0, 0, 0, 1); Work3.color = new Color(0, 0, 0, 1); }
        else if(peopleWorking == 2) { Work1.color = new Color(255, 255, 255, 1); Work2.color = new Color(255, 255, 255, 1); Work3.color = new Color(0, 0, 0, 1); }
        else if(peopleWorking == 3) { Work1.color = new Color(255, 255, 255, 1); Work2.color = new Color(255, 255, 255, 1); Work3.color = new Color(255, 255, 255, 1); }

        if (storage.woodA >= woodNeed) { color1.color = new Color(0, 255, 0, 0.3f); } else { color1.color = new Color(255, 0, 0, 0.3f); }
        if (storage.stoneA >= stoneNeed) { color2.color = new Color(0, 255, 0, 0.3f); } else { color2.color = new Color(255, 0, 0, 0.3f); }
        if (storage.coalA >= coalNeed) { color3.color = new Color(0, 255, 0, 0.3f); } else { color3.color = new Color(255, 0, 0, 0.3f); }
        if (storage.ironA >= ironNeed) { color4.color = new Color(0, 255, 0, 0.3f); } else { color4.color = new Color(255, 0, 0, 0.3f); }
        if (storage.refinedA >= refinedNeed) { color5.color = new Color(0, 255, 0, 0.3f); } else { color5.color = new Color(255, 0, 0, 0.3f); }
        if (storage.diamondA >= diamondNeed) { color6.color = new Color(0, 255, 0, 0.3f); } else { color6.color = new Color(255, 0, 0, 0.3f); }
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

    public void AddWorker()
    {
        if (center.peopleL > 0 && peopleWorking < peopleCanWork)
        {
            peopleWorking += 1;
            center.peopleL -= 1;
            center.peopleW += 1;
            timer = 0;
        }
    }
    public void RemoveWorker()
    {
        if (center.peopleW > 0 && peopleWorking > 0)
        {
            peopleWorking -= 1;
            center.peopleL += 1;
            center.peopleW -= 1;
            timer = 0;
        }
    }

    void Farming()
    {
        if (Level == 1) { timer += Time.deltaTime; }
        else if (Level == 2) { timer += Time.deltaTime * 1.5f; }
        else if (Level == 3) { timer += Time.deltaTime * 1.7f; }
        else if (Level == 4) { timer += Time.deltaTime * 2f; }

        cont.value = timer;
        if (timer >= 60)
        {
            moneyGain.GetComponent<Text>().text = "+ " + moneyCan;
            moneyGain.SetBool("Recieve", true);
            center.moneyA += moneyCan;

            cont.value = 0;
            timer = 0;
        }
        else
        {
            moneyGain.SetBool("Recieve", false);
        }
    }

    void Build()
    {
        moneyC.text = "0";

        peopleCanWork = 0;

        woodNeed = 2;
        stoneNeed = 3;
        coalNeed = 0;
        ironNeed = 0;
        refinedNeed = 0;
        diamondNeed = 0;
    }
    void Level1()
    {
        if (peopleWorking == 0) { moneyCan = 0; moneyC.text = "0"; }
        else if (peopleWorking == 1) { moneyCan = Random.Range(0, 6); moneyC.text = "0 - 5"; }
        else if (peopleWorking == 2) { moneyCan = Random.Range(0, 11); moneyC.text = "0 - 10"; }
        else if (peopleWorking == 3) { moneyCan = Random.Range(0, 16); moneyC.text = "0 - 15"; }

        peopleCanWork = 3;

        woodNeed = 5;
        stoneNeed = 5;
        coalNeed = 1;
        ironNeed = 0;
        refinedNeed = 0;
        diamondNeed = 0;
    }
    void Level2()
    {
        if (peopleWorking == 0) { moneyCan = 0; moneyC.text = "0"; }
        else if (peopleWorking == 1) { moneyCan = Random.Range(1, 8); moneyC.text = "1 - 7"; }
        else if (peopleWorking == 2) { moneyCan = Random.Range(1, 14); moneyC.text = "1 - 13"; }
        else if (peopleWorking == 3) { moneyCan = Random.Range(1, 19); moneyC.text = "1 - 18"; }

        peopleCanWork = 3;

        woodNeed = 10;
        stoneNeed = 8;
        coalNeed = 5;
        ironNeed = 3;
        refinedNeed = 2;
        diamondNeed = 1;
    }
    void Level3()
    {
        if (peopleWorking == 0) { moneyCan = 0; moneyC.text = "0"; }
        else if (peopleWorking == 1) { moneyCan = Random.Range(3, 11); moneyC.text = "3 - 10"; }
        else if (peopleWorking == 2) { moneyCan = Random.Range(3, 16); moneyC.text = "3 - 15"; }
        else if (peopleWorking == 3) { moneyCan = Random.Range(3, 21); moneyC.text = "3 - 20"; }

        peopleCanWork = 3;

        woodNeed = 15;
        stoneNeed = 10;
        coalNeed = 8;
        ironNeed = 6;
        refinedNeed = 3;
        diamondNeed = 2;
    }
    void Level4()
    {
        if (peopleWorking == 0) { moneyCan = 0; moneyC.text = "0"; }
        else if (peopleWorking == 1) { moneyCan = Random.Range(4, 11); moneyC.text = "4 - 10"; }
        else if (peopleWorking == 2) { moneyCan = Random.Range(4, 18); moneyC.text = "4 - 17"; }
        else if (peopleWorking == 3) { moneyCan = Random.Range(4, 24); moneyC.text = "4 - 23"; }

        peopleCanWork = 3;

        woodNeed = 0;
        stoneNeed = 0;
        coalNeed = 0;
        ironNeed = 0;
        refinedNeed = 0;
        diamondNeed = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") { canActive = true; }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { canActive = false; }
    }
}
