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
    }
    
    //Player Stats
    public float walkSpeed = 4f; 
    public int maxHealth = 100;
    
    //Inventory
    public int maxAmmoInMagazine = 10;
    public int maxMagazine = 3; 
    
    //Prefabs
    public GameObject bulletPrefab;

}
