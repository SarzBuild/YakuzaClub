using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sc_PlayerStats : MonoBehaviour
{ 
    private static Sc_PlayerStats _statsInstance;
    public static Sc_PlayerStats StatsInstance
    {
        get
        {
            return _statsInstance;
        }
    }

    private void Awake()
    {
        if (_statsInstance != null && _statsInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _statsInstance = this;
        }

        currentHealth = maxHealth;
    }

    //Quick keybinds to test player losing and gaining HP on the UI
    private void Update()
    {

    }



    //Player Stats
    public float walkSpeed = 4f;
    public int currentHealth;
    public int maxHealth = 8;
    
    //Inventory
    public int maxAmmoInMagazine = 10;
    public int currentAmmoInMagazine = 10;
    public int maxMagazine = 3;
    public int currentMagazine = 3;
    
    //Prefabs
    public GameObject bulletPrefab;
    public GameObject consumableHP;
    public GameObject consumableMagazine;

}
