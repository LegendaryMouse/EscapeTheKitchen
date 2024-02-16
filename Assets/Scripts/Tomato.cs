using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public float explosionRadius;
    public float explosionDamage;

    public bool hitAllies;
    public bool boss;
    public GameObject minion;
    public GameObject megaMinion;
    public GameObject zone;

    public void Explosion()
    {
 
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        for(int i=0; i<targets.Length; i++)
        {
            if (targets[i].tag == "Player")
                targets[i].GetComponent<Player>().TakeDamage(explosionDamage);
            if(hitAllies && targets[i].tag == "Enemy")
                targets[i].GetComponent<Enemy>().TakeDamage(explosionDamage);
            if (targets[i].tag == "Obst")
                targets[i].GetComponent<Obst>().TakeDamage(explosionDamage);
        }
    }
}
