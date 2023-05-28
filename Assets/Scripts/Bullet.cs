using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public float damage;
    public LayerMask whatIsSolid;

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance,  whatIsSolid);
        if(hit.collider != null)
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
            //Debug.Log(hit.collider);
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    
}
