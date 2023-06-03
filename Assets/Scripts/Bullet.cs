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

    private void Start()
    {

    }

    private void Update()
    {
        /*RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, distance, whatIsSolid);
        Debug.DrawRay(transform.position, transform.forward);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                if (Random.Range(0, 100) > critChance)
                    hit.collider.GetComponent<Enemy>().TakeDamage(damage);
                else
                    hit.collider.GetComponent<Enemy>().TakeDamage(damage * critCof);
            }
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }*/
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.CompareTag("Enemy"))
        {
            if (Random.Range(0, 100) < critChance)
                hit.GetComponent<Enemy>().TakeDamage(damage * critCof);
            else
                hit.GetComponent<Enemy>().TakeDamage(damage);

            //Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (hit.CompareTag("Obst"))
        {
            if (Random.Range(0, 100) < critChance)
                hit.GetComponent<Obst>().TakeDamage(damage * critCof);
            else
                hit.GetComponent<Obst>().TakeDamage(damage);

            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (hit.CompareTag("Wall") || hit.CompareTag("Door"))
        {
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
