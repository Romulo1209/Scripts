using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    public GameObject window;
    public bool active;
    bool canActive;
    public float Level = 0;
    public Text levelT;

    public Text woodU;
    public Text stoneU;
    public Text coalU;
    public Text ironU;
    public Text refinedU;
    public Text diamondU;

    public Text stoneC;
    public Text coalC;
    public Text ironC;
    public Text diamondC;

    public Image color1;
    public Image color2;
    public Image color3;
    public Image color4;
    public Image color5;
    public Image color6;

    public Animator StoneGain;
    public Animator CoalGain;
    public Animator IronGain;
    public Animator DiamondGain;

    public Text peopleSlotsWork;
    public Text peopleFree;

    public float peopleCanWork;
    public float peopleWorking;

    float woodNeed;
    float stoneNeed;
    float coalNeed;
    float ironNeed;
    float refinedNeed;
    float diamondNeed;

    float stoneCan;
    float coalCan;
    float ironCan;
    float diamondCan;

    float timer = 0;
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

        if(Level == 0) { Build(); }
        else if(Level == 1) { Level1(); }
        else if(Level == 2) { Level2(); }
        else if(Level == 3) { Level3(); }
        else if(Level == 4) { Level4(); }

        Hud();

        if(Level > 0 && peopleWorking > 0) { Mining(); }

        if (active == true) { window.SetActive(true); }
        else { window.SetActive(false); }
    }

    void Hud()
    {
        levelT.text = "Nivel " + Level.ToString();

        peopleSlotsWork.text = peopleWorking.ToString() + " / " + peopleCanWork.ToString();
        peopleFree.text = "Pessoas desocupadas   " + center.peopleL.ToString();

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
            woodU.text = "Max";
            stoneU.text = "Max";
            coalU.text = "Max";
            ironU.text = "Max";
            refinedU.text = "Max";
            diamondU.text = "Max";
        }

        if (Level == 0) { Gem.sprite = null; Light1.SetActive(false); Light2.SetActive(false); Light3.SetActive(false); Light4.SetActive(false); Gem.gameObject.SetActive(false); }
        else if (Level == 1) { Gem.gameObject.SetActive(true); Gem.sprite = Lv1; Light1.SetActive(true); Light2.SetActive(false); Light3.SetActive(false); Light4.SetActive(false); }
        else if (Level == 2) { Gem.gameObject.SetActive(true); Gem.sprite = Lv2; Light1.SetActive(false); Light2.SetActive(true); Light3.SetActive(false); Light4.SetActive(false); }
        else if (Level == 3) { Gem.gameObject.SetActive(true); Gem.sprite = Lv3; Light1.SetActive(false); Light2.SetActive(false); Light3.SetActive(true); Light4.SetActive(false); }
        else if (Level == 4) { Gem.gameObject.SetActive(true); Gem.sprite = Lv4; Light1.SetActive(false); Light2.SetActive(false); Light3.SetActive(false); Light4.SetActive(true); }

        if (peopleWorking == 0) { Work1.color = new Color(0, 0, 0, 1); Work2.color = new Color(0, 0, 0, 1); Work3.color = new Color(0, 0, 0, 1); }
        else if (peopleWorking == 1) { Work1.color = new Color(255, 255, 255, 1); Work2.color = new Color(0, 0, 0, 1); Work3.color = new Color(0, 0, 0, 1); }
        else if (peopleWorking == 2) { Work1.color = new Color(255, 255, 255, 1); Work2.color = new Color(255, 255, 255, 1); Work3.color = new Color(0, 0, 0, 1); }
        else if (peopleWorking == 3) { Work1.color = new Color(255, 255, 255, 1); Work2.color = new Color(255, 255, 255, 1); Work3.color = new Color(255, 255, 255, 1); }

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
        if(center.peopleL > 0 && peopleWorking < peopleCanWork)
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

    void Mining()
    {
        if(Level == 1) { timer += Time.deltaTime; }
        else if(Level == 2) { timer += Time.deltaTime * 1.5f; }
        else if(Level == 3) { timer += Time.deltaTime * 1.7f; }
        else if(Level == 4) { timer += Time.deltaTime * 2f; }
        
        cont.value = timer;
        if (timer >= 60)
        {
            StoneGain.GetComponent<Text>().text = "+ " + stoneCan;
            CoalGain.GetComponent<Text>().text = "+ " + coalCan;
            IronGain.GetComponent<Text>().text = "+ " + ironCan;
            DiamondGain.GetComponent<Text>().text = "+ " + diamondCan;

            if(stoneCan > 0) { StoneGain.SetBool("Recieve", true); }
            if(coalCan > 0) { CoalGain.SetBool("Recieve", true); }
            if(ironCan > 0) { IronGain.SetBool("Recieve", true); }
            if(diamondCan > 0) { DiamondGain.SetBool("Recieve", true); }
            
            storage.stoneA += stoneCan;
            storage.coalA += coalCan;
            storage.ironA += ironCan;
            storage.diamondA += diamondCan;

            cont.value = 0;
            timer = 0;
        }
        else
        {
            StoneGain.SetBool("Recieve", false);
            CoalGain.SetBool("Recieve", false);
            IronGain.SetBool("Recieve", false);
            DiamondGain.SetBool("Recieve", false);
        }
    }

    void Build()
    {
        stoneC.text = "0"; coalC.text = "0"; ironC.text = "0"; diamondC.text = "0";

        peopleCanWork = 0;

        woodNeed = 3;
        stoneNeed = 2;
        coalNeed = 0;
        ironNeed = 0;
        refinedNeed = 0;
        diamondNeed = 0;
    }

    void Level1()
    {
        if(peopleWorking == 0) { stoneCan = 0; coalCan = 0; ironCan = 0; diamondCan = 0; stoneC.text = "0"; coalC.text = "0"; ironC.text = "0"; diamondC.text = "0"; }
        else if(peopleWorking == 1) { stoneCan = Random.Range(0, 2); coalCan = Random.Range(0, 2); ironCan = 0; diamondCan = 0; stoneC.text = "0 - 1"; coalC.text = "0 - 1"; ironC.text = "0"; diamondC.text = "0"; }
        else if(peopleWorking == 2) { stoneCan = Random.Range(0, 3); coalCan = Random.Range(0, 2); ironCan = 0; diamondCan = 0; stoneC.text = "0 - 2"; coalC.text = "0 - 1"; ironC.text = "0"; diamondC.text = "0"; }
        else if(peopleWorking == 3) { stoneCan = Random.Range(0, 4); coalCan = Random.Range(0, 3); ironCan = 0; diamondCan = 0; stoneC.text = "0 - 3"; coalC.text = "0 - 2"; ironC.text = "0"; diamondC.text = "0"; }

        peopleCanWork = 3;

        woodNeed = 5;
        stoneNeed = 3;
        coalNeed = 1;
        ironNeed = 0;
        refinedNeed = 0;
        diamondNeed = 0;
    }

    void Level2()
    {
        if (peopleWorking == 0) { stoneCan = 0; coalCan = 0; ironCan = 0; diamondCan = 0; stoneC.text = "0"; coalC.text = "0"; ironC.text = "0"; diamondC.text = "0"; }
        else if (peopleWorking == 1) { stoneCan = Random.Range(1, 3); coalCan = Random.Range(0, 3); ironCan = Random.Range(0, 2); diamondCan = 0; stoneC.text = "1 - 2"; coalC.text = "0 - 2"; ironC.text = "0 - 1"; diamondC.text = "0 - 1"; }
        else if (peopleWorking == 2) { stoneCan = Random.Range(1, 4); coalCan = Random.Range(0, 4); ironCan = Random.Range(0, 3); diamondCan = Random.Range(0, 2); stoneC.text = "1 - 3"; coalC.text = "0 - 3"; ironC.text = "0 - 2"; diamondC.text = "0 - 1"; }
        else if (peopleWorking == 3) { stoneCan = Random.Range(1, 5); coalCan = Random.Range(0, 5); ironCan = Random.Range(0, 4); diamondCan = Random.Range(0, 2); stoneC.text = "1 - 4"; coalC.text = "0 - 4"; ironC.text = "0 - 3"; diamondC.text = "0 - 1"; }

        peopleCanWork = 3;

        woodNeed = 6;
        stoneNeed = 4;
        coalNeed = 3;
        ironNeed = 4;
        refinedNeed = 2;
        diamondNeed = 0;
    }

    void Level3()
    {
        if (peopleWorking == 0) { stoneCan = 0; coalCan = 0; ironCan = 0; diamondCan = 0; stoneC.text = "0"; coalC.text = "0"; ironC.text = "0"; diamondC.text = "0"; }
        else if (peopleWorking == 1) { stoneCan = Random.Range(2, 4); coalCan = Random.Range(1, 4); ironCan = Random.Range(0, 3); diamondCan = Random.Range(0, 2); stoneC.text = "2 - 3"; coalC.text = "1 - 3"; ironC.text = "0 - 2"; diamondC.text = "0 - 1"; }
        else if (peopleWorking == 2) { stoneCan = Random.Range(2, 5); coalCan = Random.Range(1, 5); ironCan = Random.Range(0, 4); diamondCan = Random.Range(0, 2); stoneC.text = "2 - 4"; coalC.text = "1 - 4"; ironC.text = "0 - 3"; diamondC.text = "0 - 1"; }
        else if (peopleWorking == 3) { stoneCan = Random.Range(2, 6); coalCan = Random.Range(1, 6); ironCan = Random.Range(0, 5); diamondCan = Random.Range(0, 3); stoneC.text = "2 - 5"; coalC.text = "1 - 5"; ironC.text = "0 - 4"; diamondC.text = "0 - 2"; }

        peopleCanWork = 3;

        woodNeed = 8;
        stoneNeed = 6;
        coalNeed = 5;
        ironNeed = 6;
        refinedNeed = 4;
        diamondNeed = 2;
    }

    void Level4()
    {
        if (peopleWorking == 0) { stoneCan = 0; coalCan = 0; ironCan = 0; diamondCan = 0; stoneC.text = "0"; coalC.text = "0"; ironC.text = "0"; diamondC.text = "0"; }
        else if (peopleWorking == 1) { stoneCan = Random.Range(3, 6); coalCan = Random.Range(2, 5); ironCan = Random.Range(1, 4); diamondCan = Random.Range(0, 2); stoneC.text = "3 - 5"; coalC.text = "2 - 4"; ironC.text = "1 - 3"; diamondC.text = "0 - 1"; }
        else if (peopleWorking == 2) { stoneCan = Random.Range(3, 7); coalCan = Random.Range(2, 6); ironCan = Random.Range(1, 5); diamondCan = Random.Range(0, 3); stoneC.text = "3 - 6"; coalC.text = "2 - 5"; ironC.text = "1 - 4"; diamondC.text = "0 - 2"; }
        else if (peopleWorking == 3) { stoneCan = Random.Range(3, 8); coalCan = Random.Range(2, 7); ironCan = Random.Range(1, 6); diamondCan = Random.Range(0, 3); stoneC.text = "3 - 7"; coalC.text = "2 - 6"; ironC.text = "1 - 5"; diamondC.text = "0 - 2"; }

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