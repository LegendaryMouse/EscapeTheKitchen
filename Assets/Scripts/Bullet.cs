using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public float damage;
    public float critChance;
    public float critCof;
    public LayerMask whatIsSolid;
    public GameObject particles;
    public bool sticky;
    public bool piercing;
    public int piercingStrength;
    public float damageDelayTime;

    public  bool stuck;
    private int piercedTimes;

    private void Update()
    {
        if (!stuck)
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (piercing)
            if (piercedTimes > piercingStrength)
            {
                Destroy(gameObject);
            }
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.CompareTag("Enemy"))
        {
            if (sticky)
            {
                stuck = true;
                transform.SetParent(hit.transform);
            }

            StartCoroutine(GiveDamage(hit));

            if (!sticky)
                if (!piercing)
                    Destroy(gameObject);
                else
                {
                    piercedTimes++;
                }
        }

        if (hit.CompareTag("Obst"))
        {
            if (sticky)
            {
                stuck = true;
                transform.SetParent(hit.transform);
            }

            StartCoroutine(GiveDamage(hit));

            if (particles)
                Instantiate(particles, transform.position, Quaternion.identity);

            if (!sticky)
                if (!piercing)
                    Destroy(gameObject);
                else
                {
                    piercedTimes++;
                }
        }

        if (hit.CompareTag("Wall") | hit.CompareTag("Door"))
        {
            if (particles)
                Instantiate(particles, transform.position, Quaternion.identity);
            if (sticky)
            {
                stuck = true;
                transform.SetParent(hit.transform);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator GiveDamage(Collider2D targ)
    {
        if (targ.CompareTag("Enemy"))
            if (Random.Range(0, 100) < critChance)
                targ.GetComponent<Enemy>().TakeDamage(damage * critCof);
            else
                targ.GetComponent<Enemy>().TakeDamage(damage);

        if (targ.CompareTag("Obst"))
            if (Random.Range(0, 100) < critChance)
                targ.GetComponent<Obst>().TakeDamage(damage * critCof);
            else
                targ.GetComponent<Obst>().TakeDamage(damage);

        yield return new WaitForSeconds(damageDelayTime);
    }

}
