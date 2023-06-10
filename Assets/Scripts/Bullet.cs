using System.Collections;
using System.Collections.Generic;
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
    private bool stuck;

    private void Start()
    {

    }

    private void Update()
    {
        if (!stuck)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
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

            if (Random.Range(0, 100) < critChance)
                hit.GetComponent<Enemy>().TakeDamage(damage * critCof);
            else
                hit.GetComponent<Enemy>().TakeDamage(damage);

            if (!sticky)
                Destroy(gameObject);
        }

        if (hit.CompareTag("Obst"))
        {
            if (sticky)
            {
                stuck = true;
                transform.SetParent(hit.transform);
            }

            if (Random.Range(0, 100) < critChance)
                hit.GetComponent<Obst>().TakeDamage(damage * critCof);
            else
                hit.GetComponent<Obst>().TakeDamage(damage);

            Instantiate(particles, transform.position, Quaternion.identity);

            if (!sticky)
                Destroy(gameObject);
        }

        if (hit.CompareTag("Wall") || hit.CompareTag("Door"))
        {
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

}
