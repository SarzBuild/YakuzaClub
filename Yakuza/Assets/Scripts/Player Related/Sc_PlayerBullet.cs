using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PlayerBullet : MonoBehaviour
{
    private int damage;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<Sc_EnemyAI1>())
            {
                int randomisedDamage = Random.Range(1, 10);
                if (randomisedDamage <= 9)
                    //"Normal" damage
                    damage = 4;
                else
                    //Crit damage
                    damage = 5;
                
                collision.gameObject.GetComponent<Sc_EnemyAI1>().GetDamaged(damage);
            }
        }
    }
}
