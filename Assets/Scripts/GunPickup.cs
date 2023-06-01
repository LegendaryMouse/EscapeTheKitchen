using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject prefabSelf;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player p = collision.GetComponent<Player>();
            for (int i = 0; i < p.weapons.Length; i++)
            {
                if (!p.weapons[i].gameObject)
                {
                    p.weapons[i] = prefabSelf;
                    //Debug.Log("added");
                    Destroy(gameObject);
                    break;
                }
            }
            //GameObject gun = collision.transform.Find(name).gameObject;
            //gun.SetActive(true);
        }
    }
}
