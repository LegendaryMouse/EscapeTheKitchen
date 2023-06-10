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
        if (hp < 0)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().Play();
            Destroy(GetComponent<Collider2D>());

            if (transform.childCount > 0)
                for (int i = 0; i < transform.childCount + 2; i++)
            {
                try
                {
                    transform.GetChild(0).SetParent(null);
                }
                catch
                {
                    
                }
            }

            Destroy(gameObject);
        }
    }
}
