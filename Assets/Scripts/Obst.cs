using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obst : MonoBehaviour
{
    public float hp;
    public float dfCof;
    public float time;

    public GameObject damageText;

    public void TakeDamage(float damage)
    {
        hp = hp - (damage * dfCof);
        GetComponent<AudioSource>().Play();

        GameObject text = Instantiate(damageText, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
        text.transform.GetComponentInChildren<TextMeshPro>().color = new Color(1, 1, 0, 1);
        text.transform.GetComponentInChildren<TextMeshPro>().text = "-" + damage;

        if (hp <= 0)
        {
            Die();
        }
    }


    public void Die()
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
