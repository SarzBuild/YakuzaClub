using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_UI_Healthbar : MonoBehaviour
{
    public GameObject player;
    private SpriteRenderer tick1;
    private SpriteRenderer tick2;
    private SpriteRenderer tick3;
    private SpriteRenderer tick4;
    private SpriteRenderer tick5;
    private SpriteRenderer tick6;
    private SpriteRenderer tick7;
    private SpriteRenderer tick8;
    public int hp;
    private Text chargerText;
    public int currentMagazineDisplay;
    public float currentBulletCountToDisplay;
    private Image bulletCount;


    //Get the UI elements for Health Ticks
    void Start()
    {
        tick1 = transform.Find("Healthbar_Tick1").GetComponent<SpriteRenderer>();
        tick2 = transform.Find("Healthbar_Tick2").GetComponent<SpriteRenderer>();
        tick3 = transform.Find("Healthbar_Tick3").GetComponent<SpriteRenderer>();
        tick4 = transform.Find("Healthbar_Tick4").GetComponent<SpriteRenderer>();
        tick5 = transform.Find("Healthbar_Tick5").GetComponent<SpriteRenderer>();
        tick6 = transform.Find("Healthbar_Tick6").GetComponent<SpriteRenderer>();
        tick7 = transform.Find("Healthbar_Tick7").GetComponent<SpriteRenderer>();
        tick8 = transform.Find("Healthbar_Tick8").GetComponent<SpriteRenderer>();
        chargerText = transform.Find("ChargerText").GetComponent<Text>();
        bulletCount = transform.Find("Gun").GetComponent<Image>();
    }


    //Update the UI elements for Health Ticks
    void Update()
    {
        hp = player.GetComponent<Sc_PlayerStats>().currentHealth;

        if (hp > 0) {
            tick1.enabled = true;
            if (hp > 1) {
                tick2.enabled = true;
                if (hp > 2) {
                    tick3.enabled = true;
                    if (hp > 3) {
                        tick4.enabled = true;
                        if (hp > 4) { 
                            tick5.enabled = true;
                            if (hp > 5) {
                                tick6.enabled = true;
                                if (hp > 6) {
                                    tick7.enabled = true;
                                    if (hp > 7) {
                                        tick8.enabled = true;
                                    } else {
                                        tick8.enabled = false;
                                    }
                                } else {
                                    tick7.enabled = false;
                                }
                            } else {
                                tick6.enabled = false;
                            }
                        } else {
                            tick5.enabled = false;
                        }
                    } else {
                        tick4.enabled = false;
                    }
                } else {
                    tick3.enabled = false;
                }
            } else { 
                tick2.enabled = false; 
            }
        } else {
            tick1.enabled = false;
        }

        currentMagazineDisplay = player.GetComponent<Sc_PlayerStats>().currentMagazine;
        chargerText.text = currentMagazineDisplay.ToString();

        currentBulletCountToDisplay = player.GetComponent<Sc_PlayerStats>().currentAmmoInMagazine;
        bulletCount.fillAmount = currentBulletCountToDisplay/ 10;
    }
}
