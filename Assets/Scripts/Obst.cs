using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst : MonoBehaviour
{
    public float hp;
    public float dfCof;

    public void TakeDamage(float damage)
    {
        hp = hp - (damage * dfCof);
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if(hp < 0) 
        {
            Destroy(gameObject);
        }
    }
}
