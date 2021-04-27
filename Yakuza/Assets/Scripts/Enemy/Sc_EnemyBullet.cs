using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_EnemyBullet : MonoBehaviour
{
    private int damage;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Sc_PlayerController>())
            {
                int randomisedDamage = Random.Range(1, 10);
                if (randomisedDamage <= 9)
                    //"Normal" damage
                    damage = 1;
                else
                    //Crit damage
                    damage = 2;
                
                collision.gameObject.GetComponent<Sc_PlayerController>().HandleDamage(damage);
            }
        }
    }
}
